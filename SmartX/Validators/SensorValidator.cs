//The Smart-X validators were developed specifically for the application's sensor,
//telemetry and deployment requirements with assistance from Micosoft learn.
//The validation logic was structured to separate validation responsibilities from the
//API controllers, following the overall ASP.NET Core application structure described
//in Microsoft documentation

using System.Text.RegularExpressions;
using SmartX.Models;

namespace SmartX.Validators
{
    public static class SensorValidator
    {
        public static List<string> Validate(SensorRegistration sensor)
        {
            var errors = new List<string>();

            var deviceId = sensor.DeviceId ?? "";
            var location = sensor.Location ?? "";
            var category = sensor.Category ?? "";

            if (string.IsNullOrWhiteSpace(deviceId))
            {
                errors.Add("Device ID / MAC address is required.");
            }
            else
            {
                if (deviceId.Length < 3 || deviceId.Length > 40)
                {
                    errors.Add("Device ID must be between 3 and 40 characters.");
                }

                if (!Regex.IsMatch(
                    deviceId,
                    @"^[A-Za-z0-9:_\-\.]+$"))
                {
                    errors.Add("Device ID contains invalid characters.");
                }
            }

            if (string.IsNullOrWhiteSpace(location))
            {
                errors.Add("Deployment location is required.");
            }

            var allowedCategories = new[]
            {
                "Environmental",
                "Power Consumption",
                "Actuator"
            };

            if (!allowedCategories.Contains(category))
            {
                errors.Add("Choose a valid sensor category.");
            }

            return errors;
        }
    }
}

//Reference list
//ardalis (2026). Handle requests with controllers in ASP.NET core MVC. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/actions?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].