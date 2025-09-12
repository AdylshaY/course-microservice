[![.NET](https://img.shields.io/badge/.NET-9-purple)](#)
[![Docker](https://img.shields.io/badge/Docker-Compose-blue)](#)
[![MongoDB](https://img.shields.io/badge/MongoDB-4EA94B?logo=mongodb&logoColor=white)](#)
[![Redis](https://img.shields.io/badge/Redis-DC382D?logo=redis&logoColor=white)](#)
[![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?logo=microsoft-sql-server&logoColor=white)](#)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?logo=postgresql&logoColor=white)](#)
[![Keycloak](https://img.shields.io/badge/Keycloak-4D4D4D?logo=redhat&logoColor=white)](#)
[![YARP](https://img.shields.io/badge/YARP-API_Gateway-orange)](#)
[![MediatR](https://img.shields.io/badge/MediatR-CQRS-green)](#)
[![RabbitMQ](https://img.shields.io/badge/RabbitMQ-FF6600?logo=rabbitmq&logoColor=white)](#)
[![MassTransit](https://img.shields.io/badge/MassTransit-Message_Bus-lightgrey)](#)
[![Swagger](https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=black)](#)

[English](README.md) | [Türkçe](README.tr.md)

# Course Microservice Platform

This project is a modern microservices architecture for online learning platforms. It provides a scalable and secure e-learning system using .NET 9, MongoDB, Redis, SQL Server, and Keycloak.

## 🏗️ Architecture

The platform follows microservices architecture; each service has a single responsibility:

- **API Gateway (YARP)**: Central entry routing all requests
- **Catalog API**: Course and category management
- **Basket API**: Shopping basket operations and session
- **Order API**: Order operations and tracking
- **Payment API**: Payment operations
- **Discount API**: Discount coupons and promotions
- **File API**: File upload and static content

Inter-service async communication is powered by RabbitMQ and MassTransit.

## 🎯 Key Features

### 🎓 Course Management

- Create, update, delete courses
- Category-based organization
- Course details (duration, instructor, rating)
- User-specific course listing

### 🛒 Basket and Orders

- Real-time basket with Redis
- Apply discount coupons
- Create and track orders
- Order status updates

### 💰 Payments and Discounts

- Secure payment workflows
- Discount coupon system
- User-based discount management
- Order total calculation

### 🔐 Security and Identity

- Keycloak integration
- JWT-based authentication
- Secure inter-service communication

## 🛠️ Tech Stack

### Backend

- **.NET 9**
- **ASP.NET Core Minimal APIs**
- **Entity Framework Core**
- **MediatR** (CQRS)
- **AutoMapper**
- **FluentValidation**

### Databases

- **MongoDB** (Catalog, Discount)
- **Redis** (Basket cache)
- **SQL Server** (Order)
- **PostgreSQL** (Keycloak)

### Infrastructure

- **Docker & Docker Compose**
- **YARP** (API Gateway)
- **Keycloak** (IAM)
- **RabbitMQ + MassTransit** (Event-driven async)
- **Swagger/OpenAPI**

## 🚀 Quickstart

### Requirements

- Docker Desktop
- .NET 9 SDK
- Visual Studio 2022 or VS Code

### Setup

1) **Clone the repository:**

```bash
git clone <repository-url>
cd course-microservice
```

2) **Create the environment variables (.env):**

```bash
# .env dosyası oluşturun ve gerekli değişkenleri tanımlayın
# Mongo (Catalog/Discount)
MONGO_USERNAME=myuser
MONGO_PASSWORD=Password12

# Redis (Basket)
REDIS_PASSWORD=Password12
REDIS_UI_USERNAME=admin
REDIS_UI_PASSWORD=admin

# SQL Server (Order)
SA_PASSWORD=Password12*

# Keycloak + Postgres
KEYCLOAK_ADMIN=admin
KEYCLOAK_ADMIN_PASSWORD=admin
POSTGRES_USER=postgres
POSTGRES_PASSWORD=postgres
POSTGRES_DB=keycloak
PGADMIN_DEFAULT_EMAIL=admin@local
PGADMIN_DEFAULT_PASSWORD=admin

# RabbitMQ
RABBITMQ_DEFAULT_USER=root
RABBITMQ_DEFAULT_PASS=Password12
```

3) **Start infrastructure containers:**

```bash
docker-compose up -d
```

4) **Run the services:**

```bash
dotnet run --project CourseMicroservice.Gateway
dotnet run --project CourseMicroservice.Catalog.API
dotnet run --project CourseMicroservice.Basket.API
dotnet run --project CourseMicroservice.Order.API
dotnet run --project CourseMicroservice.Payment.API
dotnet run --project CourseMicroservice.Discount.API
dotnet run --project CourseMicroservice.File.API
```

5) **Import Keycloak realm and clients (optional but recommended):**

- Keycloak UI: `http://localhost:8080`
- Admin ile giriş yapın: `KEYCLOAK_ADMIN` / `KEYCLOAK_ADMIN_PASSWORD`
- `keycloak-clients/realm-export.json` dosyasını Import edin.
- Servislerin `Audience` değerleri Keycloak’ta client ID’leri ile eşleşmelidir:
  - Gateway: `Gateway.API`
  - Catalog: `Catalog.API`
  - Basket: `Basket.API`
  - Order: `Order.API`
  - Payment: `Payment.API`
  - Discount: `Discount.API`
  - File: `File.API`

### Getting Tokens

For Password policy routes (resource owner password):

```bash
curl -X POST \
  -d "client_id=Web" \
  -d "grant_type=password" \
  -d "username=<user>" \
  -d "password=<pass>" \
  http://localhost:8080/realms/courseTenant/protocol/openid-connect/token
```

For ClientCredential policy routes (client credentials):

```bash
curl -X POST \
  -d "client_id=Gateway.API" \
  -d "client_secret=<secret>" \
  -d "grant_type=client_credentials" \
  http://localhost:8080/realms/courseTenant/protocol/openid-connect/token
```

6) **Create Order database (EF Core migrations):**

```bash
dotnet tool install --global dotnet-ef
dotnet ef database update \
  --project CourseMicroservice.Order.Persistence \
  --startup-project CourseMicroservice.Order.API
```

> Note: Order API `SqlServer` connection string matches SQL Server port (1434) from docker-compose and `SA_PASSWORD`. Adjust for your environment if needed.

## 📋 API Endpoints

### Gateway (Port: 5220)

- Central entry; routes requests to downstream services
- URL versioning: `/v1/...`, `/v2/...`

#### YARP Routes & Policies

- `/{version}/baskets/{**}` -> Basket.API (`/api/{version}/baskets/{**}`), Policy: Password
- `/{version}/catalogs/{**}` -> Catalog.API (`/api/{version}/{**}`), Policy: ClientCredential
- `/{version}/discounts/{**}` -> Discount.API (`/api/{version}/discounts/{**}`), Policy: Password
- `/{version}/files` -> File.API (`/api/{version}/files`), Policy: ClientCredential
- `/{version}/orders/{**}` -> Order.API (`/api/{version}/orders/{**}`), Policy: Password
- `/{version}/payments/{**}` -> Payment.API (`/api/{version}/payments/{**}`), Policy: Password

### Catalog API

- `GET /api/v1/courses` - List all courses
- `GET /api/v1/courses/{id}` - Course details
- `POST /api/v1/courses` - Create a course
- `GET /api/v1/categories` - List categories

### Basket API

- `GET /api/v1/baskets/user` - Get user basket
- `POST /api/v1/baskets` - Add item to basket
- `PUT /api/v1/baskets/user/apply-discount` - Apply discount

### Order API

- `GET /api/v1/orders` - List user orders
- `POST /api/v1/orders` - Create order

### Payment API

- `POST /api/v1/payments` - Create payment

### Discount API

- `POST /api/v1/discounts` - Create discount coupon
- `GET /api/v1/discounts/{code}` - Query coupon by code

### File API

- `POST /api/v1/files` - Upload file (triggers MassTransit command)
- Static files are served via service `/files/{name}` (not via gateway)

## 🔧 Service Ports

| Service               | Port  | Admin UI               |
| --------------------- | ----- | ---------------------- |
| Gateway               | 5220  | -                      |
| Catalog API           | 5277  | Swagger                |
| Basket API            | 5237  | Swagger                |
| Discount API          | 5062  | Swagger                |
| File API              | 5275  | Swagger                |
| Order API             | 5074  | Swagger                |
| Payment API           | 5129  | Swagger                |
| MongoDB (Catalog)     | 27030 | Mongo Express: 27032   |
| MongoDB (Discount)    | 27034 | Mongo Express: 27036   |
| Redis (Basket)        | 6379  | Redis Commander: 27033 |
| SQL Server (Order)    | 1434  | -                      |
| RabbitMQ              | 5672  | Management: 15672      |
| Keycloak              | 8080  | -                      |
| PostgreSQL (Keycloak) | 5432  | pgAdmin: 8888          |

## 🏛️ Architectural Patterns

### CQRS (Command Query Responsibility Segregation)

- Commands and queries separated via MediatR
- Dedicated handlers per operation

### Domain-Driven Design (DDD)

- Layered architecture for Order service
- Domain, Application, Persistence layers

### Repository Pattern

- Abstracted data access layer
- Unit of Work pattern

### Minimal APIs

- Lightweight endpoints
- Organized via extension methods

### Event-Driven Communication (RabbitMQ + MassTransit)

- Commands: `UploadCoursePictureCommand` (triggered upstream, consumed by File API)
- Events: `CoursePictureUploadedEvent` (published by File API; consumed by Catalog API to update `ImageUrl`)
- Shared config: `BusOptions` (host/username/password/port)

## 🔄 Microservice Communication

- **Synchronous**: HTTP/REST calls
- **API Gateway Pattern**: Central routing via YARP
- **Database per Service**: Separate DB per service
- **Authentication**: Central auth via Keycloak

### Authorization Policies

- `Password` Policy: End-user tokens (requires `email` claim)
- `ClientCredential` Policy: App/service tokens (requires `client_id` claim)

Configure Keycloak clients (confidential/public) and roles to issue tokens matching the policies.

## 📊 Monitoring & Admin

- **Swagger UI** per service
- **Mongo Express**
- **Redis Commander**
- **pgAdmin**
- **RabbitMQ Management**

## ❓ Troubleshooting

- SQL Server connection: Ensure `SA_PASSWORD` in `.env` matches Order API connection string (`Password12*`).
- Keycloak 401: Use a token matching the required policy (Password/ClientCredential) for the route.
- Mongo connection: `MONGO_USERNAME`/`MONGO_PASSWORD` must match Catalog/Discount connection strings (`mongodb://myuser:Password12@localhost:27030`).
