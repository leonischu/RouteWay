import { HttpInterceptorFn } from '@angular/common/http';
import { Auth } from '../services/auth';
import { inject } from '@angular/core';

export const authInterceptorInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(Auth)

  if(authService.getToken()){
    const cloned = req.clone({
      headers: req.headers.set('Authorization', 'Bearer ' + authService.getToken())
    })

    return next(cloned)
  }

  return next(req);
};
