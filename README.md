# ClickFighter

ClickFighter is a multiplayer click-based game where two players compete in real time. Each match consists of a 60-second round, and the player with the highest number of clicks wins.

The project consists of an Android application and an ASP.NET Core backend connected to a SQL Server database.

---

## Table of Contents

- [Project Architecture](#project-architecture)
- [Repository Structure](#repository-structure)
- [Prerequisites](#prerequisites)
- [Backend Setup](#backend-setup)
  - [1. Database Setup](#1-database-setup)
  - [2. Connection String Configuration](#2-connection-string-configuration)
  - [3. Restore Dependencies](#3-restore-dependencies)
  - [4. Build the Solution](#4-build-the-solution)
  - [5. Run the Server](#5-run-the-server)
  - [6. Verify the Database Connection](#6-verify-the-database-connection)
- [Client Setup](#client-setup)
- [Client-Server Communication](#client-server-communication)
- [Developer Configuration](#developer-configuration)
- [Current Development Status](#current-development-status)

---

## Project Architecture

The project follows a layered backend architecture:

- **Domain**: Contains the core business entities and domain logic.
- **Application**: Contains application use cases and business workflows.
- **Infrastructure**: Handles external concerns such as database access and persistence through Entity Framework Core.
- **Server**: ASP.NET Core Web API entry point. Configures dependency injection, HTTP endpoints, and application startup.
- **Client**: Android application built with Kotlin and Jetpack Compose.

The backend uses:

- **ASP.NET Core** for the Web API.
- **Entity Framework Core** as the ORM.
- **SQL Server** for persistent storage.
- **SignalR** (planned) for real-time client-server communication.

REST endpoints will be used for standard API operations, while SignalR will handle real-time game events.

---

## Repository Structure

```text
ClickFighter/
├── Client/
│   └── ...                         # Android application
│
└── Server/
    ├── clickFighter/
    │   ├── Controllers/
    │   ├── Properties/
    │   ├── Program.cs
    │   ├── appsettings.json
    │   ├── appsettings.Development.json
    │   └── clickFighter.server.csproj
    │
    ├── clickFighter.Application/
    │   └── clickFighter.Application.csproj
    │
    ├── clickFighter.Infrastructure/
    │   ├── DataBase/
    │   │   └── AppDbContext.cs
    │   ├── DependencyInjection.cs
    │   └── clickFighter.Infrastructure.csproj
    │
    ├── clickFigther.Domain/
    │   └── clickFigther.Domain.csproj
    │
    └── clickFighter.sln
```

---

## Prerequisites

Install the following tools before setting up the project.

| Tool | Purpose |
|---|---|
| [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) | Build and run the backend |
| [SQL Server Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) | Local database server |
| [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/ssms/install/install) | Database administration |
| [Android Studio](https://developer.android.com/studio) | Android client development |

The backend currently targets **.NET 10**.

---

## Backend Setup

All commands in this section assume that the terminal is opened at the repository root.

### 1. Database Setup

Install and start SQL Server Developer Edition.

Using SQL Server Management Studio (SSMS):

1. Connect to your local SQL Server instance using Windows Authentication.
2. Create a database named `ClickFighterDB`.
3. Ensure that the SQL Server service is running.

SSMS is only an administration tool. The SQL Server service must remain running independently for the application to access the database.

### 2. Connection String Configuration

The backend uses Entity Framework Core with the SQL Server provider.

The local development connection string is configured in:

`Server/clickFighter/appsettings.Development.json`

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ClickFighterDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True"
  }
}
```

Connection string parameters:

| Parameter | Description |
|---|---|
| `Server=localhost` | Local SQL Server instance |
| `Database=ClickFighterDB` | Target database |
| `Trusted_Connection=True` | Uses Windows Authentication |
| `TrustServerCertificate=True` | Allows the local development connection without certificate validation |
| `MultipleActiveResultSets=True` | Enables multiple active result sets on a connection |

**Developer-specific configuration:**

Each developer must configure their own local connection string according to their SQL Server instance and authentication method.

Do not commit personal credentials or production connection strings to the repository.

### 3. Restore Dependencies

From the repository root:

```powershell
dotnet restore .\Server\clickFighter\clickFighter.sln
```

This restores the NuGet dependencies required by all backend projects.

### 4. Build the Solution

From the repository root:

```powershell
dotnet build .\Server\clickFighter\clickFighter.sln
```

A successful build should compile all four backend projects:

- `clickFigther.Domain`
- `clickFighter.Application`
- `clickFighter.Infrastructure`
- `clickFighter.server`

### 5. Run the Server

Navigate to the ASP.NET Core project directory:

```powershell
cd .\Server\clickFighter
```

Start the server:

```powershell
dotnet run
```

The application will display its listening address in the terminal. For example:

```text
Now listening on: http://localhost:5048
Hosting environment: Development
```

The port may vary depending on the local launch configuration.

Keep the terminal open while using the server. Press `Ctrl+C` to stop it.

### 6. Verify the Database Connection

A temporary health endpoint is available to verify that the backend can connect to SQL Server through Entity Framework Core.

With the server running, open the following URL in a browser:

```text
http://localhost:5048/api/health/database
```

Replace `5048` with the port displayed by your application.

Expected successful response:

```json
{
  "status": "Connected"
}
```

This verifies that:

1. ASP.NET Core successfully starts.
2. The `AppDbContext` is correctly registered through dependency injection.
3. Entity Framework Core can communicate with SQL Server.
4. The configured database is reachable.

If the connection fails, verify that the SQL Server service is running, that the database exists, and that the connection string matches your local SQL Server configuration.

> **Note:** This endpoint is currently intended for development and verification. It should not be exposed as an unrestricted production diagnostic endpoint.

---

## Client Setup

**Status: Pending**

The Android application is developed using Kotlin and Jetpack Compose.

This section will be completed once the client configuration is finalized.

Planned documentation:

- Android Studio setup.
- Required Android SDK and JDK versions.
- Gradle dependency configuration.
- Emulator and physical device setup.
- Application build and launch instructions.
- Environment-specific client configuration.

### Client Environment Configuration

To be documented:

| Configuration | Description |
|---|---|
| Backend base URL | Address used by the Android application to reach the API |
| Development server address | Address accessible from the Android emulator or physical device |
| SignalR hub URL | Real-time communication endpoint |
| Environment | Development or production configuration |

---

## Client-Server Communication

**Status: Pending**

The client-server communication layer will be documented once implemented.

The planned communication architecture is:

| Technology | Responsibility |
|---|---|
| REST API (HTTP) | Player identity, statistics, and match history |
| SignalR | Matchmaking, game events, and real-time updates |
| SQL Server | Persistent player and match data |

The final documentation will include:

- API base URL configuration.
- SignalR hub endpoint.
- Authentication and connection lifecycle.
- Local network configuration for physical Android devices.
- Emulator networking considerations.
- CORS and HTTP/HTTPS development configuration.

---

## Developer Configuration

### Backend Projects and Dependencies

The backend is organized into four projects with the following dependency direction:

```text
Server
  ├── Application
  └── Infrastructure

Infrastructure
  ├── Application
  └── Domain

Application
  └── Domain

Domain
  └── No project dependencies
```

Entity Framework Core and the SQL Server provider are used in the Infrastructure layer. Database access should remain outside the Domain and Application layers.

### Local Development Files

The following files may contain developer-specific settings:

| File | Purpose |
|---|---|
| `appsettings.json` | Shared application configuration |
| `appsettings.Development.json` | Local development configuration |
| `Properties/launchSettings.json` | Local launch profiles and server URLs |

Avoid committing passwords, access tokens, or other secrets.

### Useful Commands

Run these commands from the repository root unless otherwise specified.

| Command | Purpose |
|---|---|
| `dotnet restore .\Server\clickFighter\clickFighter.sln` | Restore backend dependencies |
| `dotnet build .\Server\clickFighter\clickFighter.sln` | Build the backend solution |
| `dotnet run` | Start the server from its project directory |
| `dotnet clean .\Server\clickFighter\clickFighter.sln` | Clean build artifacts |

---

## Current Development Status

| Component | Status |
|---|---|
| Backend solution and project structure | Implemented |
| .NET 10 configuration | Implemented |
| Entity Framework Core integration | Implemented |
| SQL Server connection | Verified locally |
| Android client foundation | In progress |
| Client-server communication | Planned |
| Domain entities and business logic | Pending |
| Matchmaking and real-time gameplay | Planned |
| Persistent match history | Planned |

