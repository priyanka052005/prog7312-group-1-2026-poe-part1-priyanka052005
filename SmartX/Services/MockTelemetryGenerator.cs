//The Smart-X service classes were developed with assistance from Mircosoft Learn for
//telemetry processing, mock telemetry generation, application storage, alert generation
//and file protection. The custom TelemetryBuffer<T> was informed by Microsoft documentation
//on C# collections and implementing custom collections.
//The file encryption/protection functionality was informed by ASP.NET Core Data
//Protection documentation

using SmartX.Models;

namespace SmartX.Services
{
    public class MockTelemetryGenerator
    {
        private readonly SmartXStore _store;
        private readonly Random _random = new();

        public MockTelemetryGenerator(SmartXStore store)
        {
            _store = store;
        }

        public List<TelemetryRecord> GenerateBatch(int count)
        {
            var generated = new List<TelemetryRecord>();

            if (_store.Sensors.Count == 0)
            {
                return generated;
            }

            for (int i = 0; i < count; i++)
            {
                var sensor =
                    _store.Sensors[
                        _random.Next(_store.Sensors.Count)
                    ];

                var record =
                    CreateReading(sensor);

                _store.Telemetry.Add(record);

                generated.Add(record);
            }

            return generated;
        }

        private TelemetryRecord CreateReading(
            SensorRegistration sensor)
        {
            return sensor.Category switch
            {
                "Environmental" =>
                    CreateEnvironmentalReading(sensor),

                "Power Consumption" =>
                    CreatePowerReading(sensor),

                "Actuator" =>
                    CreateActuatorReading(sensor),

                _ =>
                    CreateDefaultReading(sensor)
            };
        }

        private TelemetryRecord CreateEnvironmentalReading(
            SensorRegistration sensor)
        {
            var option =
                _random.Next(0, 3);

            if (option == 0)
            {
                var temperature =
                    Math.Round(
                        18 + (_random.NextDouble() * 18),
                        1
                    );

                return new TelemetryRecord
                {
                    DeviceId = sensor.DeviceId,
                    Metric = "Temperature",
                    DataType = "double",
                    DisplayValue =
                        temperature.ToString(),
                    Unit = "°C",
                    Timestamp = DateTime.UtcNow
                };
            }

            if (option == 1)
            {
                var moisture =
                    Math.Round(
                        10 + (_random.NextDouble() * 80),
                        1
                    );

                return new TelemetryRecord
                {
                    DeviceId = sensor.DeviceId,
                    Metric = "Soil Moisture",
                    DataType = "double",
                    DisplayValue =
                        moisture.ToString(),
                    Unit = "%",
                    Timestamp = DateTime.UtcNow
                };
            }

            var humidity =
                Math.Round(
                    30 + (_random.NextDouble() * 60),
                    1
                );

            return new TelemetryRecord
            {
                DeviceId = sensor.DeviceId,
                Metric = "Humidity",
                DataType = "double",
                DisplayValue =
                    humidity.ToString(),
                Unit = "%",
                Timestamp = DateTime.UtcNow
            };
        }

        private TelemetryRecord CreatePowerReading(
            SensorRegistration sensor)
        {
            var watts =
                _random.Next(
                    300,
                    1600
                );

            return new TelemetryRecord
            {
                DeviceId = sensor.DeviceId,
                Metric = "Power",
                DataType = "int",
                DisplayValue =
                    watts.ToString(),
                Unit = "W",
                Timestamp = DateTime.UtcNow
            };
        }

        private TelemetryRecord CreateActuatorReading(
            SensorRegistration sensor)
        {
            var state =
                _random.Next(0, 2) == 1;

            return new TelemetryRecord
            {
                DeviceId = sensor.DeviceId,
                Metric = "ValveOpen",
                DataType = "bool",
                DisplayValue =
                    state.ToString(),
                Unit = "",
                Timestamp = DateTime.UtcNow
            };
        }

        private TelemetryRecord CreateDefaultReading(
            SensorRegistration sensor)
        {
            return new TelemetryRecord
            {
                DeviceId = sensor.DeviceId,
                Metric = "GenericValue",
                DataType = "double",
                DisplayValue = "0",
                Unit = "",
                Timestamp = DateTime.UtcNow
            };
        }
    }
}

//Reference list
//aartig13 (2022). Implement custom collections in visual C# - C#. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/implement-custom-collection [Accessed 14 Sept. 2026].
//BillWagner (2023). Collections - C#. [online] learn.microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/collections [Accessed 14 Sept. 2026].
//tdykstra (2026). ASP.NET core data protection overview. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].