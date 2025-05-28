import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private apiUrl = 'http://localhost:5190/api/Account';

  constructor(private http: HttpClient) { }


  login(credentials: any): Observable<string> {
    return this.http.post('http://localhost:5190/api/Account/login', credentials, { responseType: 'text' });
  }

  register(registerDto: { email: string; password: string; }): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, registerDto);
  }
}
