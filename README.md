# PebriBox - Real Estate Management API
*For Learning Purpose*: https://www.udemy.com/course/net-clean-architecture-solutions

A clean architecture .NET Web API for managing real estate properties and agents, built with ASP.NET Core and PostgreSQL.

## 🏗️ Architecture

This project follows **Clean Architecture** (Onion Architecture) principles:

```
PebriBox.Domain          (Core - Business Entities)
    ↑
PebriBox.Application     (Use Cases - Business Logic)
    ↑
PebriBox.Infrastructure  (Data Access - EF Core)
    ↑
PebriBox.WebAPI          (Presentation - Controllers)
```

### Project Structure

```
PebriBox.sln
├── PebriBox.Domain/              # Core business entities
│   └── Entities/
│       ├── Property.cs           # Real estate property
│       └── Agent.cs              # Real estate agent
│
├── PebriBox.Application/         # Business logic & interfaces
│   └── Wrappers/
│       ├── IResponseWrapper.cs   # Response interfaces
│       └── ResponseWrapper.cs    # Response implementations
│
├── PebriBox.Infrastructure/      # Data access layer
│   ├── Contexts/
│   │   └── ApplicationDbContext.cs
│   ├── Migrations/               # EF Core migrations
│   └── Startup.cs                # Infrastructure DI registration
│
└── PebriBox.WebAPI/              # API endpoints
    ├── Controllers/
    ├── appsettings.json
    └── Program.cs
```

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL 16](https://www.postgresql.org/download/) or use Homebrew on Mac
- [Git](https://git-scm.com/)

### Installation

#### 1. Clone the repository

```bash
git clone <your-repo-url>
cd NET
```

#### 2. Install PostgreSQL (macOS)

```bash
# Using Homebrew
brew install postgresql@16

# Start PostgreSQL service
brew services start postgresql@16

# Add to PATH
echo 'export PATH="/opt/homebrew/opt/postgresql@16/bin:$PATH"' >> ~/.zshrc
source ~/.zshrc

# Create database
createdb pebribox_db

# Create postgres role (if needed)
psql -d postgres -c "CREATE ROLE postgres WITH LOGIN SUPERUSER CREATEDB CREATEROLE PASSWORD 'postgres';"
```

#### 3. Install .NET EF Tools

```bash
# Install globally
dotnet tool install --global dotnet-ef

# Add to PATH
export PATH="$PATH:$HOME/.dotnet/tools"
echo 'export PATH="$PATH:$HOME/.dotnet/tools"' >> ~/.zprofile
```

#### 4. Restore NuGet Packages

```bash
dotnet restore
```

#### 5. Update Database Connection String

Edit `PebriBox.WebAPI/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=pebribox_db;Username=postgres;Password=postgres"
  }
}
```

#### 6. Apply Database Migrations

```bash
# From solution root directory
dotnet ef database update --project PebriBox.Infrastructure --startup-project PebriBox.WebAPI
```

#### 7. Run the Application

```bash
cd PebriBox.WebAPI
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5051`
- Swagger UI: `http://localhost:5051/swagger` (in Development mode)

## 📊 Database Schema

### Entities

**Property** (Real Estate Listing)
- `Id` - Primary key
- `AgentId` - Foreign key to Agent
- `ShortDescription` - Brief description
- `LongDescription` - Detailed description
- `Price` - Property price (decimal)
- `ListingDate` - Date listed

**Agent** (Real Estate Agent)
- `Id` - Primary key
- `FirstName` - Agent's first name
- `LastName` - Agent's last name
- `Email` - Contact email
- `PhoneNumber` - Contact phone

**Relationship:** One Agent → Many Properties

## 🛠️ Development

### Working with Migrations

```bash
# Create a new migration
dotnet ef migrations add MigrationName --project PebriBox.Infrastructure --startup-project PebriBox.WebAPI

# Apply migrations to database
dotnet ef database update --project PebriBox.Infrastructure --startup-project PebriBox.WebAPI

# List all migrations
dotnet ef migrations list --project PebriBox.Infrastructure --startup-project PebriBox.WebAPI

# Remove last migration (if not applied)
dotnet ef migrations remove --project PebriBox.Infrastructure --startup-project PebriBox.WebAPI

# Rollback to specific migration
dotnet ef database update PreviousMigrationName --project PebriBox.Infrastructure --startup-project PebriBox.WebAPI
```

### Adding Project References

```bash
# Add reference from one project to another
dotnet add ProjectA/ProjectA.csproj reference ProjectB/ProjectB.csproj

# List all references in a project
dotnet list ProjectA/ProjectA.csproj reference
```

### Installing NuGet Packages

```bash
# Install package
dotnet add ProjectName/ProjectName.csproj package PackageName

# List installed packages
dotnet list ProjectName/ProjectName.csproj package
```

### Building the Solution

```bash
# Build entire solution
dotnet build

# Build specific project
dotnet build PebriBox.WebAPI/PebriBox.WebAPI.csproj

# Clean build artifacts
dotnet clean
```

## 📝 API Response Wrapper

All API responses use a consistent wrapper format:

### C# Code
```csharp
public interface IResponseWrapper
{
    List<string> Messages { get; set; }
    bool IsSuccess { get; set; }
}

public interface IResponseWrapper<T> : IResponseWrapper
{
    T Data { get; set; }
}
```

**TypeScript Equivalent:**
```typescript
interface IResponseWrapper {
    messages: string[];
    isSuccess: boolean;
}

interface IResponseWrapper<T> extends IResponseWrapper {
    data: T;
}
```

### JSON Response Examples

**Success Response:**
```json
{
  "messages": ["Property retrieved successfully"],
  "isSuccess": true,
  "data": {
    "id": 1,
    "shortDescription": "3BR apartment",
    "price": 500000,
    "listingDate": "2026-01-14T00:00:00",
    "agentId": 2
  }
}
```

**Error Response:**
```json
{
  "messages": ["Property not found"],
  "isSuccess": false,
  "data": null
}
```

## 🔑 Key Concepts

### Dependency Injection (DI)

**C# Implementation:**
```csharp
// Register service
builder.Services.AddScoped<IPropertyService, PropertyService>();

// Inject in constructor
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _service;
    
    public PropertiesController(IPropertyService service)
    {
        _service = service;
    }
}
```

**TypeScript/NestJS Equivalent:**
```typescript
// Register service
@Injectable()
export class PropertyService { }

// Inject in constructor
@Controller('properties')
export class PropertiesController {
    constructor(private readonly propertyService: PropertyService) {}
}
```

### Entity Relationships

**C# One-to-Many:**
```csharp
public class Agent
{
    public int Id { get; set; }
    public List<Property> PropertyListings { get; set; }  // Collection
}

public class Property
{
    public int Id { get; set; }
    public int AgentId { get; set; }      // Foreign key
    public Agent Agent { get; set; }       // Navigation property
}
```

**TypeScript/TypeORM Equivalent:**
```typescript
@Entity()
export class Agent {
    @PrimaryGeneratedColumn()
    id: number;
    
    @OneToMany(() => Property, property => property.agent)
    propertyListings: Property[];
}

@Entity()
export class Property {
    @PrimaryGeneratedColumn()
    id: number;
    
    @ManyToOne(() => Agent, agent => agent.propertyListings)
    agent: Agent;
}
```

## 🗂️ Database Commands

### PostgreSQL Commands

```bash
# Connect to database
psql -d pebribox_db

# List all databases
psql -l

# Inside psql:
\dt                 # List tables
\d table_name       # Describe table
\q                  # Quit
```

## 🧪 Testing

```bash
# Run all tests (when tests are added)
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"
```

## 📦 Production Deployment

```bash
# Publish for production
dotnet publish -c Release -o ./publish

# Run published app
cd publish
dotnet PebriBox.WebAPI.dll
```

## 🔧 Troubleshooting

### "Could not execute because the specified command or file was not found"
- Install dotnet-ef tools: `dotnet tool install --global dotnet-ef`
- Add to PATH: `export PATH="$PATH:$HOME/.dotnet/tools"`

### "Unable to create a 'DbContext' of type..."
- Always specify `--startup-project PebriBox.WebAPI` when running migrations
- Ensure `Microsoft.EntityFrameworkCore.Design` is installed in WebAPI project

### Connection Errors
- Verify PostgreSQL is running: `brew services list`
- Check connection string in `appsettings.json`
- Test connection: `psql -d pebribox_db`

## 📚 Technologies Used

- **Framework:** ASP.NET Core 10.0
- **ORM:** Entity Framework Core 10.0
- **Database:** PostgreSQL 16
- **Language:** C# 12
- **Architecture:** Clean Architecture / Onion Architecture
- **API Documentation:** OpenAPI (Swagger)

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/amazing-feature`
3. Commit changes: `git commit -m 'Add amazing feature'`
4. Push to branch: `git push origin feature/amazing-feature`
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License.

## 👨‍💻 Author

Your Name - [@yourhandle](https://github.com/yourhandle)

---

**Note:** This is a learning project demonstrating Clean Architecture principles in .NET with PostgreSQL on macOS.
