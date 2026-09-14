//The Smart-X service classes were developed with assistance from Mircosoft Learn for
//telemetry processing, mock telemetry generation, application storage, alert generation
//and file protection. The custom TelemetryBuffer<T> was informed by Microsoft documentation
//on C# collections and implementing custom collections.
//The file encryption/protection functionality was informed by ASP.NET Core Data
//Protection documentation

using System.Collections;

namespace SmartX.Services
{
    public class TelemetryBuffer<T> : IEnumerable<T>
    {
        private readonly T[] _items;
        private int _start;
        private int _count;

        public int Count => _count;
        public int Capacity => _items.Length;

        public TelemetryBuffer(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException(
                    "Capacity must be greater than zero."
                );
            }

            _items = new T[capacity];
        }

        public void Add(T item)
        {
            if (_count < Capacity)
            {
                var index =
                    (_start + _count) % Capacity;

                _items[index] = item;

                _count++;
            }
            else
            {
                _items[_start] = item;

                _start =
                    (_start + 1) % Capacity;
            }
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                var index =
                    (_start + i) % Capacity;

                yield return _items[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

//Reference list
//aartig13 (2022). Implement custom collections in visual C# - C#. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/implement-custom-collection [Accessed 14 Sept. 2026].
//BillWagner (2023). Collections - C#. [online] learn.microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/collections [Accessed 14 Sept. 2026].
//tdykstra (2026). ASP.NET core data protection overview. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].