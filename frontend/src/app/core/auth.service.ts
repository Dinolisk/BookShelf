import { Injectable, computed, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthResponse } from './models';

const TOKEN_KEY = 'bq.token';
const USER_KEY = 'bq.username';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly api = `${environment.apiUrl}/api/auth`;

  private readonly _token = signal<string | null>(this.read(TOKEN_KEY));
  private readonly _username = signal<string | null>(this.read(USER_KEY));

  readonly username = this._username.asReadonly();
  readonly isLoggedIn = computed(() => this._token() !== null);

  constructor(private http: HttpClient) {}

  register(username: string, password: string): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(`${this.api}/register`, { username, password })
      .pipe(tap((res) => this.store(res)));
  }

  login(username: string, password: string): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(`${this.api}/login`, { username, password })
      .pipe(tap((res) => this.store(res)));
  }

  logout(): void {
    this._token.set(null);
    this._username.set(null);
    this.remove(TOKEN_KEY);
    this.remove(USER_KEY);
  }

  get token(): string | null {
    return this._token();
  }

  private store(res: AuthResponse): void {
    this._token.set(res.token);
    this._username.set(res.username);
    this.write(TOKEN_KEY, res.token);
    this.write(USER_KEY, res.username);
  }

  // localStorage can throw (private mode, disabled storage) — never let it break the app.
  private read(key: string): string | null {
    try {
      return localStorage.getItem(key);
    } catch {
      return null;
    }
  }

  private write(key: string, value: string): void {
    try {
      localStorage.setItem(key, value);
    } catch {
      /* ignore */
    }
  }

  private remove(key: string): void {
    try {
      localStorage.removeItem(key);
    } catch {
      /* ignore */
    }
  }
}
