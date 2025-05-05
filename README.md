# 📚 Quiz Review Application

An enterprise-grade ASP.NET Core Web API application to manage quiz questions, categories, and AI-evaluated student answers using Clean Architecture, MediatR, FluentValidation, and more.

---

## 🚀 Features

- Create, retrieve, and manage **Questions**, **Answers**, and **Categories**
- AI integration for automatic answer evaluation and feedback
- Clean Architecture with SOLID principles
- CQRS with **MediatR**
- FluentValidation with pipeline behavior
- Global Exception Handling middleware
- Unit tests with **xUnit**, **Moq**, and **FluentAssertions**

---

## 🛠️ Tech Stack

- **ASP.NET Core 7**
- **MediatR**
- **Entity Framework Core**
- **FluentValidation**
- **AutoMapper**
- **xUnit**, **Moq** (Unit Testing)
- **OpenAI / AI service** integration
- Clean Architecture + CQRS pattern

---

## 🗂️ Project Structure

QuizReviewApplication/
│
├── Application/
│ ├── Features/ # CQRS Handlers (Commands & Queries)
│ ├── Dtos/ # Data Transfer Objects
│ ├── Repositories/ # Interface definitions
│ ├── Services/ # AI evaluation logic
│ └── Helper/ # ApiResponse, ValidationBehavior
│
├── Domain/
│ ├── Entities/ # Core models (Question, Answer, Category)
│ └── Enum/ # Enums for Skill & Question level
│
├── Infrastructure/
│ └── Repositories/ # EF Core Implementations
│
├── WebApi/
│ ├── Controllers/ # API Controllers
│ ├── Middleware/ # Global Exception Handler
│ └── Program.cs # Entry point
│
├── Tests/
│ └── Handlers/ # Unit Tests for CQRS handlers
