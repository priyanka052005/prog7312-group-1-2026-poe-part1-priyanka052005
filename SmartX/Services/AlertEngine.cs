//The Smart-X service classes were developed with assistance from Mircosoft Learn for
//telemetry processing, mock telemetry generation, application storage, alert generation
//and file protection. The custom TelemetryBuffer<T> was informed by Microsoft documentation
//on C# collections and implementing custom collections.
//The file encryption/protection functionality was informed by ASP.NET Core Data
//Protection documentation
using SmartX.Models;

namespace SmartX.Services
{
    public static class AlertEngine
    {
        public static List<DashboardAlert> BuildAlerts(SmartXStore store)
        {
            var alerts = new List<DashboardAlert>();

            foreach (var sensor in store.Sensors)
            {
                var latest = store.Telemetry
                    .Where(t =>
                        t.DeviceId.Equals(
                            sensor.DeviceId,
                            StringComparison.OrdinalIgnoreCase
                        )
                    )
                    .OrderByDescending(t => t.Timestamp)
                    .FirstOrDefault();

                if (latest == null)
                {
                    alerts.Add(new DashboardAlert
                    {
                        Severity = "Warning",
                        DeviceId = sensor.DeviceId,
                        AlertType = "No Telemetry",
                        Message = "No telemetry has been received from this sensor.",
                        SuggestedAction = "Check whether the sensor is powered on and connected to the network.",
                        CurrentValue = "No data"
                    });

                    continue;
                }

                var age = DateTime.UtcNow - latest.Timestamp;

                if (age.TotalMinutes > 10)
                {
                    alerts.Add(new DashboardAlert
                    {
                        Severity = "Critical",
                        DeviceId = sensor.DeviceId,
                        AlertType = "Sensor Disconnect",
                        Message = "No recent telemetry has been received from this sensor.",
                        SuggestedAction = "Check the sensor power supply, Wi-Fi connection and device status.",
                        CurrentValue = $"{Math.Round(age.TotalMinutes)} minutes since last reading"
                    });
                }

                if (
                    latest.Metric.Contains(
                        "temperature",
                        StringComparison.OrdinalIgnoreCase
                    )
                    &&
                    double.TryParse(
                        latest.DisplayValue,
                        out var temperature
                    )
                    &&
                    temperature > 30
                )
                {
                    alerts.Add(new DashboardAlert
                    {
                        Severity = "Critical",
                        DeviceId = sensor.DeviceId,
                        AlertType = "Temperature Spike",
                        Message =
                            $"Temperature is unusually high at {temperature}{latest.Unit}.",
                        SuggestedAction =
                            "Check the sensor location, surrounding equipment and environmental conditions.",
                        CurrentValue =
                            $"{temperature}{latest.Unit}"
                    });
                }

                if (
                    latest.Metric.Contains(
                        "moisture",
                        StringComparison.OrdinalIgnoreCase
                    )
                    &&
                    double.TryParse(
                        latest.DisplayValue,
                        out var moisture
                    )
                    &&
                    moisture < 20
                )
                {
                    alerts.Add(new DashboardAlert
                    {
                        Severity = "Critical",
                        DeviceId = sensor.DeviceId,
                        AlertType = "Low Moisture",
                        Message =
                            $"Soil moisture is critically low at {moisture}{latest.Unit}.",
                        SuggestedAction =
                            "Inspect the irrigation system and verify that water is reaching the affected zone.",
                        CurrentValue =
                            $"{moisture}{latest.Unit}"
                    });
                }

                if (
                    latest.Metric.Contains(
                        "power",
                        StringComparison.OrdinalIgnoreCase
                    )
                    &&
                    int.TryParse(
                        latest.DisplayValue,
                        out var power
                    )
                    &&
                    power > 1200
                )
                {
                    alerts.Add(new DashboardAlert
                    {
                        Severity = "Warning",
                        DeviceId = sensor.DeviceId,
                        AlertType = "Power Spike",
                        Message =
                            $"Power consumption is unusually high at {power}{latest.Unit}.",
                        SuggestedAction =
                            "Check connected equipment for abnormal power usage or possible faults.",
                        CurrentValue =
                            $"{power}{latest.Unit}"
                    });
                }
            }

            if (alerts.Count == 0)
            {
                alerts.Add(new DashboardAlert
                {
                    Severity = "Info",
                    DeviceId = "SYSTEM",
                    AlertType = "System Normal",
                    Message =
                        "All monitored sensors are currently within normal ranges.",
                    SuggestedAction =
                        "No immediate action is required.",
                    CurrentValue =
                        "Normal"
                });
            }

            return alerts;
        }
    }
}

//Reference list
//aartig13 (2022). Implement custom collections in visual C# - C#. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/implement-custom-collection [Accessed 14 Sept. 2026].
//BillWagner (2023). Collections - C#. [online] learn.microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/collections [Accessed 14 Sept. 2026].
//tdykstra (2026). ASP.NET core data protection overview. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].