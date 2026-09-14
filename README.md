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


