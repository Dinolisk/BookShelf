import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { QuoteService } from '../../core/quote.service';
import { Quote } from '../../core/models';
import { PageHeader } from '../../layout/page-header';

@Component({
  selector: 'app-quote-list',
  imports: [ReactiveFormsModule, PageHeader],
  templateUrl: './quote-list.html',
})
export class QuoteList {
  private quotes = inject(QuoteService);
  private fb = inject(FormBuilder);

  protected readonly items = signal<Quote[]>([]);
  protected readonly loading = signal(true);
  protected readonly error = signal<string | null>(null);

  protected readonly formOpen = signal(false);
  protected readonly editingId = signal<number | null>(null);
  protected readonly saving = signal(false);
  protected readonly deletingId = signal<number | null>(null);

  protected readonly form = this.fb.nonNullable.group({
    text: ['', [Validators.required, Validators.maxLength(1000)]],
    author: ['', [Validators.maxLength(200)]],
  });

  constructor() {
    this.load();
  }

  load(): void {
    this.loading.set(true);
    this.error.set(null);
    this.quotes.list().subscribe({
      next: (quotes) => {
        this.items.set(quotes);
        this.loading.set(false);
      },
      error: () => {
        this.error.set('Kunde inte hämta citaten.');
        this.loading.set(false);
      },
    });
  }

  startAdd(): void {
    this.editingId.set(null);
    this.form.reset({ text: '', author: '' });
    this.formOpen.set(true);
  }

  startEdit(quote: Quote): void {
    this.editingId.set(quote.id);
    this.form.reset({ text: quote.text, author: quote.author ?? '' });
    this.formOpen.set(true);
  }

  cancel(): void {
    this.formOpen.set(false);
    this.editingId.set(null);
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.saving.set(true);
    this.error.set(null);

    const raw = this.form.getRawValue();
    const payload = { text: raw.text.trim(), author: raw.author.trim() || null };
    const id = this.editingId();

    const request$: Observable<unknown> =
      id === null ? this.quotes.create(payload) : this.quotes.update(id, payload);

    request$.subscribe({
      next: () => {
        this.saving.set(false);
        this.cancel();
        this.load();
      },
      error: () => {
        this.error.set('Kunde inte spara citatet.');
        this.saving.set(false);
      },
    });
  }

  remove(quote: Quote): void {
    if (!confirm('Radera citatet?')) {
      return;
    }
    this.deletingId.set(quote.id);
    this.quotes.delete(quote.id).subscribe({
      next: () => {
        this.items.update((list) => list.filter((q) => q.id !== quote.id));
        this.deletingId.set(null);
      },
      error: () => {
        this.error.set('Kunde inte radera citatet.');
        this.deletingId.set(null);
      },
    });
  }
}
