import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth.services';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);

  const router = inject(Router);
  const currentUser = authService.isLoggedIn();
  if (currentUser) {
    return true;
  }
  router.navigateByUrl('/admin/login');
  return false;
};
