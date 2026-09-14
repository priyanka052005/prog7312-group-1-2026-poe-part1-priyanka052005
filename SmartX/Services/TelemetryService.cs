//The Smart-X service classes were developed with assistance from Mircosoft Learn for
//telemetry processing, mock telemetry generation, application storage, alert generation
//and file protection. The custom TelemetryBuffer<T> was informed by Microsoft documentation
//on C# collections and implementing custom collections.
//The file encryption/protection functionality was informed by ASP.NET Core Data
//Protection documentation

using SmartX.Models;

namespace SmartX.Services
{
    public class TelemetryService
    {
        private readonly SmartXStore _store;

        public TelemetryService(SmartXStore store)
        {
            _store = store;
        }

        public TelemetryRecord ProcessInput(TelemetryInput input)
        {
            var type = input.DataType.Trim().ToLowerInvariant();

            return type switch
            {
                "double" => ProcessDouble(input),
                "float" => ProcessDouble(input),
                "int" => ProcessInt(input),
                "integer" => ProcessInt(input),
                "bool" => ProcessBool(input),
                "boolean" => ProcessBool(input),

                _ => throw new ArgumentException(
                    "Data type must be double, int, or bool."
                )
            };
        }

        private TelemetryRecord ProcessDouble(TelemetryInput input)
        {
            if (!double.TryParse(input.Value, out var value))
            {
                throw new ArgumentException(
                    "Value must be a valid decimal number."
                );
            }

            var packet = new TelemetryPacket<double>
            {
                DeviceId = input.DeviceId,
                Metric = input.Metric,
                Value = value,
                Unit = input.Unit,
                Timestamp = DateTime.UtcNow
            };

            return SavePacket(packet, "double");
        }

        private TelemetryRecord ProcessInt(TelemetryInput input)
        {
            if (!int.TryParse(input.Value, out var value))
            {
                throw new ArgumentException(
                    "Value must be a valid whole number."
                );
            }

            var packet = new TelemetryPacket<int>
            {
                DeviceId = input.DeviceId,
                Metric = input.Metric,
                Value = value,
                Unit = input.Unit,
                Timestamp = DateTime.UtcNow
            };

            return SavePacket(packet, "int");
        }

        private TelemetryRecord ProcessBool(TelemetryInput input)
        {
            if (!bool.TryParse(input.Value, out var value))
            {
                throw new ArgumentException(
                    "Boolean value must be true or false."
                );
            }

            var packet = new TelemetryPacket<bool>
            {
                DeviceId = input.DeviceId,
                Metric = input.Metric,
                Value = value,
                Unit = input.Unit,
                Timestamp = DateTime.UtcNow
            };

            return SavePacket(packet, "bool");
        }

        private TelemetryRecord SavePacket<T>(
            TelemetryPacket<T> packet,
            string dataType)
        {
            var record = new TelemetryRecord
            {
                DeviceId = packet.DeviceId,
                Metric = packet.Metric,
                DataType = dataType,
                DisplayValue = packet.Value?.ToString() ?? "",
                Unit = packet.Unit,
                Timestamp = packet.Timestamp
            };

            _store.Telemetry.Add(record);

            return record;
        }

        public List<double> ProcessHistoricalBatches(double[][] batches)
        {
            var readings = new List<double>();

            foreach (var batch in batches)
            {
                readings.AddRange(batch);
            }

            return readings;
        }
    }
}

//Reference list
//aartig13 (2022). Implement custom collections in visual C# - C#. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/implement-custom-collection [Accessed 14 Sept. 2026].
//BillWagner (2023). Collections - C#. [online] learn.microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/collections [Accessed 14 Sept. 2026].
//tdykstra (2026). ASP.NET core data protection overview. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].