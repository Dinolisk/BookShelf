import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'books' },
  {
    path: 'books',
    loadComponent: () => import('./features/books/book-list').then((m) => m.BookList),
  },
  {
    path: 'books/new',
    loadComponent: () => import('./features/books/book-form').then((m) => m.BookForm),
  },
  {
    path: 'books/:id/edit',
    loadComponent: () => import('./features/books/book-form').then((m) => m.BookForm),
  },
  {
    path: 'quotes',
    loadComponent: () => import('./features/quotes/quote-list').then((m) => m.QuoteList),
  },
  {
    path: 'login',
    loadComponent: () => import('./features/auth/login').then((m) => m.Login),
  },
  {
    path: 'register',
    loadComponent: () => import('./features/auth/register').then((m) => m.Register),
  },
  { path: '**', redirectTo: 'books' },
];
