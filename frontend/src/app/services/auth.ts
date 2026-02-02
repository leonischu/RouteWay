import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  private apiUrl = 'http://localhost:5278/'; 

  constructor(private http: HttpClient) {}

  // Register new user
  register(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}api/Auth/register`, data);
  }

  // Login
  login(email: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}api/Auth/login`, { email, password })
      .pipe(
        tap((response: any) => {
          // Save token to localStorage
          if (response.token) {
            localStorage.setItem('token', response.token);
            localStorage.setItem('user', JSON.stringify(response.user));
          }
        })
      );
  }

  // Logout
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }

  // Check if user is logged in
  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  // Get token
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // Get user name
  getUserName(): string {
    const user = localStorage.getItem('user');
    if (user) {
      return JSON.parse(user).name || 'User';
    }
    return 'Guest';
  }

  // Get user role (for admin check)
  getUserRole(): string {
    const user = localStorage.getItem('user');
    if (user) {
      return JSON.parse(user).role || 'user';
    }
    return 'guest';
  }
  
}
