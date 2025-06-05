import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Reservation } from '../Model/reservation.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  private apiUrl = 'http://localhost:5190/api/Reservation';

  constructor(private http: HttpClient) { }

  getReservationById(id: number): Observable<Reservation> {
    return this.http.get<Reservation>(`${this.apiUrl}/GetReservation/${id}`);
  }

  startReservation(reservation: Reservation): Observable<any> {
    return this.http.post(`${this.apiUrl}/StartReservation`, reservation);
  }

  cancelReservation(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/CancelReservation/${id}`);
  }

  updateReservation(id: number, reservation: Reservation): Observable<any> {
    return this.http.put(`${this.apiUrl}/UpdateReservation/${id}`, reservation);
  }
}
