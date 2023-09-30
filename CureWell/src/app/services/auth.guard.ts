import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from './account.service';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService=inject(AccountService);
  const router=inject(Router);

  return accountService.isAuthenticated()?true: router.navigateByUrl("/login");
};
