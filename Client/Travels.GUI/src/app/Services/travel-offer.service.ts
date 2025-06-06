import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TravelOffer } from '../Model/travelOffer.model';
import { Observable } from 'rxjs';
import { Destination } from '../Model/destination.model';

@Injectable({
  providedIn: 'root'
})
export class TravelOfferService {

  private apiUrl = 'http://localhost:5190/api/TravelOffer';

  constructor(private http: HttpClient) { }

  createOffer(offer: TravelOffer): Observable<any> {
    return this.http.post(`${this.apiUrl}/CreateTravel`, offer);
  }

  updateOffer(id: number, offer: TravelOffer): Observable<any> {
    return this.http.put(`${this.apiUrl}/UpdateTravel/${id}`, offer);
  }

  getOfferById(id: number): Observable<TravelOffer> {
    return this.http.get<TravelOffer>(`${this.apiUrl}/GetTravel/${id}`);
  }

  deleteOffer(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/DeleteTravel/${id}`);
  }

  getDestinations(): Observable<Destination[]> {
    return this.http.get<Destination[]>(`${this.apiUrl}/GetDestinations`);
  }

}
