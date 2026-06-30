PRODUCT API 🚀
A REST API built with ASP.NET Core featuring JWT authentication, Entity Framework Core, SQL Server (Docker), and unit testing.

------------------------------------------------------------
TECHNOLOGIES
------------------------------------------------------------

- ASP.NET Core 10 Web API
- Entity Framework Core
- SQL Server (Docker)
- JWT Authentication
- Serilog (logging)
- xUnit (unit testing)
- Moq (mocking)
- FluentAssertions
- Docker & Docker Compose

------------------------------------------------------------
ARCHITECTURE
------------------------------------------------------------

Controller → Service → DbContext → SQL Server

Tests:
Controller → Mock (Moq)
Service → InMemory Database

------------------------------------------------------------
HOW TO RUN THE PROJECT
------------------------------------------------------------

1. Start with Docker:

docker compose up --build

------------------------------------------------------------
API AVAILABLE AT
------------------------------------------------------------

http://localhost:8080

Swagger:
http://localhost:8080/swagger

------------------------------------------------------------
DATABASE
------------------------------------------------------------

- SQL Server running in Docker
- Automatically created using EF Core Migrations
- Seed data included (DbSeeder)

------------------------------------------------------------
JWT AUTHENTICATION
------------------------------------------------------------

Endpoint:

POST /api/auth/login

Example response:

{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}

------------------------------------------------------------
HOW TO USE THE TOKEN
------------------------------------------------------------

Add it to the request header:

Authorization: Bearer <token>

------------------------------------------------------------
PRODUCT ENDPOINTS
------------------------------------------------------------

GET     /api/products        -> Get all products
GET     /api/products/{id}   -> Get product by ID
POST    /api/products        -> Create product
PUT     /api/products/{id}   -> Update product
DELETE  /api/products/{id}   -> Delete product

------------------------------------------------------------
TESTS
------------------------------------------------------------

Run tests:

dotnet test

Test types:
- Unit tests (Controller with mocks)
- Service tests (InMemory database)

------------------------------------------------------------
DOCKER
------------------------------------------------------------

Containers:
- product-api
- sqlserver
