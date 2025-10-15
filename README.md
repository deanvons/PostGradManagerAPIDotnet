# PostGradManager API (.NET)

An ASP.NET Core Web API for managing postgraduate students, programs, and related data.

## Tech Stack
- **.NET** 8 (ASP.NET Core Web API)
- **Entity Framework Core** (Code-First, SQL Server or PostgreSQL)
- **Swagger / OpenAPI** for API documentation

## Prerequisites
- .NET SDK 8.x installed
- A database (SQL Server or PostgreSQL)
- (Optional) `dotnet-ef` CLI: `dotnet tool install --global dotnet-ef`

## Getting Started

### 1. Clone the repository
```bash
git clone https://github.com/deanvons/PostGradManagerAPIDotnet.git
cd PostGradManagerAPIDotnet
2. Configure the database connection
Edit appsettings.Development.json and update the connection string.
```
SQL Server example:

json
Copy code
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PostGradManager;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
PostgreSQL example:

json
Copy code
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=postgradmanager;Username=postgres;Password=yourpassword"
  }
}
3. Apply EF Core migrations
bash
Copy code
# Create a migration if one doesn’t exist
dotnet ef migrations add InitialCreate

# Update the database
dotnet ef database update
4. Run the API
bash
Copy code
dotnet run
Then open Swagger UI in your browser:

bash
Copy code
https://localhost:5001/swagger
Project Structure
bash
Copy code
PostGradManagerAPIDotnet/
│
├── Controllers/       # API endpoints
├── Data/              # DbContext and EF Core setup
├── Models/            # Entity classes
├── DTOs/              # Data transfer objects
├── Migrations/        # EF Core migrations
├── Program.cs         # Entry point
└── appsettings*.json  # Configuration
Contributing
Pull requests are welcome! Please follow standard Git branching and PR practices.
