import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Review } from '../Model/review.model';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  private readonly apiUrl = 'http://localhost:5190/api/Review' 

  constructor(private readonly http:HttpClient) { }

  getReviews(): Observable<Review[]>{
    return this.http.get<Review[]>(`${this.apiUrl}`)
  }
}
