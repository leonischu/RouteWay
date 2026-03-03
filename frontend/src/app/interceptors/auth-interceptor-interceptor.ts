import { HttpInterceptorFn } from '@angular/common/http';
import { Auth } from '../services/auth';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';

export const authInterceptorInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(Auth)
  const token = authService.getToken();

  // If token exist garcha vane check expiry first 
 if (token) {

    if (authService.isTokenExpired()) {
      authService.logout();
      return throwError(() => new Error('Token expired'));
    }

    const cloned = req.clone({
      headers: req.headers.set('Authorization', 'Bearer ' + token)
    });

    return next(cloned).pipe(
      catchError((error) => {

        //  If backend returns 401 then logout
        if (error.status === 401) {
          authService.logout();
        }

        return throwError(() => error);
      })
    );
  }

  return next(req);
};
