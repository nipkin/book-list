import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { map, of, catchError } from 'rxjs';

export const authCheckGuard: CanActivateFn = (route, state) => {
  const auth = inject(AuthService);

  // skip check on login page
  if (state.url.includes('/login')) return of(true);

  return auth.checkAuth(true).pipe(
    map(() => true), // allow route
    catchError(() => of(true))
  );
};

export const authGuard: CanActivateFn = () => {
  const auth = inject(AuthService);
  const router = inject(Router);

  return auth.checkAuth(true).pipe(
    map(res => res.authenticated ? true : router.createUrlTree(['/login'])),
    catchError(() => of(router.createUrlTree(['/login'])))
  );
};

export const guestGuard: CanActivateFn = () => {
  const auth = inject(AuthService);
  const router = inject(Router);

  return auth.checkAuth(true).pipe(
    map(res => !res.authenticated ? true : router.createUrlTree(['/'])),
    catchError(() => of(true))
  );
};
