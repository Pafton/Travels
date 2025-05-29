import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private apiUrl = 'http://localhost:5190/api/Account';
  private authUrl = 'http://localhost:5190/api/Auth'

  constructor(private http: HttpClient) { }


  login(credentials: any): Observable<string> {
    return this.http.post('http://localhost:5190/api/Account/login', credentials, { responseType: 'text' });
  }

  register(registerDto: { name: string; surname: string; email: string; password: string; }): Observable<string> {
    return this.http.post(`${this.apiUrl}/register`, registerDto, { responseType: 'text' });
  }

  sendPasswordResetLink(email: string): Observable<string> {
    return this.http.post(`${this.authUrl}/send-password-reset-link`,JSON.stringify(email), { responseType: 'text', headers: { 'Content-Type': 'application/json' } });
  }

  resetPassword(token: string, newPassword: string) {
    return this.http.post(`${this.authUrl}/reset-password`, { token, newPassword }, { responseType: 'text' });
  }
  
}
