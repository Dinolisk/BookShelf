import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Quote, QuoteRequest } from './models';

@Injectable({ providedIn: 'root' })
export class QuoteService {
  private http = inject(HttpClient);
  private readonly api = `${environment.apiUrl}/api/quotes`;

  list(): Observable<Quote[]> {
    return this.http.get<Quote[]>(this.api);
  }

  create(quote: QuoteRequest): Observable<Quote> {
    return this.http.post<Quote>(this.api, quote);
  }

  update(id: number, quote: QuoteRequest): Observable<void> {
    return this.http.put<void>(`${this.api}/${id}`, quote);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }
}
