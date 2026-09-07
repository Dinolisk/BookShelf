# BookQuotes – frontend

Angular 20 SPA för BookQuotesApp. Bootstrap 5 + Font Awesome, JWT i `localStorage` via HTTP-interceptor.

## Kör lokalt

```bash
npm install
npm start          # ng serve → http://localhost:4200
```

API:t förväntas på `http://localhost:5249` (se `src/environments/environment.development.ts`).
Starta backend först (se `../backend/README.md`).

## Bygg

```bash
npm run build      # → dist/bookquotes-web
```

## Struktur

```
src/app/
  core/         auth.service, auth.interceptor, models
  layout/       navbar (responsiv, kollapsar < md)
  features/
    books/      book-list, book-form
    quotes/     quote-list
    auth/       login, register
  app.routes.ts  lazy-laddade routes
```

## Routes

| Path | Vy |
|------|-----|
| `/books` | Boklista (startsida) |
| `/books/new` | Ny bok |
| `/books/:id/edit` | Redigera bok |
| `/quotes` | Mina citat |
| `/login`, `/register` | Autentisering |

`/books*` och `/quotes` skyddas av `authGuard` (skickar till `/login?returnUrl=…`).
`/login` och `/register` skyddas av `guestGuard` (redan inloggad → `/books`).
