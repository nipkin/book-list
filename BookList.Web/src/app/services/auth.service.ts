import { Injectable, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap, of, shareReplay } from 'rxjs';

export interface LoginRequest {
  username: string;
  password: string;
}

export interface UserRequest {
  password: string;
  username: string;
  confirmPassword: string;
}

export interface AuthResponse {
  success: boolean;
  errorMessage: string;
}

export interface AddUserResponse {
  success: boolean;
  errorMessage?: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = '/api/auth';

  private _isLoggedIn = signal<boolean>(false);
  private _isChecked = signal<boolean>(false);

  public isLoggedIn = computed(() => this._isLoggedIn());
  public isChecked = computed(() => this._isChecked());

  constructor(private http: HttpClient) { }

  checkAuth(forceRefresh = false): Observable<{ authenticated: boolean }> {
    if (this._isChecked() && !forceRefresh) {
      return of({ authenticated: this._isLoggedIn() });
    }

    return this.http.get<{ authenticated: boolean }>(this.apiUrl + '/check', { withCredentials: true }).pipe(
      tap(res => {
        this._isLoggedIn.set(res.authenticated);
        this._isChecked.set(true);
      })
    );
  }

  login(payload: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(this.apiUrl + '/login', payload, { withCredentials: true }).pipe(
      tap(res => {
        if (res.success) {
          this._isLoggedIn.set(true);
          this._isChecked.set(true);
        }
      })
    );
  }

  logout(): Observable<{ success: boolean }> {
    return this.http.post<{ success: boolean }>(
      `${this.apiUrl}/logout`,
      {},
      { withCredentials: true }
    ).pipe(
      tap(() => {
        this._isLoggedIn.set(false);
        this._isChecked.set(false);
      })
    );
  }

  clearSession() {
    this._isLoggedIn.set(false);
    this._isChecked.set(false);
  }

  addUser(payload: UserRequest): Observable<AddUserResponse> {
    return this.http.post<AddUserResponse>(this.apiUrl + '/add', payload);
  }
}
