import { inject, runInInjectionContext } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { map } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';

export const authGuard: CanActivateFn = (route, state) => {
  const router: Router = inject(Router);
  return inject(AccountService).currentUser$.pipe(
    map(auth => {
      if(auth) return true;
      else {
        router.navigate(['/account/login'], {queryParams:{returnUrl: state.url}})
        return false
      }
    })
  );
};
