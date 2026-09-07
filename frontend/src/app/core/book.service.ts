import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Book, BookRequest } from './models';

@Injectable({ providedIn: 'root' })
export class BookService {
  private http = inject(HttpClient);
  private readonly api = `${environment.apiUrl}/api/books`;

  list(): Observable<Book[]> {
    return this.http.get<Book[]>(this.api);
  }

  get(id: number): Observable<Book> {
    return this.http.get<Book>(`${this.api}/${id}`);
  }

  create(book: BookRequest): Observable<Book> {
    return this.http.post<Book>(this.api, book);
  }

  update(id: number, book: BookRequest): Observable<void> {
    return this.http.put<void>(`${this.api}/${id}`, book);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }
}
