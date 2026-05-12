# Proxinex 🌌

Enterprise AI orchestration platform built with:

- .NET 10
- Semantic Kernel
- Ollama
- Redis
- PostgreSQL
- Qdrant
- OpenTelemetry
- Serilog

## Features

- AI Chat APIs
- Streaming Responses
- Conversation Memory
- Model Routing
- Enterprise Observability
- RAG-ready Architecture

## Tech Stack

- ASP.NET Core
- Semantic Kernel
- Ollama
- Redis
- OpenTelemetry
- Serilog

## Status

🚧 In Active Development

----------

🚀 Proxinex — Enterprise AI Orchestration Platform 🌌
# 🚀 Proxinex

> Enterprise-grade AI orchestration platform built using .NET 10, Semantic Kernel, Ollama, PostgreSQL, Redis, Qdrant, OpenTelemetry, and modern AI infrastructure patterns.

---

# 🌌 Vision

Proxinex is designed as a next-generation AI orchestration platform capable of:

- Multi-model AI routing
- Enterprise RAG
- AI agents
- Memory systems
- Streaming AI responses
- LLMOps
- Observability
- Token tracking
- AI governance
- Multimodal AI architecture

The platform evolves beyond a simple chatbot into a full AI Operating System.

---

# 🧠 AI MODEL STRATEGY

## Open Source Models

| Model | Purpose |
|---|---|
| Phi-3 Mini | Lightweight enterprise reasoning |
| TinyLlama | Cheap general tasks |
| Qwen 2.5 | Coding + long-context reasoning |
| DeepSeek | Advanced coding |
| Kimi | Long-context intelligence |

---

## Closed Source Models

| Model | Purpose |
|---|---|
| GPT-4o | Premium reasoning |
| Azure OpenAI | Enterprise AI |
| Claude | Long-context orchestration |
| Gemini | Vision + multimodal |
| Grok | Real-time reasoning |

---

# 🏗️ SOLUTION STRUCTURE

```text
Proxinex/
│
├── backend/
│   │
│   ├── Proxinex.ChatService/
│   ├── Proxinex.AgentService/
│   ├── Proxinex.RagService/
│   ├── Proxinex.MemoryService/
│   ├── Proxinex.LlmOpsService/
│   ├── Proxinex.gateway/
│   │
│   └── shared/
│       ├── Proxinex.Shared.Contracts/
│       ├── Proxinex.Shared.Infrastructure/
│       └── Proxinex.Shared.SemanticKernel/
│
├── frontend/
│
├── ai-models/
│
├── infrastructure/
│   ├── core/
│   ├── observability/
│   └── ai-models/
│
└── docs/
⚙️ TECH STACK
Backend
.NET 10
ASP.NET Core Web API
Semantic Kernel
MediatR
FluentValidation
YARP Gateway
AI
Ollama
Semantic Kernel
OpenAI
Azure OpenAI
Claude
Gemini
Grok
Databases
PostgreSQL
Redis
Qdrant Vector DB
Observability
OpenTelemetry
Grafana
Prometheus
Loki
Tempo
Seq
Infrastructure
Docker
Docker Compose
Kubernetes (Future)
Terraform (Future)
📦 REQUIRED INSTALLATIONS
1️⃣ Install .NET 10 SDK

https://dotnet.microsoft.com/en-us/download

Verify:

dotnet --version
2️⃣ Install VS Code

https://code.visualstudio.com/

3️⃣ Install Docker Desktop

https://www.docker.com/products/docker-desktop/

Verify:

docker --version
docker compose version
4️⃣ Install Ollama

https://ollama.com/download

Verify:

ollama --version
🧩 VS CODE EXTENSIONS

Install:

C#
C# Dev Kit
Docker
REST Client
GitLens
Thunder Client
PostgreSQL
YAML
Error Lens
🚀 CLONE PROJECT
git clone <YOUR_GITHUB_REPO>
cd Proxinex
🏗️ CREATE SOLUTION
mkdir backend
cd backend

dotnet new sln -n Proxinex
🚀 CREATE SERVICES
dotnet new webapi -n Proxinex.ChatService
dotnet new webapi -n Proxinex.AgentService
dotnet new webapi -n Proxinex.RagService
dotnet new webapi -n Proxinex.MemoryService
dotnet new webapi -n Proxinex.LlmOpsService
dotnet new webapi -n Proxinex.gateway
🚀 CREATE SHARED PROJECTS
mkdir shared
cd shared

dotnet new classlib -n Proxinex.Shared.Contracts
dotnet new classlib -n Proxinex.Shared.Infrastructure
dotnet new classlib -n Proxinex.Shared.SemanticKernel
🚀 ADD PROJECTS TO SOLUTION
cd ..

dotnet sln add Proxinex.ChatService
dotnet sln add Proxinex.AgentService
dotnet sln add Proxinex.RagService
dotnet sln add Proxinex.MemoryService
dotnet sln add Proxinex.LlmOpsService
dotnet sln add Proxinex.gateway

dotnet sln add shared/Proxinex.Shared.Contracts
dotnet sln add shared/Proxinex.Shared.Infrastructure
dotnet sln add shared/Proxinex.Shared.SemanticKernel
📦 INSTALL CORE PACKAGES
🧠 SEMANTIC KERNEL

Install inside:

shared/Proxinex.Shared.SemanticKernel
dotnet add package Microsoft.SemanticKernel
dotnet add package Microsoft.SemanticKernel.Connectors.OpenAI
dotnet add package Microsoft.SemanticKernel.Connectors.Ollama --prerelease
⚙️ CONFIGURATION PACKAGES

Install inside:

shared/Proxinex.Shared.Infrastructure
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Options.ConfigurationExtensions
🐘 POSTGRESQL

Install inside:

shared/Proxinex.Shared.Infrastructure
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Design
🔴 REDIS

Install inside:

shared/Proxinex.Shared.Infrastructure
dotnet add package StackExchange.Redis
🧠 QDRANT

Install inside:

shared/Proxinex.Shared.Infrastructure
dotnet add package Qdrant.Client
📡 OPENTELEMETRY

Install inside ALL APIs:

Proxinex.ChatService
Proxinex.AgentService
Proxinex.RagService
Proxinex.MemoryService
Proxinex.LlmOpsService
Proxinex.gateway
dotnet add package OpenTelemetry.Extensions.Hosting
dotnet add package OpenTelemetry.Instrumentation.AspNetCore
dotnet add package OpenTelemetry.Exporter.OpenTelemetryProtocol
🪵 SERILOG

Install inside ALL APIs:

dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File
🌉 YARP GATEWAY

Install inside:

Proxinex.gateway
dotnet add package Yarp.ReverseProxy
🧪 VALIDATION

Install inside APIs:

dotnet add package FluentValidation.AspNetCore
🔄 MEDIATR

Install inside APIs:

dotnet add package MediatR
🐳 DOCKER INFRASTRUCTURE
infrastructure/core/docker-compose.yml
services:

  postgres:
    image: postgres:16
    container_name: proxinex-postgres
    environment:
      POSTGRES_USER: proxinex
      POSTGRES_PASSWORD: proxinex
      POSTGRES_DB: proxinex
    ports:
      - "5432:5432"

  redis:
    image: redis:7
    container_name: proxinex-redis
    ports:
      - "6379:6379"

  qdrant:
    image: qdrant/qdrant:latest
    container_name: proxinex-qdrant
    ports:
      - "6333:6333"
      - "6334:6334"

  pgadmin:
    image: dpage/pgadmin4
    container_name: proxinex-pgadmin
    environment:
      PGADMIN_DEFAULT_EMAIL: admin@proxinex.ai
      PGADMIN_DEFAULT_PASSWORD: admin
    ports:
      - "8080:80"

  rabbitmq:
    image: rabbitmq:3-management
    container_name: proxinex-rabbitmq
    ports:
      - "5672:5672"
      - "15672:15672"

  minio:
    image: minio/minio
    container_name: proxinex-minio
    command: server /data --console-address ":9001"
    environment:
      MINIO_ROOT_USER: admin
      MINIO_ROOT_PASSWORD: password123
    ports:
      - "9000:9000"
      - "9001:9001"
📊 OBSERVABILITY STACK
infrastructure/observability/docker-compose.yml
services:

  prometheus:
    image: prom/prometheus
    container_name: proxinex-prometheus
    ports:
      - "9090:9090"

  grafana:
    image: grafana/grafana
    container_name: proxinex-grafana
    ports:
      - "3000:3000"

  loki:
    image: grafana/loki
    container_name: proxinex-loki
    ports:
      - "3100:3100"

  tempo:
    image: grafana/tempo
    container_name: proxinex-tempo
    ports:
      - "3200:3200"
🚀 START INFRASTRUCTURE
Core Infrastructure
cd infrastructure/core
docker compose up -d
Observability Stack
cd infrastructure/observability
docker compose up -d
🌌 LOCAL SERVICE URLS
Service	URL
PgAdmin	http://localhost:8080

RabbitMQ	http://localhost:15672

Qdrant Dashboard	http://localhost:6333/dashboard

Grafana	http://localhost:3000

Prometheus	http://localhost:9090

Loki	http://localhost:3100

Tempo	http://localhost:3200

MinIO	http://localhost:9001
🤖 OLLAMA MODELS

Pull lightweight models:

ollama pull phi3:mini
ollama pull tinyllama

Verify:

ollama list
🚀 RUN CHAT SERVICE
cd backend/Proxinex.ChatService
dotnet run

Swagger:

http://localhost:5121/swagger
🧠 ENTITY FRAMEWORK MIGRATIONS
Install EF Tool
dotnet tool install --global dotnet-ef
Create Migration
dotnet ef migrations add InitialChatHistory \
--project ../shared/Proxinex.Shared.Infrastructure \
--startup-project .
Update Database
dotnet ef database update \
--project ../shared/Proxinex.Shared.Infrastructure \
--startup-project .
🚀 SAMPLE CHAT REQUEST
{
  "prompt": "Generate Redis caching code in C#",
  "conversationId": "conv-001",
  "systemPrompt": "You are an expert .NET engineer.",
  "temperature": 0.3,
  "stream": false
}
🚀 SAMPLE STREAM REQUEST
{
  "prompt": "Explain Semantic Kernel simply",
  "conversationId": "stream-001",
  "systemPrompt": "You are an enterprise AI architect.",
  "temperature": 0.5,
  "stream": true
}
🧠 MODEL ROUTING
Task Type	Model
Cheap Tasks	TinyLlama
Coding Tasks	Phi-3 Mini
Enterprise Tasks	Phi-3 Mini
Long Context	Phi-3 Mini
📡 LLMOPS METRICS

Tracked Metrics:

Prompt logs
Response logs
Token usage
Latency
Model performance
Retrieval quality
Hallucinations
Agent traces
Cost tracking
Safety violations
🔥 FUTURE ROADMAP
Dynamic Kernel Factory
Multi-provider orchestration
AI agents
Enterprise RAG
Voice AI
Video generation
AI workflows
Kubernetes deployment
Terraform provisioning
MCP support
LangGraph integration
Langfuse integration
🧩 FINAL ARCHITECTURE
User Request
    │
    ▼
API Gateway
    │
    ▼
Chat Orchestration Service
    │
    ├── AI Router
    │       ├── TinyLlama
    │       ├── Phi3
    │       ├── OpenAI
    │       ├── Claude
    │       └── Gemini
    │
    ├── Semantic Kernel
    │
    ├── Memory Layer
    │       ├── Redis
    │       ├── PostgreSQL
    │       └── Qdrant
    │
    ├── AI Agents
    │
    ├── Plugins
    │
    └── Observability
            ├── OpenTelemetry
            ├── Grafana
            ├── Loki
            ├── Tempo
            └── Prometheus
🌌 Proxinex

Building the AI Operating System for enterprise intelligence 🚀