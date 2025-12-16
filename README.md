# Wayplot Backend

## Introduction
This is the backend for WayPlot. It provides API endpoints and data management for the WayPlot platform. Main features include user management, data transfer, analytics and database integration.

## Setup Instructions
1. Install [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
2. Clone this repo
3. Configure "ConnectionStrings.Default" in `appsettings.json` with your database connection string.
4. Run 'Update-Database' to apply migrations and create the database
5. Run `dotnet restore` in the project root
6. Run `dotnet build` to build the project
7. Run `dotnet run` to start the backend server
## Folder Structure
```
Constants/         # Enums for auth, user roles, status
Controllers/       # API controllers
Database/          # DB context and connection factories
DTOs/              # Data transfer objects
Models/            # Entity models
Repositories/      # Data access layer
Services/          # Business logic layer
Properties/        # Launch settings
bin/, obj/         # Build output
```

## Technologies Used
- .NET 8
- ASP.NET Core
- C#
- SQL Server
- Entity Framework Core