# ToDo List API

A RESTful To-Do List API built with ASP.NET Core Web API following Clean Architecture principles.

## Features

- JWT-based Authentication
- Role-based Authorization (Owner vs Guest)
- Task Management (CRUD operations)
- Task Filtering, Searching, and Pagination
- Category and Priority support for tasks
- Input Validation using FluentValidation
- AutoMapper for DTOs
- Global Error Handling and Logging
- Swagger UI Documentation
- Docker Support

## Prerequisites

- .NET 8.0 SDK
- Docker and Docker Compose (for containerized deployment)
- SQL Server (for local development)

## Getting Started

### Local Development

1. Clone the repository
2. Update the connection string in `appsettings.json`
3. Run the following commands:

```bash
dotnet restore
dotnet build
dotnet run --project ToDo.WebAPI
```

### Docker Deployment

1. Build and run using Docker Compose:

```bash
docker-compose up --build
```

The API will be available at:
- HTTP: http://localhost:8080
- HTTPS: https://localhost:8081
- Swagger UI: https://localhost:8081/swagger

## API Documentation

The API documentation is available through Swagger UI at `/swagger` when running the application.

### Authentication

1. Register a new user:
   ```http
   POST /api/auth/register
   Content-Type: application/json

   {
     "username": "testuser",
     "email": "test@example.com",
     "password": "password123",
     "role": "Owner"
   }
   ```

2. Login to get JWT token:
   ```http
   POST /api/auth/login
   Content-Type: application/json

   {
     "username": "testuser",
     "password": "password123"
   }
   ```

3. Use the token in subsequent requests:
   ```http
   Authorization: Bearer <your_token>
   ```

### Task Management

- Create Task: `POST /api/tasks`
- Get Tasks: `GET /api/tasks`
- Get Task by ID: `GET /api/tasks/{id}`
- Update Task: `PUT /api/tasks/{id}`
- Delete Task: `DELETE /api/tasks/{id}`
- Toggle Task Completion: `PATCH /api/tasks/{id}/toggle`

## Testing

Run the tests using:

```bash
dotnet test
```

## Project Structure

- `ToDo.Domain`: Core business logic and entities
- `ToDo.Application`: Application services, DTOs, and interfaces
- `ToDo.Infrastructure`: Database context, repositories, and external services
- `ToDo.WebAPI`: Controllers, middleware, and API configuration

## License

This project is licensed under the MIT License - see the LICENSE file for details.
