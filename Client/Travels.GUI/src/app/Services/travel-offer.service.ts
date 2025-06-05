import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TravelOffer } from '../Model/travelOffer.model';
import { Observable } from 'rxjs';

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

  deleteOffer(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/DeleteTravel/${id}`);
  }

}
