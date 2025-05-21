import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TravelOffer } from '../Model/travelOffer.model';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  private readonly apiUrl = 'http://localhost:5190/api/TravelOffer'

  constructor(private readonly http:HttpClient) { }


    getTravelOfferById(id: number): Observable<TravelOffer> {
      return this.http.get<TravelOffer>(`${this.apiUrl}/${id}`);
  }

  getTravelAllOffers(): Observable<TravelOffer[]>{
     return this.http.get<TravelOffer[]>(`${this.apiUrl}/GetAllTravels`);
  }
}
