
# ViroCure Application

## Overview
The ViroCure Application is a web-based system for managing patient information, including their associated viruses and treatments. It consists of a **Backend (BE)** built with ASP.NET Core Web API and a **Frontend (FE)** using ASP.NET Core Razor Pages. The backend follows a **3-layer architecture** (Presentation, Business Logic, Data Access) and uses a **Generic Repository** pattern for data operations. The database is created using a DB-first approach with Entity Framework Core.

### Features
- **Authentication**: JWT-based login using email and password.
- **Authorization**:
  - **Doctor (Role=3)**: Full CRUD operations and search.
  - **Admin (Role=1)**: Search only.
  - **Patient (Role=2)**: View own information.
- **CRUD Operations**: Manage persons and their associated viruses (create, read, update, delete).
- **Validation**:
  - FullName: Capitalized words, allows a-z, A-Z, 0-9, @, #, space.
  - Birthday: Must be before 01-01-2007.
  - Phone: Format `+84989xxxxxx`.
  - Resistance Rate: Between 0 and 1.
- **Frontend**: Role-based UI with Razor Pages for login, CRUD, and search.
- **Database**: SQL Server (`ViroCureFAL2024DB`) with tables for users, persons, viruses, and person-virus relationships.

## Project Structure

### Backend
- **Solution**: `PE_PRN231_FA24_TrialTest_StudentFullname_BE`
- **Projects**:
  - **ViroCureAPI**: Presentation Layer (ASP.NET Core Web API, RESTful, JWT, CORS).
  - **ViroCureBLL**: Business Logic Layer (Class Library, handles validations and business rules).
  - **ViroCureDAL**: Data Access Layer (Class Library, EF Core, Generic Repository).
- **Architecture**:
  - **Presentation Layer**: Exposes RESTful endpoints (`/api/login`, `/api/person`, `/api/persons`).
  - **Business Logic Layer**: Validates inputs, enforces role-based permissions, and maps DTOs.
  - **Data Access Layer**: Uses Generic Repository for CRUD and custom methods for complex queries (e.g., including viruses).
- **Database**: `ViroCureFAL2024DB` (created via provided SQL script).

### Frontend
- **Project**: `PE_PRN231_FA24_TrialTest_StudentCode_FE`
- **Framework**: ASP.NET Core Razor Pages.
- **Features**:
  - Login page (default).
  - Role-based UI: CRUD for Doctors, search for Admins/Doctors, view for Patients.
  - Session storage for user info (Token, UserId, Role).
  - Client-side validation and API integration via `HttpClient`.

## Setup Instructions

### Prerequisites
- **Visual Studio 2019+**
- **SQL Server 2012+**
- **.NET Core SDK 6.0 or later**
- **NuGet Packages** (installed via project files):
  - Backend: `Microsoft.EntityFrameworkCore.SqlServer`, `Microsoft.AspNetCore.Authentication.JwtBearer`, `System.IdentityModel.Tokens.Jwt`, `Microsoft.AspNetCore.Cors`.
  - Frontend: `System.Net.Http.Json`, `Microsoft.AspNetCore.Mvc.NewtonsoftJson`.

### Database Setup
1. Run the provided SQL script (`ViroCureFAL2024DB.sql`) in SQL Server Management Studio to create the `ViroCureFAL2024DB` database.
2. Update the connection string in `ViroCureAPI/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=ViroCureFAL2024DB;Trusted_Connection=True;"
     }
   }
   ```

### Backend Setup
1. Open the solution `PE_PRN231_FA24_TrialTest_StudentFullname_BE` in Visual Studio.
2. Restore NuGet packages.
3. Build and run the `ViroCureAPI` project (default port: `https://localhost:5001`).
4. Test API endpoints using Swagger (`/swagger`) or tools like Postman:
   - `POST /api/login`: Authenticate user.
   - `GET /api/person/{id}`: Get person details.
   - `GET /api/persons`: Get all persons.
   - `POST /api/person`: Add person.
   - `PUT /api/person/{id}`: Update person.
   - `DELETE /api/person/{id}`: Delete person.

### Frontend Setup
1. Open the project `PE_PRN231_FA24_TrialTest_StudentCode_FE` in Visual Studio.
2. Update the API base address in Razor Page code-behind files (e.g., `Login.cshtml.cs`) if needed:
   ```csharp
   _httpClient.BaseAddress = new Uri("https://localhost:5001/api/");
   ```
3. Restore NuGet packages.
4. Build and run the project (default port: `https://localhost:5000`).
5. Navigate to the login page (`/Login`) and test with sample credentials (e.g., `john.doe@example.com`, `password123`).

## Usage
1. **Login**:
   - Use credentials from the `ViroCureUser` table (e.g., `doctor@example.com`, `password789` for Doctor role).
   - On success, the system stores user info in session and redirects to the main page.
2. **Role-Based Access**:
   - **Doctor**: Add, edit, delete, and search persons/viruses.
   - **Admin**: Search persons.
   - **Patient**: View own information.
3. **CRUD Operations**:
   - Add/Edit: Validate inputs (FullName, Birthday, Phone, Resistance Rate).
   - Delete: Confirm before deletion.
   - Search: Available for Doctors and Admins.

## Key Implementation Details
- **Backend**:
  - **3-Layer Architecture**: Separation of concerns (API → BLL → DAL).
  - **Generic Repository**: Reusable CRUD operations for all entities (`Person`, `ViroCureUser`, `Virus`, `PersonVirus`).
  - **DB-First**: EF Core models generated from `ViroCureFAL2024DB`.
  - **JWT Authentication**: Secure endpoints with role-based authorization.
  - **CORS**: Enabled for frontend access.
- **Frontend**:
  - Razor Pages for simple, role-based UI.
  - Session-based user management.
  - API integration with proper error handling.

## Troubleshooting
- **Database Connection**: Ensure SQL Server is running and the connection string is correct.
- **API Errors**: Check Swagger for endpoint responses or debug `ViroCureAPI`.
- **Frontend Issues**: Verify API base address and session data.
- **CORS Issues**: Ensure `AllowAll` policy is configured in `ViroCureAPI/Program.cs`.

## License
This project is for educational purposes only and is not licensed for commercial use.

## Contact
For issues or questions, please contact [https://github.com/HungTabe] at [trandinhhung717@gmail.com].
