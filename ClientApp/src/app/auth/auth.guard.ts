import { CanActivateFn, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';
import { inject } from '@angular/core';


export const authGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
 ) => {
  
       const authService: AuthService = inject(AuthService)
       const router: Router = inject(Router);

     if (!authService.currentUser)
       router.navigate(['/register-passenger', {requestedUrl: state.url}])
        return true;
 
};
