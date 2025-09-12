![.NET](https://img.shields.io/badge/.NET-9-purple)
![Docker](https://img.shields.io/badge/Docker-Compose-blue)
![MongoDB](https://img.shields.io/badge/MongoDB-4EA94B?logo=mongodb&logoColor=white)
![Redis](https://img.shields.io/badge/Redis-DC382D?logo=redis&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?logo=microsoft-sql-server&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?logo=postgresql&logoColor=white)
![Keycloak](https://img.shields.io/badge/Keycloak-4D4D4D?logo=redhat&logoColor=white)
![YARP](https://img.shields.io/badge/YARP-API_Gateway-orange)
![MediatR](https://img.shields.io/badge/MediatR-CQRS-green)
![RabbitMQ](https://img.shields.io/badge/RabbitMQ-FF6600?logo=rabbitmq&logoColor=white)
![MassTransit](https://img.shields.io/badge/MassTransit-Message_Bus-lightgrey)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=black)

# Course Microservice Platform

Bu proje, online eğitim platformları için tasarlanmış modern bir mikroservis mimarisidir. .NET 9, MongoDB, Redis, SQL Server ve Keycloak teknolojilerini kullanarak ölçeklenebilir ve güvenli bir e-öğrenme sistemi sunar.

## 🏗️ Mimari

Proje mikroservis mimarisini benimser ve her servis kendi sorumluluğuna odaklanır:

- **API Gateway (YARP)**: Tüm istekleri yönlendiren merkezi giriş noktası
- **Catalog API**: Kurs ve kategori yönetimi
- **Basket API**: Sepet işlemleri ve oturum yönetimi
- **Order API**: Sipariş işlemleri ve siparişlerin durumu
- **Payment API**: Ödeme işlemleri
- **Discount API**: İndirim kuponları ve promosyon yönetimi
- **File API**: Dosya yükleme ve statik içerik sunumu

Ek olarak servisler arası iletişimde RabbitMQ ve MassTransit kullanılır.

## 🎯 Temel Özellikler

### 🎓 Kurs Yönetimi

- Kurs oluşturma, düzenleme ve silme
- Kategori bazlı organizasyon
- Kurs detayları (süre, eğitmen, rating)
- Kullanıcıya özel kurs listeleme

### 🛒 Sepet ve Sipariş

- Gerçek zamanlı sepet yönetimi (Redis)
- İndirim kuponları uygulama
- Sipariş oluşturma ve takibi
- Sipariş durumu güncellemeleri

### 💰 Ödeme ve İndirimler

- Güvenli ödeme işlemleri
- İndirim kuponu sistemi
- Kullanıcı bazlı indirim yönetimi
- Sipariş toplam hesaplaması

### 🔐 Güvenlik ve Kimlik Doğrulama

- Keycloak entegrasyonu
- JWT token tabanlı kimlik doğrulama
- Servisler arası güvenli iletişim

## 🛠️ Teknoloji Stack

### Backend

- **.NET 9**: Ana framework
- **ASP.NET Core Minimal APIs**: Lightweight API endpoints
- **Entity Framework Core**: ORM ve veritabanı erişimi
- **MediatR**: CQRS pattern implementasyonu
- **AutoMapper**: Object mapping
- **FluentValidation**: Input validation

### Veritabanları

- **MongoDB**: Catalog ve Discount servisleri için
- **Redis**: Basket cache sistemi için
- **SQL Server**: Order servisi için
- **PostgreSQL**: Keycloak için

### Altyapı

- **Docker & Docker Compose**: Containerization
- **YARP**: API Gateway
- **Keycloak**: Identity ve Access Management
- **RabbitMQ + MassTransit**: Event-driven asenkron iletişim
- **Swagger/OpenAPI**: API dokümantasyonu

## 🚀 Hızlı Başlangıç

### Gereksinimler

- Docker Desktop
- .NET 9 SDK
- Visual Studio 2022 veya Visual Studio Code

### Kurulum

1. **Repository'yi klonlayın:**

```bash
git clone <repository-url>
cd course-microservice
```

2. **Environment variables dosyasını oluşturun:**

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

3. **Docker container'ları başlatın:**

```bash
docker-compose up -d
```

4. **Servisleri çalıştırın:**

```bash
dotnet run --project CourseMicroservice.Gateway
dotnet run --project CourseMicroservice.Catalog.API
dotnet run --project CourseMicroservice.Basket.API
dotnet run --project CourseMicroservice.Order.API
dotnet run --project CourseMicroservice.Payment.API
dotnet run --project CourseMicroservice.Discount.API
dotnet run --project CourseMicroservice.File.API
```

5. **Keycloak realm ve client’ları içe aktarın (opsiyonel ama önerilir):**

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

### Token Alma Örnekleri

Password Policy gerektiren rotalar için (kullanıcı akışı):

```bash
curl -X POST \
  -d "client_id=Web" \
  -d "grant_type=password" \
  -d "username=<user>" \
  -d "password=<pass>" \
  http://localhost:8080/realms/courseTenant/protocol/openid-connect/token
```

ClientCredential Policy gerektiren rotalar için (uygulama akışı):

```bash
curl -X POST \
  -d "client_id=Gateway.API" \
  -d "client_secret=<secret>" \
  -d "grant_type=client_credentials" \
  http://localhost:8080/realms/courseTenant/protocol/openid-connect/token
```

6. **Order servisi için veritabanını oluşturun (EF Core Migrations):**

```bash
dotnet tool install --global dotnet-ef
dotnet ef database update \
  --project CourseMicroservice.Order.Persistence \
  --startup-project CourseMicroservice.Order.API
```

> Not: Order API `appsettings.Development.json` içindeki `SqlServer` connection string’i docker-compose ile açılan SQL Server portu (1434) ve `SA_PASSWORD` ile uyumludur. Farklı ortamda çalıştıracaksanız bu değerleri güncelleyin.

## 📋 API Endpoints

### Gateway (Port: 5220)

- Ana giriş noktası, tüm istekleri ilgili servislere yönlendirir
- URL versiyonlaması kullanılır: `/v1/...`, `/v2/...`

#### YARP Rotaları ve Politikalar

- `/{version}/baskets/{**}` -> Basket.API (`/api/{version}/baskets/{**}`), Policy: Password
- `/{version}/catalogs/{**}` -> Catalog.API (`/api/{version}/{**}`), Policy: ClientCredential
- `/{version}/discounts/{**}` -> Discount.API (`/api/{version}/discounts/{**}`), Policy: Password
- `/{version}/files` -> File.API (`/api/{version}/files`), Policy: ClientCredential
- `/{version}/orders/{**}` -> Order.API (`/api/{version}/orders/{**}`), Policy: Password
- `/{version}/payments/{**}` -> Payment.API (`/api/{version}/payments/{**}`), Policy: Password

### Catalog API

- `GET /api/v1/courses` - Tüm kursları listele
- `GET /api/v1/courses/{id}` - Kurs detayı
- `POST /api/v1/courses` - Yeni kurs oluştur
- `GET /api/v1/categories` - Kategorileri listele

### Basket API

- `GET /api/v1/baskets/user` - Kullanıcı sepetini getir
- `POST /api/v1/baskets` - Sepete ürün ekle
- `PUT /api/v1/baskets/user/apply-discount` - İndirim uygula

### Order API

- `GET /api/v1/orders` - Siparişleri listele
- `POST /api/v1/orders` - Yeni sipariş oluştur

### Payment API

- `POST /api/v1/payments` - Ödeme işlemi

### Discount API

- `POST /api/v1/discounts` - Yeni indirim kuponu oluştur
- `GET /api/v1/discounts/{code}` - İndirim kuponunu sorgula

### File API

- `POST /api/v1/files` - Dosya yükleme (MassTransit komutu tetikler)
- Statik dosyalar servis üzerinden `/files/{name}` ile sunulur (Gateway üzerinden değil)

## 🔧 Servis Portları

| Servis                | Port  | Admin UI               |
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

## 🏛️ Mimari Desenler

### CQRS (Command Query Responsibility Segregation)

- MediatR kullanılarak command ve query'ler ayrılmış
- Her işlem için ayrı handler'lar

### Domain-Driven Design (DDD)

- Order servisi için layered architecture
- Domain, Application, Persistence katmanları

### Repository Pattern

- Veritabanı erişim katmanının soyutlanması
- Unit of Work pattern implementasyonu

### Minimal APIs

- Lightweight endpoint tanımlamaları
- Extension method'lar ile organize edilmiş endpoint'ler

### Event-Driven İletişim (RabbitMQ + MassTransit)

- Komutlar: `UploadCoursePictureCommand` (Gateway veya başka bir servis tarafından tetiklenir, File API tüketir)
- Olaylar: `CoursePictureUploadedEvent` (File API yayınlar, Catalog API tüketir ve `ImageUrl` günceller)
- Ortak yapılandırma: `BusOptions` ile RabbitMQ host/kullanıcı/şifre/port ayarı

## 🔄 Mikroservis İletişimi

- **Synchronous**: HTTP/REST API çağrıları
- **API Gateway Pattern**: YARP ile merkezi routing
- **Database per Service**: Her servis kendi veritabanı
- **Authentication**: Keycloak ile merkezi kimlik doğrulama

### Yetkilendirme Politikaları

- `Password` Policy: Kullanıcı bazlı akış; JWT içinde `email` claim’i zorunlu
- `ClientCredential` Policy: Servis hesabı/uygulama kimlik bilgileri; JWT içinde `client_id` claim’i zorunlu

Keycloak’ta ilgili client’ların erişim türlerini (confidential/public) ve rollerini ayarlayarak bu politikalara uygun token’lar üretmelisiniz.

## 📊 Monitoring ve Admin

- **Swagger UI**: Her servis için API dokümantasyonu
- **Mongo Express**: MongoDB veritabanı yönetimi
- **Redis Commander**: Redis cache yönetimi
- **pgAdmin**: PostgreSQL yönetimi
- **RabbitMQ Management**: Kuyruk ve exchange yönetimi

## ❓ Sık Karşılaşılan Sorunlar

- SQL Server bağlantı hatası: `.env` içindeki `SA_PASSWORD` ile Order API `appsettings.Development.json` içindeki şifre uyumlu olmalı (`Password12*`).
- Keycloak 401: İstek gönderdiğiniz rota hangi policy’i istiyorsa (Password/ClientCredential) ona uygun token kullanın. Gateway rotaları kısmını kontrol edin.
- Mongo bağlantı hatası: `MONGO_USERNAME`/`MONGO_PASSWORD` değerleri ile Catalog/Discount connection string’leri uyumlu olmalı (`mongodb://myuser:Password12@localhost:27030`).
