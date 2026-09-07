import { Component, computed, inject, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { BookService } from '../../core/book.service';
import { Book } from '../../core/models';
import { PageHeader } from '../../layout/page-header';

@Component({
  selector: 'app-book-list',
  imports: [RouterLink, DatePipe, PageHeader],
  templateUrl: './book-list.html',
})
export class BookList {
  private books = inject(BookService);

  protected readonly items = signal<Book[]>([]);
  protected readonly loading = signal(true);
  protected readonly error = signal<string | null>(null);
  protected readonly deletingId = signal<number | null>(null);
  protected readonly filter = signal('');

  protected readonly visible = computed(() => {
    const term = this.filter().trim().toLowerCase();
    if (!term) {
      return this.items();
    }
    return this.items().filter(
      (b) => b.title.toLowerCase().includes(term) || b.author.toLowerCase().includes(term),
    );
  });

  constructor() {
    this.load();
  }

  load(): void {
    this.loading.set(true);
    this.error.set(null);
    this.books.list().subscribe({
      next: (books) => {
        this.items.set(books);
        this.loading.set(false);
      },
      error: () => {
        this.error.set('Kunde inte hämta böckerna.');
        this.loading.set(false);
      },
    });
  }

  remove(book: Book): void {
    if (!confirm(`Radera "${book.title}"?`)) {
      return;
    }
    this.deletingId.set(book.id);
    this.books.delete(book.id).subscribe({
      next: () => {
        this.items.update((list) => list.filter((b) => b.id !== book.id));
        this.deletingId.set(null);
      },
      error: () => {
        this.error.set('Kunde inte radera boken.');
        this.deletingId.set(null);
      },
    });
  }

  onFilter(event: Event): void {
    this.filter.set((event.target as HTMLInputElement).value);
  }
}
