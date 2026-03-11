import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { catchError, throwError } from 'rxjs';

/**
 * Clears session if server responds 401 (unauthorized)
 */
export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(AuthService);
  const router = inject(Router);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401) {
        // Don't redirect if it's just the auth check
        if (!req.url.includes('/auth/check')) {
          auth.clearSession();
          router.navigate(['/login'], {
            queryParams: { reason: 'session_expired' }
          });
        } else {
          auth.clearSession();
        }
      }
      return throwError(() => error);
    })
  );
};
