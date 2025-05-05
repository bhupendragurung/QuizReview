
# QuizReviewApplication

A modular, clean-architecture based ASP.NET Core application for managing quiz questions, categories, and AI-powered answer evaluations.

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

