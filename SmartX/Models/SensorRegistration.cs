//The Smart-X data models were developed specifically for the requirements of the project with assistance
//from Microsoft documentation. It was consulted for model structure and generic programming.
//In particular, the generic TelemetryPacket<T> implementation was informed by Microsoft's guidance on
//generic types and methods

namespace SmartX.Models
{
    public class SensorRegistration
    {
        public string DeviceId { get; set; } = "";

        public string Location { get; set; } = "";

        public string Category { get; set; } = "";

        public string Status { get; set; } = "Online";

        public DateTime CreatedAt { get; set; }
    }
}

//Reference list
//BillWagner (2026). Generic types and methods - C#. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics [Accessed 14 Sept. 2026].
//jekelly (2025). Create models for your app - visual studio (windows). [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/visualstudio/modeling/create-models-for-your-app?view=visualstudio [Accessed 14 Sept. 2026].