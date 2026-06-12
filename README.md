# Manufacturing Integration Platform

## Overview

Manufacturing Integration Platform is a project that demonstrates modern Azure integration architecture for manufacturing systems.

The project simulates the modernization of a traditional ERP-to-MES integration process by introducing event-driven architecture, messaging, and cloud-native integration services.

### Legacy Architecture

ERP

↓

Batch Import

↓

MES

### Target Architecture

ERP API

↓

Azure API Management

↓

Azure Service Bus Topic

↓

Azure Function

↓

MES API

↓

PostgreSQL Database

---

## Business Scenario

A manufacturing company releases production orders from an ERP system.

When an order is released:

1. The ERP system publishes a Production Order Released event.
2. The integration layer processes the event.
3. The MES system creates a production job automatically.
4. Production execution can begin without manual intervention.

---

## Technologies

### Backend

* ASP.NET Core 8 Web API
* Entity Framework Core 8
* C#

### Database

* PostgreSQL
* Supabase

### Azure Integration Services

* Azure API Management (Planned)
* Azure Service Bus Topic (Planned)
* Azure Functions (Planned)
* Application Insights (Planned)

### DevOps

* Git
* GitHub
* Azure DevOps Pipelines (Planned)

---

## Solution Structure

```text
ManufacturingIntegrationPlatform

├── Erp.Api
├── Mes.Api
└── Shared.Contracts
```

---

## ERP API

### Features

* Create Production Order
* Get All Orders
* Get Order By Id
* Release Production Order

### Endpoints

```http
POST /api/orders
GET /api/orders
GET /api/orders/{id}
PUT /api/orders/{id}/release
```

### Order Lifecycle

```text
Draft
  ↓
Released
```

---

## MES API

### Features

* Create Job
* Get All Jobs
* Get Job By Id

### Endpoints

```http
POST /api/jobs
GET /api/jobs
GET /api/jobs/{id}
```

---

## Shared Contracts

### ProductionOrderReleasedEvent

```csharp
public record ProductionOrderReleasedEvent(
    string OrderNumber,
    string ProductCode,
    int Quantity,
    DateTime ReleasedAt);
```

This event represents a released production order and will be used for Azure Service Bus integration.

---

## Database Schema

### ERP

```text
manufacturing.orders
```

### MES

```text
manufacturing.jobs
```

---

## Current Status

### Completed

* ERP API
* MES API
* Entity Framework Core
* PostgreSQL Integration
* DTO Validation
* Release Workflow
* Event Contract
* Structured Logging
* GitHub Repository

### In Progress

* Azure Service Bus Topic Integration

### Planned

* Azure Function Consumer
* MES Integration Automation
* Retry Handling
* Dead Letter Queue
* Application Insights
* Correlation IDs
* Azure DevOps CI/CD

---

## Future Architecture

```text
ERP API
    ↓
Azure API Management
    ↓
Azure Service Bus Topic
    ↓
Azure Function
    ↓
MES API
    ↓
PostgreSQL
```
