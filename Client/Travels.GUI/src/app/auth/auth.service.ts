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
    localStorage.setItem(this.tokenKey, token);
    this.loggedIn.next(true);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  clearToken() {
    localStorage.removeItem(this.tokenKey);
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
}
