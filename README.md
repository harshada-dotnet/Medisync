# MediSync — Healthcare Management System

Full-stack healthcare platform built with **ASP.NET Core 8** and **Angular**.

## Tech Stack
- **Backend:** C# · ASP.NET Core 8 Web API · EF Core · SQL Server
- **Frontend:** Angular · TypeScript · Bootstrap
- **Auth:** JWT · RBAC (Admin / Doctor / Patient)
- **DevOps:** Azure DevOps · Docker

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server (local or Docker)
- Node.js 18+

### Backend Setup
```bash
cd MediSync.API
dotnet restore
dotnet ef database update
dotnet run
```

API runs at: `https://localhost:7001`  
Swagger UI: `https://localhost:7001/` (root)

## API Endpoints

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | /api/auth/register | Public | Register new user |
| POST | /api/auth/login | Public | Login, returns JWT |

## Project Structure
```
MediSync.API/
├── Controllers/     — API endpoints
├── Services/        — Business logic
├── Models/          — Database entities
├── DTOs/            — Request/Response objects
├── Data/            — DbContext
├── Helpers/         — Shared utilities
└── Middleware/      — Custom middleware
```
