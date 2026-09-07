# BookQuotesApp

Responsiv CRUD-webbapplikation: hantera böcker och favoritcitat, med användarinloggning via JWT.

## Teknik

| Del | Stack |
|-----|-------|
| Frontend | Angular 20, Bootstrap 5, Font Awesome |
| Backend | .NET 9 C# REST API, EF Core, PostgreSQL |
| Auth | JWT (register + login) |

## Struktur

```
BookQuotesApp/
  backend/    .NET 9 Web API + EF Core
  frontend/   Angular 20 SPA
```

## Funktioner

- Böcker: lista, lägg till, redigera, radera
- Inloggning/registrering med JWT-token, skyddade API-endpoints
- "Mina citat": lista, lägg till, redigera, radera citat
- Responsiv layout med kollapsande mobilmeny
- Ljus/mörk-tema-toggle

## Kom igång

Se `backend/README.md` och `frontend/README.md` (läggs till i respektive steg).

## Krav på utvecklingsmiljö

- .NET SDK 9
- Node.js 24 LTS + Angular CLI 20
- PostgreSQL (lokalt eller molnbaserat, t.ex. Neon)
