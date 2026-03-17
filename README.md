# 🎓 PostGradManager API (.NET)

An **ASP.NET Core Web API** for managing postgraduate students, academic
programs, and related data.

------------------------------------------------------------------------

## 🚀 Tech Stack

-   **.NET 8** -- ASP.NET Core Web API\
-   **Entity Framework Core** -- Code-First ORM\
-   **Database Support** -- SQL Server / PostgreSQL\
-   **Swagger (OpenAPI)** -- API documentation

------------------------------------------------------------------------

## 📋 Prerequisites

-   .NET SDK 8.x\
-   SQL Server or PostgreSQL\
-   (Optional) EF CLI: `dotnet tool install --global dotnet-ef`

------------------------------------------------------------------------

## ⚙️ Getting Started

### 1. Clone the Repository

``` bash
git clone https://github.com/deanvons/PostGradManagerAPIDotnet.git
cd PostGradManagerAPIDotnet
```

### 2. Configure the Database

Edit `appsettings.Development.json`.

#### SQL Server

``` json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PostGradManager;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

#### PostgreSQL

``` json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=postgradmanager;Username=postgres;Password=yourpassword"
  }
}
```

### 3. Apply Migrations

``` bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Run

``` bash
dotnet run
```

Swagger: https://localhost:5001/swagger

------------------------------------------------------------------------

## 📁 Structure

    Controllers/   # Endpoints
    Data/          # DbContext
    Models/        # Entities
    DTOs/          # Contracts
    Migrations/    # DB history
    Program.cs     # Entry

------------------------------------------------------------------------

## 🤝 Contributing

-   Feature branches\
-   Clean commits\
-   PR review
