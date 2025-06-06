import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenKey = 'token';

  private loggedIn = new BehaviorSubject<boolean>(this.isLoggedIn());
  authStatus = this.loggedIn.asObservable();

  setToken(token: string) {
    sessionStorage.setItem(this.tokenKey, token);
    this.loggedIn.next(true);
  }
  
  getToken(): string | null {
    return sessionStorage.getItem(this.tokenKey);
  }
  
  clearToken() {
    sessionStorage.removeItem(this.tokenKey);
    this.loggedIn.next(false);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  getUserId(): number | null {
    const token = this.getToken();
    if (!token) return null;

    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const idClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
      return payload[idClaim] ? Number(payload[idClaim]) : null;
    } catch (error) {
      console.error('Błąd podczas dekodowania tokena:', error);
      return null;
    }
  }

  isAdmin(): boolean {
    const token = this.getToken();
    if (!token) {
      console.log("Brak tokena, isAdmin = false");
      return false;
    }

    try {
      const payload = JSON.parse(atob(token.split('.')[1]));

      const roleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
      const roleValue = payload[roleClaim];

      if (!roleValue) return false;

      return roleValue === 'Admin';
    } catch (error) {
      console.error('Błąd podczas dekodowania tokena:', error);
      return false;
    }
  }



}
