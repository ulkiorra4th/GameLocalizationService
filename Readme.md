# Game Localization

Game Localization is a platform for managing the localization of game projects. It provides a centralized system for storing, editing, and translating game texts, supporting multiple languages and projects, with a convenient web interface and REST API.

![image](https://github.com/user-attachments/assets/cf07c083-ac03-4250-8eb5-a5897f287d6e)


---

## Features

- Multi-layer architecture (Frontend + Backend)
- REST API with versioning
- Project, language, key, and translation management
- Pagination and key search
- Swagger API documentation
- Dockerized deployment

---

## Project Structure

```
GameLocalization/
  backend/                # Backend (ASP.NET Core, C#)
    GameLocalization/     # Main backend project
    GameLocalization.Application/   # DTOs, services, interfaces
    GameLocalization.Common/        # Common classes, enums, Result
    GameLocalization.Domain/        # Domain models
    GameLocalization.Infrastructure.Mapping/ # Mappers
    GameLocalization.Persistence.Postgres/   # PostgreSQL persistence
  frontend/
    gamelocalization_frontend/      # Frontend (React + TypeScript)
      ├── src/
      │   ├── api/                  # API clients (axios)
      │   ├── hooks/                # Custom React hooks
      │   ├── pages/                # Page components (e.g., ProjectPage)
      │   ├── utils/                # Utility functions and types
      │   ├── App.tsx               # Main app component
      │   └── main.tsx              # Entry point
      ├── public/                   # Static assets
      ├── index.html                # HTML template
      ├── package.json              # Frontend dependencies
      ├── tailwind.config.js        # Tailwind CSS config
      └── ...                       # Other config files
  docker-compose.yaml               # Docker orchestration
```

---

## ER-model

![game_cloud_ER drawio](https://github.com/user-attachments/assets/2a17da6d-ea45-4e25-8a24-6c521f8d0a6a)

---

## Quick Start

### 1. Clone the repository

```bash
git clone <repository-url>
cd GameLocalizationService
```

### 2. Run with Docker

```bash
docker-compose up --build
```

- Backend: http://localhost:8080 (or port specified in compose.yaml)
- Frontend: http://localhost:8082

### 3. Run backend locally

```bash
cd backend/GameLocalization/GameLocalization
dotnet run
```

### 4. Run frontend locally

```bash
cd frontend/gamelocalization_frontend
npm install
npm run dev
```

---

## API Documentation

Swagger UI:  
`http://localhost:8080/swagger`

---

## Main Entities

- **Project** — Game project
- **Language** — Localization language
- **Key** — Localization key (string identifier)
- **Translation** — Translation for a key in a specific language

---

## Technologies

- **Backend:** ASP.NET Core 8, PostgreSQL, EF Core, Serilog, Swagger, Docker
- **Frontend:** React, TypeScript, Tailwindcss, Axios, Vite, Docker

---

## CI/CD (Template)

- Build and test backend with `dotnet build` and `dotnet test`
- Build frontend with `npm run build`
- Docker images for both frontend and backend

## License

MIT License

---

## Contacts

- Project author: Ramin Mukhtarov (ramin.muhtarov@gmail.com)
