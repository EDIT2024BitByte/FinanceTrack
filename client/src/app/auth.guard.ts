import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: object
  ) { }

  canActivate(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      const userId = sessionStorage.getItem('userId');
      if (userId) {
        return true;
      } else {
        this.router.navigate(['/login']);
        return false;
      }
    } else {
      // Handle non-browser environments appropriately
      return false;
    }
  }
}
