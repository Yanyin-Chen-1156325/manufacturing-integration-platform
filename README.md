# Manufacturing Integration Platform

## Overview

Manufacturing Integration Platform is a cloud-native, event-driven integration solution built on Microsoft Azure technologies.

The project demonstrates how a traditional manufacturing integration process can be modernized using asynchronous messaging, Azure Functions, and enterprise integration patterns.

The solution simulates the integration between an ERP system and a Manufacturing Execution System (MES), enabling automatic production job creation through Azure Service Bus messaging.

---

## Business Scenario

A manufacturing company releases production orders from an ERP system.

When an order is released:

1. ERP publishes a ProductionOrderReleased event.
2. Azure Service Bus receives the event.
3. Azure Function processes the message.
4. MES API automatically creates a production job.
5. Job data is stored in PostgreSQL.

This eliminates manual data transfer and enables reliable system-to-system integration.

---

## Legacy Architecture

```text
ERP

↓

Batch Import

↓

MES
```

---

## Modernized Architecture

```text
ERP API
   │
   ▼
Azure Service Bus Queue
   │
   ▼
Azure Function
(ProcessProductionOrderFunction)
   │
   ▼
MES API
   │
   ▼
PostgreSQL
```

---

## Failure Handling Architecture

```text
ERP API
   │
   ▼
Azure Service Bus Queue
   │
   ▼
ProcessProductionOrderFunction
   │
   ▼
MES API

Failure
   │
   ▼
Retry
   │
   ▼
Dead Letter Queue
   │
   ▼
ReprocessDeadLetterFunction
   │
   ▼
Main Queue
```

---

## Features

### ERP API

* Create Production Orders
* Retrieve Production Orders
* Release Production Orders
* Publish Integration Events to Azure Service Bus

### MES API

* Create Production Jobs
* Retrieve Production Jobs
* Request Validation
* PostgreSQL Persistence

### Azure Integration

* Event-Driven Architecture
* Azure Service Bus Queue Messaging
* Azure Functions (.NET 8 Isolated Worker)
* Dependency Injection
* HttpClientFactory
* Structured Logging

---

## Technology Stack

### Backend

* ASP.NET Core 8 Web API
* C#
* .NET 8
* Entity Framework Core 8

### Database

* PostgreSQL
* Supabase

### Azure Services

* Azure Service Bus
* Azure Functions
* Application Insights (In Progress)

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
├── Shared.Contracts
└── ManufacturingIntegration.Functions
```

---

## Integration Flow

### Successful Processing

```text
Create Order

↓

Release Order

↓

Azure Service Bus Queue

↓

Azure Function

↓

MES API

↓

PostgreSQL Jobs Table
```

---

## Retry Handling

When MES API is unavailable:

```text
Azure Function

↓

Processing Failure

↓

Azure Service Bus Retry

↓

DeliveryCount Increases

↓

Retry Processing
```

Verified successfully.

---

## Tested Scenarios

### Successful Integration

```text
ERP API
 ↓
Azure Service Bus
 ↓
Azure Function
 ↓
MES API
 ↓
PostgreSQL
```

### MES API Offline

```text
MES API Offline
 ↓
Function Failure
 ↓
Retry
 ↓
Dead Letter Queue
```

### Validation Failure

```text
Quantity = 111111

↓

MES API Validation Error

↓

400 Bad Request

↓

Retry

↓

Dead Letter Queue
```

---

## Current Status

### Completed

* ERP API
* MES API
* PostgreSQL Integration
* Entity Framework Core 8
* DTO Validation
* Event Contracts
* Azure Service Bus Integration
* Azure Functions
* Dependency Injection
* HttpClientFactory
* Structured Logging
* Retry Handling
* Dead Letter Queue Processing
* Dead Letter Queue Reprocessing
* End-to-End Integration Testing

### In Progress

* Application Insights
* Azure Deployment
* Azure DevOps CI/CD Pipeline
