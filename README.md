<div align="center">

# 🏥 MediSync
### Healthcare Management System

![.NET](https://img.shields.io/badge/.NET_8-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Angular](https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)
![Azure DevOps](https://img.shields.io/badge/Azure_DevOps-0078D7?style=for-the-badge&logo=azuredevops&logoColor=white)

*A full-stack healthcare platform for managing patients, doctors, appointments and medical records.*

</div>

---

## ✨ Features

- 🔐 **JWT Authentication** with role-based access control (Admin / Doctor / Patient)
- 👨‍⚕️ **Doctor Management** — profiles, specializations, availability
- 🧑‍🤝‍🧑 **Patient Management** — registration, medical history, records
- 📅 **Appointment Booking** — real-time status tracking and management
- 🗂️ **Medical Records** — diagnosis, prescriptions, patient history
- 📊 **Dashboard Analytics** — live stats, top doctors, recent appointments
- 🏗️ **Layered Architecture** — Controllers, Services, DTOs, Models
- 🐳 **Docker Ready** — containerized for easy deployment

---

## 🛠️ Tech Stack

| Layer | Technology |
|-------|-----------|
| **Backend** | C# · ASP.NET Core 8 · Web API |
| **ORM** | Entity Framework Core 8 |
| **Database** | SQL Server |
| **Auth** | JWT · RBAC |
| **Frontend** | Angular (coming soon) |
| **DevOps** | Azure DevOps · Docker · GitHub Actions |
| **Tools** | Swagger · Postman |

---

## 📡 API Endpoints

<details>
<summary>🔐 Auth</summary>

| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| `POST` | `/api/auth/register` | Public | Register new user |
| `POST` | `/api/auth/login` | Public | Login — returns JWT token |

</details>

<details>
<summary>🧑‍🤝‍🧑 Patient</summary>

| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| `GET` | `/api/patient` | Admin, Doctor | Get all patients |
| `GET` | `/api/patient/{id}` | Admin, Doctor | Get patient by ID |
| `POST` | `/api/patient` | Admin, Doctor | Register new patient |
| `PUT` | `/api/patient/{id}` | Admin, Doctor | Update patient |
| `DELETE` | `/api/patient/{id}` | Admin | Delete patient |

</details>

<details>
<summary>👨‍⚕️ Doctor</summary>

| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| `GET` | `/api/doctor` | Public | Get all doctors |
| `GET` | `/api/doctor/{id}` | Public | Get doctor by ID |
| `POST` | `/api/doctor` | Admin | Add new doctor |
| `PUT` | `/api/doctor/{id}` | Admin | Update doctor |
| `DELETE` | `/api/doctor/{id}` | Admin | Delete doctor |

</details>

<details>
<summary>📅 Appointment</summary>

| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| `GET` | `/api/appointment` | Admin, Doctor | Get all appointments |
| `GET` | `/api/appointment/{id}` | Admin, Doctor, Patient | Get by ID |
| `GET` | `/api/appointment/patient/{id}` | Admin, Doctor, Patient | Get by patient |
| `GET` | `/api/appointment/doctor/{id}` | Admin, Doctor | Get by doctor |
| `POST` | `/api/appointment` | Admin, Doctor, Patient | Book appointment |
| `PUT` | `/api/appointment/{id}/status` | Admin, Doctor | Update status |
| `DELETE` | `/api/appointment/{id}` | Admin | Delete appointment |

</details>

<details>
<summary>🗂️ Medical Records</summary>

| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| `GET` | `/api/medicalrecord` | Admin, Doctor | Get all records |
| `GET` | `/api/medicalrecord/{id}` | Admin, Doctor, Patient | Get by ID |
| `GET` | `/api/medicalrecord/patient/{id}` | Admin, Doctor, Patient | Get by patient |
| `POST` | `/api/medicalrecord` | Admin, Doctor | Add new record |
| `PUT` | `/api/medicalrecord/{id}` | Admin, Doctor | Update record |
| `DELETE` | `/api/medicalrecord/{id}` | Admin | Delete record |

</details>

<details>
<summary>📊 Dashboard</summary>

| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| `GET` | `/api/dashboard/stats` | Admin, Doctor | Get system analytics |

</details>

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server
- Node.js 18+ *(for Angular frontend)*

### Backend Setup
```bash
# Clone the repo
git clone https://github.com/harshada-dotnet/Medisync.git
cd Medisync

# Update connection string in appsettings.json
# "Server=YOUR_SERVER;Database=MediSyncDB;..."

# Run migrations
dotnet ef database update

# Start the API
dotnet run --project MediSync.API
```

API runs at: `https://localhost:PORT`
Swagger UI: `https://localhost:PORT/` *(root URL)*

### Default Roles
```
Admin   → full access to everything
Doctor  → manage patients, appointments, medical records
Patient → view own appointments and medical records
```

---

## 📁 Project Structure

```
MediSync/
└── MediSync.API/
    ├── Controllers/       → API endpoints
    ├── Services/          → Business logic
    │   └── Interfaces/    → Service contracts
    ├── Models/            → Database entities
    │   ├── Auth/
    │   ├── Patient/
    │   ├── Doctor/
    │   └── Appointment/
    ├── DTOs/              → Request & Response objects
    │   ├── Auth/
    │   ├── Patient/
    │   ├── Doctor/
    │   ├── Appointment/
    │   ├── MedicalRecord/
    │   └── Dashboard/
    ├── Data/              → EF Core DbContext
    ├── Helpers/           → Shared utilities
    └── Migrations/        → EF Core migrations
```

---

## 📊 Project Status

| Day | Feature | Status |
|-----|---------|--------|
| Day 1 | Auth — Register, Login, JWT | ✅ Done |
| Day 2 | Patient & Doctor CRUD APIs | ✅ Done |
| Day 3 | Appointment Booking API | ✅ Done |
| Day 4 | Medical Records API | ✅ Done |
| Day 5 | Dashboard Stats API | ✅ Done |
| Day 6 | Angular Frontend | 🔄 Coming |

---

## 👩‍💻 Author

**Harshada Patil**
Software Developer · ASP.NET Core · Angular · SQL Server

[![GitHub](https://img.shields.io/badge/GitHub-harshada--dotnet-181717?style=flat-square&logo=github&logoColor=white)](https://github.com/harshada-dotnet)
[![Email](https://img.shields.io/badge/Email-harshadasp09@gmail.com-D14836?style=flat-square&logo=gmail&logoColor=white)](mailto:harshadasp09@gmail.com)

---

<div align="center">
<i>Built with ❤️ — one commit at a time</i>
</div>
