import { Component, inject } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';

@Component({
  selector: 'app-book-form',
  imports: [RouterLink],
  template: `
    <a routerLink="/books" class="btn btn-link px-0 mb-3">
      <i class="fa-solid fa-arrow-left me-1"></i>Tillbaka
    </a>
    <h1 class="h3 mb-4">{{ isEdit ? 'Redigera bok' : 'Ny bok' }}</h1>
    <p class="text-body-secondary">Formuläret byggs i steg 5.</p>
  `,
})
export class BookForm {
  private route = inject(ActivatedRoute);
  protected readonly isEdit = this.route.snapshot.paramMap.has('id');
}
