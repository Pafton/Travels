import { ReviewService } from './../Services/review.service';
import { Component, inject, Input, OnInit } from '@angular/core';
import { Review } from './../Model/review.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-review',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './review.component.html',
  styleUrl: './review.component.css'
})
export class ReviewComponent implements OnInit {
  @Input() travelOfferId!: number;
  reviews: Review[] = [];
  currentIndex = 0;
  private reviewService = inject(ReviewService);

  ngOnInit(): void {
    this.loadReviews();
    setInterval(() => {
      if (this.reviews.length > 0) {
        this.currentIndex = (this.currentIndex + 1) % this.reviews.length;
      }
    }, 5000);
  }

  loadReviews(): void {
    this.reviewService.getReviews().subscribe((allReviews) => {
      this.reviews = allReviews.filter(r => r.travelOfferId === this.travelOfferId);
            console.log(this.reviews)
    });
  }
}
