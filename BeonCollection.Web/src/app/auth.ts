import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { Observable, tap } from 'rxjs';

interface LoginResult {
  token: string;
}

const API_URL = 'https://localhost:7276/api';
const TOKEN_KEY = 'beon_token';

@Injectable({ providedIn: 'root' })
export class Auth {
  private http = inject(HttpClient);

  isLoggedIn = signal(this.hasToken());

  login(username: string, password: string): Observable<LoginResult> {
    return this.http
      .post<LoginResult>(`${API_URL}/auth/login`, { username, password })
      .pipe(
        tap(result => {
          localStorage.setItem(TOKEN_KEY, result.token);
          this.isLoggedIn.set(true);
        })
      );
  }

  logout(): void {
    localStorage.removeItem(TOKEN_KEY);
    this.isLoggedIn.set(false);
  }

  getToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }

  private hasToken(): boolean {
    return this.getToken() !== null;
  }
}