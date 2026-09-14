//The Smart-X service classes were developed with assistance from Mircosoft Learn for
//telemetry processing, mock telemetry generation, application storage, alert generation
//and file protection. The custom TelemetryBuffer<T> was informed by Microsoft documentation
//on C# collections and implementing custom collections.
//The file encryption/protection functionality was informed by ASP.NET Core Data
//Protection documentation

using Microsoft.AspNetCore.DataProtection;

namespace SmartX.Services
{
    public class FileEncryptionService
    {
        private readonly IDataProtector _protector;

        public FileEncryptionService(
            IDataProtectionProvider provider)
        {
            _protector =
                provider.CreateProtector(
                    "SmartX.FileEncryption"
                );
        }

        public async Task EncryptFileAsync(
            Stream inputStream,
            string outputPath)
        {
            using var memoryStream =
                new MemoryStream();

            await inputStream.CopyToAsync(
                memoryStream
            );

            var originalBytes =
                memoryStream.ToArray();

            var encryptedBytes =
                _protector.Protect(
                    originalBytes
                );

            await File.WriteAllBytesAsync(
                outputPath,
                encryptedBytes
            );
        }

        public async Task<byte[]> DecryptFileAsync(
            string filePath)
        {
            var encryptedBytes =
                await File.ReadAllBytesAsync(
                    filePath
                );

            return _protector.Unprotect(
                encryptedBytes
            );
        }
    }
}

//Reference list
//aartig13 (2022). Implement custom collections in visual C# - C#. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/implement-custom-collection [Accessed 14 Sept. 2026].
//BillWagner (2023). Collections - C#. [online] learn.microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/collections [Accessed 14 Sept. 2026].
//tdykstra (2026). ASP.NET core data protection overview. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].