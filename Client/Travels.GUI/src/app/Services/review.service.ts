import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Review } from '../Model/review.model';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  private readonly apiUrl = 'http://localhost:5190/api/Review'

  constructor(private readonly http: HttpClient) { }

  getReviews(): Observable<Review[]> {
    return this.http.get<Review[]>(`${this.apiUrl}`)
  }

  addReview(review: Review): Observable<Review> {
    return this.http.post<Review>(this.apiUrl, review);
  }

  updateReview(review: Review): Observable<any> {
    return this.http.put(this.apiUrl, review);
  }

  deleteReview(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
