//The Smart-X data models were developed specifically for the requirements of the project with assistance
//from Microsoft documentation. It was consulted for model structure and generic programming.
//In particular, the generic TelemetryPacket<T> implementation was informed by Microsoft's guidance on
//generic types and methods

namespace SmartX.Models
{
    public class DeploymentNode
    {
        public string Name { get; set; } = "";

        public string NodeType { get; set; } = "";

        public bool IsConfigured { get; set; }

        public DeploymentNode? Child { get; set; }

        public static DeploymentNode CreateDemoHierarchy()
        {
            return new DeploymentNode
            {
                Name = "Facility A",
                NodeType = "Facility",
                IsConfigured = true,

                Child = new DeploymentNode
                {
                    Name = "Zone 1",
                    NodeType = "Zone",
                    IsConfigured = true,

                    Child = new DeploymentNode
                    {
                        Name = "Sub-Zone B",
                        NodeType = "Sub-Zone",
                        IsConfigured = true,

                        Child = new DeploymentNode
                        {
                            Name = "ESP32-A1",
                            NodeType = "Sensor",
                            IsConfigured = true
                        }
                    }
                }
            };
        }
    }
}

//Reference list
//BillWagner (2026). Generic types and methods - C#. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics [Accessed 14 Sept. 2026].
//jekelly (2025). Create models for your app - visual studio (windows). [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/visualstudio/modeling/create-models-for-your-app?view=visualstudio [Accessed 14 Sept. 2026].