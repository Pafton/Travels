import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../Model/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly apiUrl = 'http://localhost:5190/api/User'

  constructor(private readonly http:HttpClient) { }

  getUserById(id: number): Observable <User> {
    return this.http.get<User>(`${this.apiUrl}/${id}`)
  }

  getAllUsers(): Observable<User[]>{
    return this.http.get<User[]>(`${this.apiUrl}`)
  }
}
