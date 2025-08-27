![.NET](https://img.shields.io/badge/.NET-9-purple)
![Docker](https://img.shields.io/badge/Docker-Compose-blue)
![MongoDB](https://img.shields.io/badge/MongoDB-4EA94B?logo=mongodb&logoColor=white)
![Redis](https://img.shields.io/badge/Redis-DC382D?logo=redis&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?logo=microsoft-sql-server&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?logo=postgresql&logoColor=white)
![Keycloak](https://img.shields.io/badge/Keycloak-4D4D4D?logo=redhat&logoColor=white)
![YARP](https://img.shields.io/badge/YARP-API_Gateway-orange)
![MediatR](https://img.shields.io/badge/MediatR-CQRS-green)
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
MONGO_USERNAME=admin
MONGO_PASSWORD=password123
REDIS_PASSWORD=redis123
KEYCLOAK_ADMIN=admin
KEYCLOAK_ADMIN_PASSWORD=admin123
SA_PASSWORD=SqlServer123!
POSTGRES_USER=postgres
POSTGRES_PASSWORD=postgres123
POSTGRES_DB=keycloak
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

## 📋 API Endpoints

### Gateway (Port: Default)
- Ana giriş noktası, tüm istekleri ilgili servislere yönlendirir

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

## 🔧 Servis Portları

| Servis | Port | Admin UI |
|--------|------|----------|
| MongoDB (Catalog) | 27030 | 27032 |
| MongoDB (Discount) | 27034 | 27036 |
| Redis (Basket) | 6379 | 27033 |
| SQL Server (Order) | 1434 | - |
| Keycloak | 8080 | - |
| PostgreSQL (Keycloak) | 5432 | 27035 |

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

## 🔄 Mikroservis İletişimi

- **Synchronous**: HTTP/REST API çağrıları
- **API Gateway Pattern**: YARP ile merkezi routing
- **Database per Service**: Her servis kendi veritabanı
- **Authentication**: Keycloak ile merkezi kimlik doğrulama

## 📊 Monitoring ve Admin

- **Swagger UI**: Her servis için API dokümantasyonu
- **Mongo Express**: MongoDB veritabanı yönetimi
- **Redis Commander**: Redis cache yönetimi
- **pgAdmin**: PostgreSQL yönetimi