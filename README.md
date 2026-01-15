# Optician Web API

A RESTful API for optical shop management. Handles inventory (frames, glasses, lenses), sales tracking, and expense management with JWT authentication.

## 🎯 Purpose

Provides backend services for managing an optical retail business including:
- Frame, glasses, and lens inventory
- Sales transactions
- Business expenses tracking
- User authentication and management

## 🛠 Tech Stack

| Component | Technology |
|-----------|-----------|
| Framework | ASP.NET Core 8.0 |
| Database | PostgreSQL |
| ORM | Entity Framework Core |
| Authentication | JWT Bearer |
| Container | Docker |
| API Docs | Swagger/OpenAPI |

##  API Endpoints

| Resource | Endpoints |
|----------|-----------|
| **Auth** | `POST /api/auth/register`, `POST /api/auth/login` |
| **Frames** | `GET/POST/PUT/DELETE /api/frames` |
| **Glasses** | `GET/POST/PUT/DELETE /api/glasses` |
| **Lenses** | `GET/POST/PUT/DELETE /api/lens` |
| **Sales** | `GET/POST/PUT/DELETE /api/sales` |
| **Expenses** | `GET/POST/PUT/DELETE /api/expenses` |

## � Project Structure

```
├── Controllers/        # API endpoints
├── Models/            # Database entities
├── DTOs/              # Request/Response objects
├── Services/          # Business logic
├── DatabaseContext/   # EF Core context
├── Migrations/        # DB schema versions
└── Program.cs         # App configuration
```

## 📖 Resources

- [ASP.NET Core Docs](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [PostgreSQL](https://www.postgresql.org)
- [Docker](https://docs.docker.com)

---

**Version**: 1.0.0 | **Last Updated**: January 15, 2026