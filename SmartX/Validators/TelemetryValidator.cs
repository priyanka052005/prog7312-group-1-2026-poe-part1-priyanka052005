//The Smart-X validators were developed specifically for the application's sensor,
//telemetry and deployment requirements with assistance from Micosoft learn.
//The validation logic was structured to separate validation responsibilities from the
//API controllers, following the overall ASP.NET Core application structure described
//in Microsoft documentation

using SmartX.Models;

namespace SmartX.Validators
{
    public static class TelemetryValidator
    {
        public static List<string> Validate(TelemetryInput input)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(input.DeviceId))
            {
                errors.Add("Device ID is required.");
            }

            if (string.IsNullOrWhiteSpace(input.Metric))
            {
                errors.Add("Metric name is required.");
            }

            if (input.Metric.Length > 50)
            {
                errors.Add("Metric name must be 50 characters or fewer.");
            }

            var validTypes = new[]
            {
                "double",
                "float",
                "int",
                "integer",
                "bool",
                "boolean"
            };

            if (string.IsNullOrWhiteSpace(input.DataType) ||
                !validTypes.Contains(input.DataType.ToLowerInvariant()))
            {
                errors.Add("Data type must be double, int, or bool.");
            }

            if (string.IsNullOrWhiteSpace(input.Value))
            {
                errors.Add("Telemetry value is required.");
            }

            if (input.Unit.Length > 20)
            {
                errors.Add("Unit must be 20 characters or fewer.");
            }

            return errors;
        }
    }
}

//Reference list
//ardalis (2026). Handle requests with controllers in ASP.NET core MVC. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/actions?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].