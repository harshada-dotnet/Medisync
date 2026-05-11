# MediSync — Healthcare Management System

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

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server

### Setup
```bash
git clone https://github.com/harshada-dotnet/Medisync.git
cd Medisync
dotnet ef database update
dotnet run
```

## Project Status
- ✅ Day 1 — Auth (Register, Login, JWT)
- ✅ Day 2 — Patient & Doctor CRUD APIs
- ✅ Day 3 — Appointment Booking (coming)
- ✅ Day 4 — Medical Records
- 🔄 Day 5 — Angular Frontend
