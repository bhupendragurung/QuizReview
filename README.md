
# QuizReviewApplication

QuizReviewApplication is an educational platform built using ASP.NET Core, Entity Framework Core, and based on Clean Architecture principles. This project is a work-in-progress and aims to create a robust backend for a quiz review application with features such as authentication, quiz management, question/answer management, and more.

This project is an excellent way to practice building real-world applications while following industry-standard best practices for code architecture, testing, and development.
# Key Features:
Clean Architecture: Ensuring clear separation of concerns with distinct layers (Domain, Application, Infrastructure, WebApi).

CQRS: Command and Query Responsibility Segregation using MediatR for better scalability and maintainability.

FluentValidation: Used for input validation in commands and queries to keep the code clean and concise.

ASP.NET Identity: User authentication, email verification, and password management.

Unit Testing: Writing comprehensive tests using xUnit and Moq for mocking dependencies like EF Core DbContext.

API Layer: Built with ASP.NET Core Web API to expose endpoints for frontend interaction.
## 🔧 Technologies Used

- **ASP.NET Core** - Web API Framework
- **Entity Framework Core** - ORM for data access
- **MediatR** - CQRS implementation
- **FluentValidation** - Input validation
- **AutoMapper** - Object mapping
- **xUnit + Moq** - Unit testing
- **GoogleAi**  - AI integration for answer evaluation

## 📁 Project Structure

<pre>
QuizReviewApplication/
│
├── Application/
│   ├── Features/              # CQRS Handlers (Commands & Queries)
│   ├── Dtos/                  # Data Transfer Objects
│   ├── Repositories/          # Interface definitions
│   ├── Services/              # AI evaluation logic
│   └── Helper/                # ApiResponse, ValidationBehavior
│
├── Domain/
│   ├── Entities/              # Core models (Question, Answer, Category)
│   └── Enum/                  # Enums for Skill & Question level
│
├── Infrastructure/
│   └── Repositories/          # EF Core Implementations
│
├── WebApi/
│   ├── Controllers/           # API Controllers
│   ├── Middleware/            # Global Exception Handler
│   └── Program.cs             # Entry point
│
├── Tests/
│   └── Handlers/              # Unit Tests for CQRS handlers
</pre>

