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
}
