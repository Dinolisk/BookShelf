# BookQuotesApp API

.NET 9 Web API — böcker och citat, PostgreSQL via EF Core.

## Kör lokalt

1. Sätt hemligheterna (sparas utanför git via user-secrets).
   Connection string: både Neons URL-format och Npgsql:s nyckel-värde-format accepteras.

   ```bash
   cd src/BookQuotesApp.Api
   dotnet user-secrets set "ConnectionStrings:Default" "postgresql://user:pass@ep-xxx.eu-central-1.aws.neon.tech/neondb?sslmode=require"
   dotnet user-secrets set "Jwt:Key" "<minst 32 tecken slumpmässig sträng>"
   ```

2. Starta API:t (migrationer körs automatiskt vid uppstart):

   ```bash
   dotnet run
   ```

   Swagger UI: `https://localhost:<port>/swagger`

I produktion (Render) sätts samma värde som miljövariabeln `ConnectionStrings__Default`.

## Endpoints

| Metod | Rutt | Auth | Beskrivning |
|-------|------|------|-------------|
| GET | `/health` | – | Hälsokoll |
| POST | `/api/auth/register` | – | Skapa användare, returnerar JWT |
| POST | `/api/auth/login` | – | Logga in, returnerar JWT |
| GET | `/api/books` | JWT | Alla böcker (delas mellan användare) |
| GET | `/api/books/{id}` | JWT | En bok |
| POST | `/api/books` | JWT | Skapa bok |
| PUT | `/api/books/{id}` | JWT | Uppdatera bok |
| DELETE | `/api/books/{id}` | JWT | Radera bok |
| GET | `/api/quotes` | JWT | Inloggad användares citat |
| GET | `/api/quotes/{id}` | JWT | Ett av användarens citat |
| POST | `/api/quotes` | JWT | Skapa citat (kopplas till användaren) |
| PUT | `/api/quotes/{id}` | JWT | Uppdatera eget citat |
| DELETE | `/api/quotes/{id}` | JWT | Radera eget citat |

JWT skickas som `Authorization: Bearer <token>`. Token gäller i 8 timmar.
Skyddade endpoints utan giltig token svarar `401`. Swagger UI har en **Authorize**-knapp för att klistra in token.

## Projektstruktur

```
src/BookQuotesApp.Api/
  Controllers/   API-controllers (Auth, Books, Quotes, Health)
  Data/          AppDbContext, DatabaseConnection, Migrations
  Dtos/          Request/response-modeller
  Models/        EF-entiteter (Book, Quote, User)
  Services/      TokenService (JWT)
  Program.cs     App-uppstart, DI, auth, CORS, Swagger
```
