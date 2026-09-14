//The Smart-X validators were developed specifically for the application's sensor,
//telemetry and deployment requirements with assistance from Micosoft learn.
//The validation logic was structured to separate validation responsibilities from the
//API controllers, following the overall ASP.NET Core application structure described
//in Microsoft documentation

using SmartX.Models;

namespace SmartX.Validators
{
    public static class DeploymentValidator
    {
        public static bool ValidatePath(DeploymentNode node)
        {
            if (node == null)
            {
                return false;
            }

            if (!node.IsConfigured || string.IsNullOrWhiteSpace(node.Name))
            {
                return false;
            }

            if (node.Child == null)
            {
                return true;
            }

            return ValidatePath(node.Child);
        }
    }
}

//Reference list
//ardalis (2026). Handle requests with controllers in ASP.NET core MVC. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/actions?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].