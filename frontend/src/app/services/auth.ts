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

  // Register new user
  register(data:Register ): Observable<any> {
    return this.http.post(`${this.apiUrl}api/Auth/register`, data);
  }

  // Login
  login(data:Login): Observable<UserDetail> {
    return this.http.post(`${this.apiUrl}api/Auth/login`, data)
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

  // getMyDetails():Observable<UserDetail>{
  //   const token = localStorage.getItem('token')
  //   const body = {
  //     token:token
  //   }
  //   return this.http.post<any>
  // }




  // Get user name
  getUserName = () =>{
  // getUserName$(): Observable<UserDetail> {

    const token = this.getToken();
    if(!token) return null;
    const decodedToken : any = jwtDecode(token);
    console.log('DECODED TOKEN', decodedToken);

    const userDetail ={
      id:decodedToken.id,
       FullName: decodedToken.name,
      email: decodedToken.email,
      role: decodedToken.role || [],
    };
    return userDetail;

  };
  
  getUserProfile(): Observable<UserDetail>{
    return this.http.get<UserDetail>(`${this.apiUrl}api/Auth/detail`);
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
