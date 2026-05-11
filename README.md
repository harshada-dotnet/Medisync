<div align="center">

<img src="https://capsule-render.vercel.app/api?type=rect&color=gradient&customColorList=2,3,30&height=120&text=🏥%20MediSync&fontSize=50&fontColor=ffffff&desc=Healthcare%20Management%20System%20API&descSize=18&descAlignY=75" width="100%"/>

![.NET](https://img.shields.io/badge/.NET_8-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![Angular](https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

![Status](https://img.shields.io/badge/Status-Active%20Development-22c55e?style=flat-square)
![Backend](https://img.shields.io/badge/Backend-Complete-22c55e?style=flat-square)
![Frontend](https://img.shields.io/badge/Frontend-In%20Progress-f59e0b?style=flat-square)
![License](https://img.shields.io/badge/License-MIT-38bdf8?style=flat-square)

</div>

---

## 🧬 What is MediSync?

**MediSync** is a production-grade REST API for healthcare management built with **ASP.NET Core 8**. It provides a complete backend solution for hospitals and clinics to manage patients, doctors, appointments, and medical records with secure role-based access.

```
🔐  JWT Authentication    →  Secure token-based login
👥  RBAC                  →  Admin · Doctor · Patient roles
🧑‍🤝‍🧑  Patient Management   →  Profiles, history, soft delete
👨‍⚕️  Doctor Management    →  Profiles, specialization, availability
📅  Appointments          →  Booking, conflict prevention, status tracking
🗂️  Medical Records       →  Diagnosis, prescriptions, patient history
📊  Dashboard Analytics   →  Live stats, top doctors, recent activity
📄  Swagger UI            →  Full API docs with JWT support
```

---

## 🛠️ Tech Stack

<div align="center">

<a href="https://skillicons.dev"><img src="https://skillicons.dev/icons?i=cs,dotnet,angular,typescript,html,css"/></a>
<br/>
<a href="https://skillicons.dev"><img src="https://skillicons.dev/icons?i=azure,docker,git,github,vscode,postman"/></a>

</div>

<br/>

| Layer | Technology |
|-------|-----------|
| **Language** | C# 12 |
| **Framework** | ASP.NET Core 8 Web API |
| **ORM** | Entity Framework Core 8 |
| **Database** | SQL Server |
| **Auth** | JWT Bearer · BCrypt · RBAC |
| **Frontend** | Angular 17 *(in progress)* |
| **DevOps** | Azure DevOps · Docker · CI/CD |
| **Docs** | Swagger / OpenAPI |

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server
- Node.js 18+ *(Angular frontend)*

### Setup

```bash
# 1. Clone the repo
git clone https://github.com/harshada-dotnet/Medisync.git
cd Medisync/MediSync.API

# 2. Update appsettings.json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=MediSyncDB;..."
}

# 3. Run migrations
dotnet ef database update

# 4. Start API
dotnet run
```

> Swagger UI opens automatically at `https://localhost:PORT`

---

## 📡 API Reference

<details>
<summary><b>🔐 Auth — Register & Login</b></summary>
<br/>

| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| `POST` | `/api/auth/register` | Public | Register — Admin / Doctor / Patient |
| `POST` | `/api/auth/login` | Public | Login — returns JWT token |

**Register body:**
```json
{
  "fullName": "Harshada Patil",
  "email": "harshada@gmail.com",
  "password": "Pass@123",
  "role": "Patient",
  "phone": "9503591731"
}
```

</details>

<details>
<summary><b>🧑‍🤝‍🧑 Patient CRUD</b></summary>
<br/>

| Method | Endpoint | Access |
|--------|----------|--------|
| `GET` | `/api/patient` | Admin, Doctor |
| `GET` | `/api/patient/{id}` | Admin, Doctor |
| `POST` | `/api/patient` | Admin, Doctor |
| `PUT` | `/api/patient/{id}` | Admin, Doctor |
| `DELETE` | `/api/patient/{id}` | Admin only |

</details>

<details>
<summary><b>👨‍⚕️ Doctor CRUD</b></summary>
<br/>

| Method | Endpoint | Access |
|--------|----------|--------|
| `GET` | `/api/doctor` | Public |
| `GET` | `/api/doctor/{id}` | Public |
| `POST` | `/api/doctor` | Admin only |
| `PUT` | `/api/doctor/{id}` | Admin only |
| `DELETE` | `/api/doctor/{id}` | Admin only |

</details>

<details>
<summary><b>📅 Appointment Booking</b></summary>
<br/>

| Method | Endpoint | Access |
|--------|----------|--------|
| `GET` | `/api/appointment` | Admin, Doctor |
| `GET` | `/api/appointment/{id}` | All roles |
| `GET` | `/api/appointment/patient/{id}` | All roles |
| `GET` | `/api/appointment/doctor/{id}` | Admin, Doctor |
| `POST` | `/api/appointment` | All roles |
| `PUT` | `/api/appointment/{id}/status` | Admin, Doctor |
| `DELETE` | `/api/appointment/{id}` | Admin only |

**Status values:** `Pending` → `Confirmed` → `Completed` / `Cancelled`

</details>

<details>
<summary><b>🗂️ Medical Records</b></summary>
<br/>

| Method | Endpoint | Access |
|--------|----------|--------|
| `GET` | `/api/medicalrecord` | Admin, Doctor |
| `GET` | `/api/medicalrecord/{id}` | All roles |
| `GET` | `/api/medicalrecord/patient/{id}` | All roles |
| `POST` | `/api/medicalrecord` | Admin, Doctor |
| `PUT` | `/api/medicalrecord/{id}` | Admin, Doctor |
| `DELETE` | `/api/medicalrecord/{id}` | Admin only |

</details>

<details>
<summary><b>📊 Dashboard Analytics</b></summary>
<br/>

| Method | Endpoint | Access | Returns |
|--------|----------|--------|---------|
| `GET` | `/api/dashboard/stats` | Admin, Doctor | Total counts, today's appointments, top doctors, recent activity |

</details>

---

## 🔒 Roles & Permissions

| Permission | 👑 Admin | 👨‍⚕️ Doctor | 🧑 Patient |
|------------|---------|--------|---------|
| Manage doctors | ✅ | ❌ | ❌ |
| Manage patients | ✅ | ✅ | ❌ |
| Book appointments | ✅ | ✅ | ✅ |
| Update appt status | ✅ | ✅ | ❌ |
| View medical records | ✅ | ✅ | own only |
| Add medical records | ✅ | ✅ | ❌ |
| View dashboard | ✅ | ✅ | ❌ |
| Delete records | ✅ | ❌ | ❌ |

---

## 📁 Project Structure

```
MediSync.API/
├── Controllers/
│   ├── AuthController.cs
│   ├── PatientController.cs
│   ├── DoctorController.cs
│   ├── AppointmentController.cs
│   ├── MedicalRecordController.cs
│   └── DashboardController.cs
│
├── Services/
│   ├── AuthService.cs
│   ├── JwtService.cs
│   ├── PatientService.cs
│   ├── DoctorService.cs
│   ├── AppointmentService.cs
│   ├── MedicalRecordService.cs
│   ├── DashboardService.cs
│   └── Interfaces/
│
├── Models/
│   ├── Auth/        ApplicationUser.cs
│   ├── Patient/     PatientModel.cs · MedicalRecord.cs
│   ├── Doctor/      DoctorModel.cs
│   └── Appointment/ AppointmentModel.cs
│
├── DTOs/
│   ├── Auth/ · Patient/ · Doctor/
│   ├── Appointment/ · MedicalRecord/ · Dashboard/
│
├── Data/            ApplicationDbContext.cs
├── Helpers/         ApiResponse.cs
└── Migrations/
```

---

## 📊 Build Progress

| Day | Feature | Status |
|-----|---------|--------|
| **Day 1** | Auth — Register, Login, JWT | ✅ Complete |
| **Day 2** | Patient & Doctor CRUD APIs | ✅ Complete |
| **Day 3** | Appointment Booking API | ✅ Complete |
| **Day 4** | Medical Records API | ✅ Complete |
| **Day 5** | Dashboard Stats API | ✅ Complete |
| **Day 6** | Angular Frontend | 🔄 In Progress |

---

<div align="center">

**Built by [Harshada Patil](https://github.com/harshada-dotnet)**

[![GitHub](https://img.shields.io/badge/GitHub-harshada--dotnet-181717?style=flat-square&logo=github&logoColor=white)](https://github.com/harshada-dotnet)
[![Email](https://img.shields.io/badge/Email-harshadasp09@gmail.com-D14836?style=flat-square&logo=gmail&logoColor=white)](mailto:harshadasp09@gmail.com)

<img src="https://capsule-render.vercel.app/api?type=waving&color=gradient&customColorList=2,3,30&height=80&section=footer" width="100%"/>

</div>
