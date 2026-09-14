# Smart-X IoT Data Ingestion and Validation Gateway

## Project Overview

Smart-X is an IoT Data Ingestion and Validation Gateway developed for Part 1 of the PROG7312 POE.

The application simulates an IoT ecosystem containing ESP32 style sensors and allows developers to register sensors, submit telemetry, generate mock telemetry, monitor sensor activity, identify abnormal readings and upload sensor related files.

The application consists of a .NET 10 ASP.NET Core backend API and an HTML, CSS and JavaScript frontend.

---

## Technologies Used

- C#
- .NET 10
- ASP.NET Core Web API
- HTML
- CSS
- JavaScript
- Visual Studio 2022
- Git
- GitHub

---

## Part 1 Features

The Smart-X Part 1 application provides:

- Sensor registration
- Unique sensor/device identification
- Deployment location management
- Sensor categories
- Telemetry submission through an API
- Support for multiple telemetry data types
- Mock telemetry generation
- Telemetry history
- Live anomaly and disconnect alerts
- Recommended actions for abnormal telemetry
- Multipart sensor file uploads
- Protected file storage
- Generic programming
- Operator overloading
- Jagged array processing
- Generic List collections
- Recursive validation
- Custom generic telemetry buffer

---

# Project Structure

## Controllers

Contains the ASP.NET Core API controllers responsible for handling requests for:

- Sensors
- Telemetry
- Alerts
- Sensor attachments
- Demonstrations
- API health/status
(ardalis, 2026b)

## Models

Contains the data structures used by Smart-X, including sensor registrations, telemetry packets, telemetry records, alerts and deployment nodes.

The generic `TelemetryPacket<T>` allows different telemetry data types to be processed using the same reusable structure.
(BillWagner, 2026)

## Services

Contains the main application logic, including:

- Telemetry processing
- Mock telemetry generation
- Alert detection
- Application data storage
- File protection
- Custom telemetry collection management
  (ardalis, 2026b)
  
## Validators

Contains validation logic for:

- Sensors
- Telemetry
- Deployment structures
  (tdykstra, 2026b)

## wwwroot

Contains the frontend application:

- `index.html`
- `styles.css`
- `app.js`
- Smart-X logo/assets
(GeeksforGeeks, 2023)
---

# Prerequisites

Before running Smart-X, ensure the following software is installed:

- Visual Studio 2022
- .NET 10 SDK
- Git

The ASP.NET and web development workload should also be installed in Visual Studio.

---

# Setup Instructions
## 1. Clone the Repository

Open Command Prompt or PowerShell and run:

```bash
git clone https://github.com/priyanka052005/prog7312-group-1-2026-poe-part1-priyanka052005.git

---

# **References**
aartig13 (2022). Implement custom collections in visual C# - C#. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/implement-custom-collection [Accessed 14 Sept. 2026].

ardalis (2026a). Handle requests with controllers in ASP.NET core MVC. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/actions?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].

ardalis (2026b). Handle requests with controllers in ASP.NET core MVC. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/actions?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].

BillWagner (2023). Collections - C#. [online] learn.microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/collections [Accessed 14 Sept. 2026].

BillWagner (2026). Generic types and methods - C#. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics [Accessed 14 Sept. 2026].

GeeksforGeeks (2023). CSS tutorial. [online] GeeksforGeeks. Available at: https://www.geeksforgeeks.org/css/css-tutorial/ [Accessed 14 Sept. 2026].

jekelly (2025). Create models for your app - visual studio (windows). [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/visualstudio/modeling/create-models-for-your-app?view=visualstudio [Accessed 14 Sept. 2026].

Microsoft.com. (2024). How to create simple HTML/CSS project in visual studio 2022 - Microsoft Q&A. [online] Available at: https://learn.microsoft.com/en-us/answers/questions/1663914/how-to-create-simple-html-css-project-in-visual-st [Accessed 14 Sept. 2026].

RoseHJM (2026). JavaScript and TypeScript in visual studio. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/visualstudio/javascript/javascript-in-visual-studio?view=visualstudio [Accessed 14 Sept. 2026].

tdykstra (2026a). ASP.NET core data protection overview. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].

tdykstra (2026b). ASP.NET core fundamentals overview. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].

tdykstra (2026c). Upload files in ASP.NET CORE. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].

W3SCHOOLS (2019). CSS tutorial. [online] W3schools.com. Available at: https://www.w3schools.com/css/ [Accessed 14 Sept. 2026].

W3Schools (2024). HTML tutorial. [online] W3schools.com. Available at: https://www.w3schools.com/html/ [Accessed 14 Sept. 2026].

wadepickett (2025). Static files in ASP.NET core. [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].
