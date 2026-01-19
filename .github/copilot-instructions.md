# GitHub Copilot Instructions

## Code Explanation Style

When explaining code or generating documentation:

1. **Always explain C# code first** - Provide a clear explanation of what the C# code does
2. **Follow with TypeScript comparison** - Show the equivalent TypeScript implementation
3. **Highlight key differences** - Point out syntax and conceptual differences between C# and TypeScript

### Example Format:

**C# Code:**
```csharp
// Explanation of C# code here
public class Example { }
```

**TypeScript Equivalent:**
```typescript
// Comparison with TypeScript
export class Example { }
```

## Project Context

This is a **Clean Architecture .NET Web API** project called PebriBox for managing real estate properties and agents.

### Architecture Layers (from inside out):

1. **Domain** (`PebriBox.Domain`) - Core business entities, no dependencies
2. **Application** (`PebriBox.Application`) - Business logic, interfaces, use cases
3. **Infrastructure** (`PebriBox.Infrastructure`) - Data access, EF Core, external services
4. **WebAPI** (`PebriBox.WebAPI`) - Controllers, API endpoints, presentation layer

### Tech Stack:

- **.NET 10** with C# 12
- **Entity Framework Core 10** (ORM)
- **PostgreSQL 16** (Database)
- **Npgsql** (PostgreSQL provider)
- **Clean Architecture** pattern
- **Dependency Injection** throughout

### Key Entities:

- **Property** - Real estate listings (Id, AgentId, ShortDescription, LongDescription, Price, ListingDate)
- **Agent** - Real estate agents (Id, FirstName, LastName, Email, PhoneNumber, PropertyListings)
- **Relationship:** One Agent has Many Properties

### Project Dependencies:

```
Domain (no dependencies)
  ↑
Application → Domain
  ↑
Infrastructure → Application
  ↑
WebAPI → Infrastructure
```

## Code Generation Guidelines

### When creating entities:
- Use C# record types for immutable DTOs
- Use classes for entities with behavior
- Always include navigation properties for relationships
- Follow Entity Framework Core conventions

### When creating services:
- Always use interfaces (`IServiceName`)
- Register in appropriate Startup.cs (Infrastructure or Application)
- Use dependency injection via constructor
- Use async/await for database operations

### When creating controllers:
- Inherit from `ControllerBase`
- Use attribute routing: `[Route("api/[controller]")]`
- Return `ActionResult<T>` or `IResponseWrapper<T>`
- Use async methods
- Include XML comments for Swagger

### Response Wrapper Pattern:
All API responses should use the standardized wrapper:
```csharp
IResponseWrapper<T> {
    List<string> Messages,
    bool IsSuccess,
    T Data
}
```

### Naming Conventions:
- **Interfaces:** `IServiceName`
- **Implementations:** `ServiceName`
- **Controllers:** `EntityNameController` (plural)
- **Entities:** Singular names (Property, Agent)
- **DbSets:** Plural names (Properties, Agents)

### Database Context:
- DbContext is `ApplicationDbContext` in `PebriBox.Infrastructure.Contexts`
- Migration history table in schema "EFCore"
- Connection string key: "DefaultConnection"

### Migration Commands:
Always specify both projects:
```bash
dotnet ef migrations add MigrationName --project PebriBox.Infrastructure --startup-project PebriBox.WebAPI
dotnet ef database update --project PebriBox.Infrastructure --startup-project PebriBox.WebAPI
```

## Coding Standards

### Dependency Injection:
- **Scoped:** DbContext, business services (per HTTP request)
- **Transient:** Lightweight, stateless services
- **Singleton:** Expensive objects, shared state

### Async Patterns:
- All database operations use async/await
- Method names end with `Async`
- Return `Task<T>` or `Task`

### Error Handling:
- Use try-catch in controllers
- Return appropriate HTTP status codes
- Use IResponseWrapper for consistent error responses

### Code Comments:
- XML documentation comments for public APIs
- Inline comments for complex business logic
- Always compare with TypeScript when explaining

## When I Ask Questions:

1. **Explain code:** Show C# explanation first, then TypeScript equivalent
2. **Show examples:** Provide both C# and TypeScript versions
3. **Troubleshooting:** Consider both .NET CLI and macOS-specific issues
4. **Be concise:** Keep explanations practical and to the point

## Platform Specifics:

- **OS:** macOS (zsh shell)
- **Package Manager:** Homebrew for system tools, dotnet CLI for .NET packages
- **Database:** PostgreSQL installed via Homebrew
- **No Visual Studio:** Using VS Code + .NET CLI (no GUI tools)

## TypeScript Comparison Context:

When comparing with TypeScript, assume familiarity with:
- NestJS framework (for API patterns)
- TypeORM (for database patterns)
- Dependency Injection in NestJS
- TypeScript interfaces and generics
