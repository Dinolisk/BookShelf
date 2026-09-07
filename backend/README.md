# BookQuotesApp API

.NET 9 Web API — böcker och citat, PostgreSQL via EF Core.

## Kör lokalt

1. Sätt connection string till Neon-databasen (sparas utanför git via user-secrets):

   ```bash
   cd src/BookQuotesApp.Api
   dotnet user-secrets set "ConnectionStrings:Default" "Host=...;Database=...;Username=...;Password=...;SSL Mode=Require"
   ```

2. Kör migrationerna mot databasen:

   ```bash
   dotnet ef database update
   ```

3. Starta API:t:

   ```bash
   dotnet run
   ```

   Swagger UI: `https://localhost:<port>/swagger`

## Endpoints (steg 2)

| Metod | Rutt | Beskrivning |
|-------|------|-------------|
| GET | `/api/books` | Alla böcker |
| GET | `/api/books/{id}` | En bok |
| POST | `/api/books` | Skapa bok |
| PUT | `/api/books/{id}` | Uppdatera bok |
| DELETE | `/api/books/{id}` | Radera bok |
| GET | `/api/quotes` | Alla citat |
| GET | `/api/quotes/{id}` | Ett citat |
| POST | `/api/quotes` | Skapa citat |
| PUT | `/api/quotes/{id}` | Uppdatera citat |
| DELETE | `/api/quotes/{id}` | Radera citat |

Auth (JWT) och skydd av endpoints läggs till i steg 3.

## Projektstruktur

```
src/BookQuotesApp.Api/
  Controllers/   API-controllers
  Data/          AppDbContext + Migrations
  Dtos/          Request/response-modeller
  Models/        EF-entiteter
  Program.cs     App-uppstart, DI, CORS, Swagger
```
