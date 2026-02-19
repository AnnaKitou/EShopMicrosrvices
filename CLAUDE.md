# EShop Microservices — Claude Guide

## Project Overview

A .NET 8 microservices e-commerce solution following **Clean Architecture**, **Domain-Driven Design (DDD)**, and **CQRS** patterns.

**Solution:** `src/eshop-microservices.sln`

---

## Architecture

### Services

| Service | Pattern | Database | Port |
|---------|---------|----------|------|
| `Catalog.API` | Carter + CQRS + Marten | PostgreSQL (CatalogDb, port 5432) | — |
| `Basket.API` | Carter + CQRS + Decorator | PostgreSQL (BasketDb, port 5433) + Redis | — |
| `Discount.Grpc` | gRPC + EF Core | SQLite | 5052 |
| `Ordering.API` | Clean Architecture + DDD | SQL Server | — |

### Shared Libraries

`src/BuildingBlocks/BuildingBlocks/` — Cross-cutting concerns:
- CQRS abstractions (`ICommand`, `IQuery`, handlers)
- MediatR pipeline behaviors (validation, logging)
- Exception types and global exception handler

---

## Key Patterns

### CQRS via MediatR
- Commands implement `ICommand` / `ICommand<TResponse>`
- Queries implement `IQuery<TResponse>`
- Handlers registered automatically via MediatR assembly scanning
- Pipeline: `LoggingBehavior` → `ValidationBehavior` → Handler

### DDD (Ordering Domain)
- **Aggregates** inherit `Aggregate<TId>` — manages domain events
- **Entities** inherit `Entity<T>` — includes audit fields (`CreatedAt`, `LastModified`, etc.)
- **Value objects** are immutable records with static `Of()` factory methods and guard clauses
- **Domain events** implement `IDomainEvent`, raised inside aggregate methods
- **Strongly typed IDs** (e.g. `OrderId`, `CustomerId`) prevent primitive obsession

### Decorator Pattern (Basket caching)
`CachedBasketRepository` decorates `BasketRepository` using **Scrutor**:
```csharp
services.Decorate<IBasketRepository, CachedBasketRepository>();
```

### Repository Pattern
Interfaces in Application layer; implementations in Infrastructure.

---

## Domain Model (Ordering)

```
Ordering.Domain/
├── Abstractions/
│   ├── Aggregate.cs       # Aggregate<TId> base — holds domain events
│   └── Entity.cs          # Entity<T> base — audit fields
├── Models/
│   ├── Order.cs           # Aggregate root — Create(), Update(), Add(), Remove()
│   ├── OrderItem.cs
│   ├── Customer.cs
│   └── Product.cs
├── ValueObjects/
│   ├── OrderId.cs
│   ├── CustomerId.cs
│   ├── ProductId.cs
│   ├── OrderItemId.cs
│   ├── OrderName.cs       # Validates length == 5
│   ├── Address.cs         # FirstName, LastName, EmailAddress, AddressLine, Country, State, ZipCode
│   └── Payment.cs         # CardName, CardNumber, Expiration, CVV (max 3 chars), PaymentMethod
├── Events/
│   ├── OrderCreatedEvent.cs
│   └── OrderUpdatedEvent.cs
├── Enums/
│   └── OrderStatus.cs     # Pending, Processing, Completed, Cancelled
└── Exceptions/
    └── DomainException.cs
```

---

## Technology Stack

| Concern | Technology |
|---------|-----------|
| Framework | .NET 8, C# (nullable enabled, implicit usings) |
| REST routing | Carter 8 |
| Mediator / CQRS | MediatR 12 |
| Validation | FluentValidation 11 |
| Object mapping | Mapster 7 |
| Document store | Marten 6 (PostgreSQL) |
| ORM | Entity Framework Core 8 (SQLite, SQL Server) |
| gRPC | Grpc.AspNetCore |
| Distributed cache | StackExchange.Redis |
| DI decoration | Scrutor 4 |
| Containerization | Docker / docker-compose |
| Health checks | AspNetCore.HealthChecks.* |

---

## Infrastructure

### Docker Compose (`src/docker-compose.yml`)
- `catalogdb` — PostgreSQL for Catalog
- `basketdb` — PostgreSQL for Basket
- `distributedcache` — Redis
- Each service has its own Dockerfile

### Connection Strings (appsettings.json)
- **Catalog:** `Server=localhost;Port=5432;Database=CatalogDb;User Id=postgres;Password=postgres`
- **Basket:** `Server=localhost;Port=5433;Database=BasketDb;` + Redis `localhost:6379`
- **Discount:** SQLite (auto-created via EF migrations)
- **Ordering:** SQL Server (configure locally)

### Migrations
Run EF migrations for Discount and Ordering services:
```bash
dotnet ef migrations add <MigrationName> --project Ordering.Infrastructure --startup-project Ordering.API
dotnet ef database update --project Ordering.Infrastructure --startup-project Ordering.API
```

---

## Development Notes

- **Ordering infrastructure is in progress** — `OrderConfiguration` has `NotImplementedException`; Infrastructure DI is commented out
- **No test projects yet** — testing infrastructure to be added
- **gRPC proto file:** `Discount.Grpc/Protos/discount.proto` — regenerate C# after any `.proto` changes
- Service registration follows the pattern `Add*Services()` extension methods, called from `Program.cs`
- All domain validation throws `DomainException`; application-layer validation uses FluentValidation

---

## Common Commands

```bash
# Start all infrastructure dependencies
docker-compose -f src/docker-compose.yml up -d catalogdb basketdb distributedcache

# Run a specific service
dotnet run --project src/Services/Catalog/Catalog.API

# Build entire solution
dotnet build src/eshop-microservices.sln
```
