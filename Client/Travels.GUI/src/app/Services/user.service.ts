import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../Model/user.model';
import { ChangePasswordDto } from '../DTO/ChangePasswordDto'

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly apiUrl = 'http://localhost:5190/api/User'
  private readonly accountUrl = 'http://localhost:5190/api/Account'

  constructor(private readonly http: HttpClient) { }

  getUserById(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/${id}`)
  }

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}`)
  }

  deleteUser(id: number): Observable<any> {
    return this.http.delete(`${this.accountUrl}/${id}`, { responseType: 'text' as 'json' });
  }


  activateUser(id: number): Observable<any> {
    return this.http.patch(`${this.accountUrl}/activate/${id}`, null, { responseType: 'text' as 'json' });
  }

  deactivateUser(id: number): Observable<any> {
    return this.http.patch(`${this.accountUrl}/deactivate/${id}`, null, { responseType: 'text' as 'json' });
  }

  changePassword(dto: ChangePasswordDto): Observable<any> {
    return this.http.post(`${this.accountUrl}/change-password`, dto);
  }
}
