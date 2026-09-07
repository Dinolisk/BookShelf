import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { BookService } from '../../core/book.service';

@Component({
  selector: 'app-book-form',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './book-form.html',
})
export class BookForm {
  private fb = inject(FormBuilder);
  private books = inject(BookService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  private readonly idParam = this.route.snapshot.paramMap.get('id');
  protected readonly isEdit = this.idParam !== null;
  protected readonly loading = signal(this.isEdit);
  protected readonly saving = signal(false);
  protected readonly error = signal<string | null>(null);

  protected readonly form = this.fb.nonNullable.group({
    title: ['', [Validators.required, Validators.maxLength(200)]],
    author: ['', [Validators.required, Validators.maxLength(200)]],
    publishedDate: ['', [Validators.required]],
  });

  constructor() {
    if (this.isEdit) {
      this.books.get(Number(this.idParam)).subscribe({
        next: (book) => {
          this.form.patchValue({
            title: book.title,
            author: book.author,
            publishedDate: book.publishedDate,
          });
          this.loading.set(false);
        },
        error: () => {
          this.error.set('Kunde inte hämta boken.');
          this.loading.set(false);
        },
      });
    }
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.saving.set(true);
    this.error.set(null);

    const value = this.form.getRawValue();
    const request$: Observable<unknown> = this.isEdit
      ? this.books.update(Number(this.idParam), value)
      : this.books.create(value);

    request$.subscribe({
      next: () => this.router.navigate(['/books']),
      error: () => {
        this.error.set('Kunde inte spara boken.');
        this.saving.set(false);
      },
    });
  }
}
