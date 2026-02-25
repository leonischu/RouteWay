import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { Login } from '../auth/login/login';
import { Register } from '../auth/register/register';
import { jwtDecode } from 'jwt-decode';
import { UserDetail } from '../model/userDetail';

@Injectable({
  providedIn: 'root',
})
export class Auth {

  private apiUrl = 'http://localhost:5278/';

  constructor(private http: HttpClient) {}

  // =========================
  // AUTH API CALLS
  // =========================

  register(data: Register): Observable<any> {
    return this.http.post(`${this.apiUrl}api/Auth/register`, data);
  }

  login(data: Login): Observable<any> {
    return this.http
      .post(`${this.apiUrl}api/Auth/login`, data)
      .pipe(
        tap((response: any) => {
          if (response?.token && typeof response.token === 'string') {
            localStorage.setItem('token', response.token);
            if (response.user) {
              localStorage.setItem('user', JSON.stringify(response.user));
            }
          }
        })
      );
  }

  googleLogin(idToken: string): Observable<any> {
    return this.http.post<any>(
      `${this.apiUrl}api/Auth/google`,
      { idToken }
    ).pipe(
      tap((response: any) => {
        // ✅ IMPORTANT: store ONLY JWT, not whole response
        if (response?.token && typeof response.token === 'string') {
          localStorage.setItem('token', response.token);
        }
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }

  // =========================
  // TOKEN HELPERS
  // =========================

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  // =========================
  // SAFE JWT DECODING
  // =========================

  private decodeToken(): any | null {
    const token = this.getToken();
    if (!token) return null;

    // JWT must have 3 parts
    if (token.split('.').length !== 3) {
      console.error('Invalid JWT format');
      this.logout();
      return null;
    }

    try {
      return jwtDecode(token);
    } catch (error) {
      console.error('JWT Decode Failed:', error);
      this.logout();
      return null;
    }
  }

  // =========================
  // USER DATA (SAFE)
  // =========================

  getUserName(): UserDetail | null {
    const decodedToken = this.decodeToken();
    if (!decodedToken) return null;

    return {
      userId: decodedToken.id,
      fullName: decodedToken.name,
      email: decodedToken.email,
      role: decodedToken.role || [],
    };
  }

  getUserRole(): string | null {
    const decoded = this.decodeToken();
    if (!decoded) return null;

    return decoded.role || decoded.Role || null;
  }

  getUserId(): number {
    const decoded = this.decodeToken();
    if (!decoded) return 0;

    return Number(decoded.id) || 0;
  }

  getUserProfile(): Observable<UserDetail> {
    return this.http.get<UserDetail>(
      `${this.apiUrl}api/Auth/detail`
    );
  }
}