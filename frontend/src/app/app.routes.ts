import { Routes } from '@angular/router';
import { authGuard, guestGuard } from './core/auth.guard';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'books' },
  {
    path: 'books',
    canActivate: [authGuard],
    loadComponent: () => import('./features/books/book-list').then((m) => m.BookList),
  },
  {
    path: 'books/new',
    canActivate: [authGuard],
    loadComponent: () => import('./features/books/book-form').then((m) => m.BookForm),
  },
  {
    path: 'books/:id/edit',
    canActivate: [authGuard],
    loadComponent: () => import('./features/books/book-form').then((m) => m.BookForm),
  },
  {
    path: 'quotes',
    canActivate: [authGuard],
    loadComponent: () => import('./features/quotes/quote-list').then((m) => m.QuoteList),
  },
  {
    path: 'login',
    canActivate: [guestGuard],
    loadComponent: () => import('./features/auth/login').then((m) => m.Login),
  },
  {
    path: 'register',
    canActivate: [guestGuard],
    loadComponent: () => import('./features/auth/register').then((m) => m.Register),
  },
  { path: '**', redirectTo: 'books' },
];
