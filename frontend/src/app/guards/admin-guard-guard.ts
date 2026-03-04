import { CanActivateFn, Router } from '@angular/router';
import { Auth } from '../services/auth';
import { inject } from '@angular/core';

export const adminGuard: CanActivateFn = (route, state) => {
  const auth = inject(Auth);
  const router = inject(Router);

  if (!auth.isLoggedIn()) {
    router.navigate(['/login']);
    return false;
  }

  const user = auth.getUserName();
  if (user?.role === 'Admin') {
    return true;
  }

  // Logged in but not admin — redirect to user page
  router.navigate(['/User']);
  return false;
};