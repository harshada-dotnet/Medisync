# MediSync — Healthcare Management System

<<<<<<< HEAD
Full-stack healthcare platform built with **ASP.NET Core 8** and **Angular**.

## Tech Stack
- **Backend:** C# · ASP.NET Core 8 Web API · EF Core · SQL Server
- **Frontend:** Angular · TypeScript · Bootstrap
- **Auth:** JWT · RBAC (Admin / Doctor / Patient)
- **DevOps:** Azure DevOps · Docker

=======
Full-stack healthcare platform built with ASP.NET Core 8 and Angular.

## Tech Stack
- **Backend:** C# · ASP.NET Core 8 · EF Core · SQL Server
- **Auth:** JWT · RBAC (Admin / Doctor / Patient)
- **Frontend:** Angular (coming soon)
- **DevOps:** Azure DevOps · Docker

## API Endpoints

### Auth
| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| POST | /api/auth/register | Public | Register user |
| POST | /api/auth/login | Public | Login, returns JWT |

### Patient
| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| GET | /api/patient | Admin, Doctor | Get all patients |
| GET | /api/patient/{id} | Admin, Doctor | Get patient by ID |
| POST | /api/patient | Admin, Doctor | Add new patient |
| PUT | /api/patient/{id} | Admin, Doctor | Update patient |
| DELETE | /api/patient/{id} | Admin | Delete patient |

### Doctor
| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| GET | /api/doctor | Public | Get all doctors |
| GET | /api/doctor/{id} | Public | Get doctor by ID |
| POST | /api/doctor | Admin | Add new doctor |
| PUT | /api/doctor/{id} | Admin | Update doctor |
| DELETE | /api/doctor/{id} | Admin | Delete doctor |

>>>>>>> 7dab3ea242e2a0f016ac0bf361223eca4d5b40ca
## Getting Started

### Prerequisites
- .NET 8 SDK
<<<<<<< HEAD
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
=======
- SQL Server

### Setup
```bash
# Clone the repo
git clone https://github.com/harshada-dotnet/Medisync.git

# Update connection string in appsettings.json

# Run migrations
dotnet ef database update

# Run the API
dotnet run
```

## Project Status
- ✅ Day 1 — Auth (Register, Login, JWT)
- ✅ Day 2 — Patient & Doctor CRUD APIs
- 🔄 Day 3 — Appointment Booking (coming)
- ⬜ Day 4 — Medical Records
- ⬜ Day 5 — Angular Frontend
>>>>>>> 7dab3ea242e2a0f016ac0bf361223eca4d5b40ca
