import { Component, Input, Output, EventEmitter, NgModule } from '@angular/core';
import { Review } from '../Model/review.model';
import { ReviewService } from '../Services/review.service';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-review-form',
  imports: [FormsModule],
  templateUrl: './review-form.component.html',
  styleUrls: ['./review-form.component.css']
})
export class ReviewFormComponent {
  @Input() review!: Review;
  @Input() isEdit = false;
  @Output() closeForm = new EventEmitter<void>();
  @Output() refresh = new EventEmitter<void>();

  constructor(private reviewService: ReviewService, private authService: AuthService) { }

  submit(): void {
    const loggedUserId = this.authService.getUserId();
    console.log('Próba pobrania userId z tokena:', loggedUserId);
    console.log('Recenzja przed przypisaniem userId:', this.review);

    // if (loggedUserId) {
    //   this.review.userId = loggedUserId;
    //   this.review.notLogginUser = null;
    // } else {
    //   console.log('Brak zalogowanego użytkownika - ustawiam userId na null');
    //   this.review.userId = null;
    // }

    console.log('Recenzja po przypisaniu userId:', this.review);

    const request = this.isEdit
      ? this.reviewService.updateReview(this.review)
      : this.reviewService.addReview(this.review);

    request.subscribe({
      next: (response) => {
        console.log('Recenzja została pomyślnie wysłana:', this.review);
        this.refresh.emit();
        this.closeForm.emit();
      },
      error: err => console.error('Błąd podczas zapisu recenzji:', err)
    });
  }



  cancel(): void {
    this.closeForm.emit();
  }
}
