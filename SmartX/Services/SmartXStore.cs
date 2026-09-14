//The Smart-X service classes were developed with assistance from Mircosoft Learn for
//telemetry processing, mock telemetry generation, application storage, alert generation
//and file protection. The custom TelemetryBuffer<T> was informed by Microsoft documentation
//on C# collections and implementing custom collections.
//The file encryption/protection functionality was informed by ASP.NET Core Data
//Protection documentation

using SmartX.Models;

namespace SmartX.Services
{
    public class SmartXStore
    {
        public List<SensorRegistration> Sensors { get; } = new();

        public TelemetryBuffer<TelemetryRecord> Telemetry { get; }
     = new TelemetryBuffer<TelemetryRecord>(5000);

        public List<SensorAttachment> Attachments { get; } = new();

        public SmartXStore()
        {
            Sensors.AddRange(new[]
            {
                new SensorRegistration
                {
                    DeviceId = "ESP32-A1",
                    Location = "Greenhouse / Zone A",
                    Category = "Environmental",
                    Status = "Online",
                    CreatedAt = DateTime.UtcNow.AddMinutes(-25)
                },

                new SensorRegistration
                {
                    DeviceId = "ESP32-B2",
                    Location = "Utility Room",
                    Category = "Power Consumption",
                    Status = "Online",
                    CreatedAt = DateTime.UtcNow.AddMinutes(-20)
                },

                new SensorRegistration
                {
                    DeviceId = "ESP32-C3",
                    Location = "Hydroponics / Zone B",
                    Category = "Actuator",
                    Status = "Online",
                    CreatedAt = DateTime.UtcNow.AddMinutes(-15)
                }
            });

            Telemetry.AddRange(new[]
            {
                new TelemetryRecord
                {
                    DeviceId = "ESP32-A1",
                    Metric = "Temperature",
                    DataType = "double",
                    DisplayValue = "23.6",
                    Unit = "°C",
                    Timestamp = DateTime.UtcNow.AddMinutes(-5)
                },

                new TelemetryRecord
                {
                    DeviceId = "ESP32-B2",
                    Metric = "Power",
                    DataType = "int",
                    DisplayValue = "850",
                    Unit = "W",
                    Timestamp = DateTime.UtcNow.AddMinutes(-4)
                },

                new TelemetryRecord
                {
                    DeviceId = "ESP32-C3",
                    Metric = "ValveOpen",
                    DataType = "bool",
                    DisplayValue = "false",
                    Unit = "",
                    Timestamp = DateTime.UtcNow.AddMinutes(-3)
                }
            });
        }
    }
}

//Reference list
//aartig13 (2022). Implement custom collections in visual C# - C#. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/implement-custom-collection [Accessed 14 Sept. 2026].
//BillWagner (2023). Collections - C#. [online] learn.microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/collections [Accessed 14 Sept. 2026].
//tdykstra (2026). ASP.NET core data protection overview. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].