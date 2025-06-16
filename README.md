# 🛒 Shopping Cart API with .NET 9 & .NET Aspire

A simple yet modern Shopping Cart API built using **.NET 9**, **Entity Framework Core**, and **PostgreSQL**, orchestrated using **.NET Aspire**.

---

## 🚀 Tech Stack

- **.NET 9 Web API**
- **Entity Framework Core**
- **PostgreSQL** (managed by Aspire)
- **.NET Aspire AppHost & Dashboard**
- **OpenAPI (Swagger)** for API docs

---

## 📁 Project Structure

```
/src
  /AppHost         --> .NET Aspire host (orchestration & dashboard)
  /ProductCatalog  --> Shopping Cart API project
```

---

## ⚙️ Features

- ✅ Add, update, and remove products
- ✅ Fetch product list or product details
- ✅ Database powered by PostgreSQL
- ✅ Hosted and managed with .NET Aspire
- ✅ Automatic service discovery and dashboard
- ✅ Swagger UI for exploring the API

---

## 🧰 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [Visual Studio 2022 17.8+](https://visualstudio.microsoft.com/)
- No need for Docker/Postgres setup – Aspire handles this

---

### 🚦 Run the Project

Use this command from the root of your solution:

```bash
dotnet run --project src/AppHost
```

This will start:

- The **Shopping Cart API**
- The **PostgreSQL database**
- The **Aspire dashboard**

---

### 🌐 Aspire Dashboard

- URL: `https://localhost:17100`
- Shows status of all services (API, PostgreSQL)
- Allows quick access to Swagger, PgAdmin, etc.

---

### 🔌 API Endpoints (Examples)

| Method | Endpoint               | Description            |
|--------|------------------------|------------------------|
| GET    | `/api/products`        | Get all products       |
| GET    | `/api/products/{id}`   | Get product by ID      |
| POST   | `/api/products`        | Create a new product   |
| PUT    | `/api/products`   | Update product         |
| DELETE | `/api/products/{id}`   | Delete product         |

Explore the full API via Swagger:  
`https://localhost:<your-api-port>/swagger`

---


## 🔧 Configurations

**PostgreSQL** is automatically set up via Aspire:

- Username: `postgres`
- Password: `YourStrong!Passw0rd`
- Use PgAdmin via Aspire Dashboard

---

## 📌 Notes

- Currently using **Entity Framework Core** (no Dapper or CQRS)
- Can be extended later with **CQRS**, **MediatR**, **Dapper**, etc.
- Suitable for microservices-style architecture

---

## 📄 License

MIT

---

## 🔗 Useful Links

- [.NET Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/)
- [ASP.NET Web API](https://learn.microsoft.com/en-us/aspnet/core/web-api/)
