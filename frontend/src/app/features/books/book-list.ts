import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-book-list',
  imports: [RouterLink],
  template: `
    <div class="d-flex justify-content-between align-items-center mb-4">
      <h1 class="h3 mb-0"><i class="fa-solid fa-book me-2"></i>Böcker</h1>
      <a routerLink="/books/new" class="btn btn-primary">
        <i class="fa-solid fa-plus me-1"></i>Lägg till ny bok
      </a>
    </div>
    <p class="text-body-secondary">Boklistan byggs i steg 5.</p>
  `,
})
export class BookList {}
