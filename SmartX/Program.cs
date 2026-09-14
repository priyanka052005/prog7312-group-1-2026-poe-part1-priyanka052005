//The application configuration in Program.cs was developed with assistance from
//Microsoft Learn documentation was used to guide ASP.NET Core application configuration,
//service registration, controller mapping, static file handling and Data Protection

using SmartX.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDataProtection();
builder.Services.AddSingleton<SmartXStore>();
builder.Services.AddSingleton<TelemetryService>();
builder.Services.AddSingleton<MockTelemetryGenerator>();
builder.Services.AddSingleton<FileEncryptionService>();

var app = builder.Build();
Console.WriteLine(
    "SMART-X RUNNING FROM: " +
    app.Environment.ContentRootPath
);

app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = context =>
    {
        context.Context.Response.Headers["Cache-Control"] =
            "no-cache, no-store, must-revalidate";

        context.Context.Response.Headers["Pragma"] =
            "no-cache";

        context.Context.Response.Headers["Expires"] =
            "0";
    }
});

Directory.CreateDirectory(
    Path.Combine(
        app.Environment.ContentRootPath,
        "Uploads"
    )
);

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();

//Reference List
//tdykstra (2026b). ASP.NET core fundamentals overview. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].