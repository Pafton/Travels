import { Component, OnInit } from '@angular/core';
import { UserService } from './../Services/user.service';
import { AuthService } from '../auth/auth.service';
import { ReviewService } from '../Services/review.service';
import { User } from '../Model/user.model';
import { Review } from '../Model/review.model';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { NavbarComponent } from '../navbar/navbar.component';
import { ReviewFormComponent } from '../review-form/review-form.component';
import { ReviewComponent } from '../review/review.component';
import { ChangePasswordDto } from '../DTO/ChangePasswordDto';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-my-profile',
  imports: [NgIf, NgFor, CommonModule, NavbarComponent, ReviewFormComponent, ReviewComponent, FormsModule],
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent implements OnInit {

  user?: User;
  showReview = false;
  currentReview?: Review;
  reviewFormVisible = false;
  editingReview = false;
  currentTravelOfferId?: number;
  userFilteredReviews: Review[] = [];
  changePasswordDto = {} as ChangePasswordDto;
  showFilteredReviews = false;
  changePasswordFormVisible = false;


  constructor(
    private userService: UserService,
    private authService: AuthService,
    private reviewService: ReviewService
  ) { }

  ngOnInit(): void {
    this.loadUserData();
  }

  loadUserData(): void {
    const userId = this.authService.getUserId();
    if (userId) {
      this.userService.getUserById(userId).subscribe({
        next: data => this.user = data,
        error: err => console.error('Błąd podczas pobierania danych użytkownika:', err)
      });
    } else {
      console.warn('Brak zalogowanego użytkownika');
    }
  }

  changePassword(): void {
    this.userService.changePassword(this.changePasswordDto).subscribe({
      next: () => {
        console.log('Hasło zmienione pomyślnie.');
        alert('Hasło zostało zmienione.');
      },
      error: (err) => {
        console.error('Błąd podczas zmiany hasła:', err);
        alert('Błąd podczas zmiany hasła: ' + err.error);
      }
    });
  }

  hasReview(travelOfferId: number): boolean {
    return !!this.user?.reviews?.some(r => r.travelOfferId === travelOfferId);
  }

  toggleChangePasswordForm(): void {
    this.changePasswordFormVisible = !this.changePasswordFormVisible;
  }

  openReviewForm(travelOfferId: number, edit: boolean = false): void {
    this.currentTravelOfferId = travelOfferId;
    this.editingReview = edit;

    if (edit) {
      const existingReview = this.user?.reviews?.find(r => r.travelOfferId === travelOfferId);
      if (existingReview) {
        this.currentReview = { ...existingReview };
      } else {
        this.currentReview = { comment: '', rating: 1, travelOfferId };
      }
    } else {
      this.currentReview = { comment: '', rating: 1, travelOfferId };
    }

    this.reviewFormVisible = true;
  }

  closeReviewForm(): void {
    this.reviewFormVisible = false;
    this.currentTravelOfferId = undefined;
    this.editingReview = false;
  }

  refreshReviews(): void {
    this.loadUserData();
  }

  deleteReview(travelOfferId: number): void {
    const review = this.user?.reviews?.find(r => r.travelOfferId === travelOfferId);
    if (!review || review.id === undefined) {
      console.warn('Recenzja nie znaleziona lub brak id');
      return;
    }

    this.reviewService.deleteReview(review.id).subscribe({
      next: () => {
        if (this.user && this.user.reviews) {
          this.user.reviews = this.user.reviews.filter(r => r.id !== review.id);
        }
        console.log('Recenzja usunięta');
      },
      error: (err) => console.error('Błąd podczas usuwania recenzji:', err)
    });
  }

  viewReviews(travelOfferId: number): void {
    this.currentTravelOfferId = travelOfferId;
    console.log(this.currentTravelOfferId)
    this.showReview = true;
  }

  /////POMOCNICZA
  showUserReviewsForOwnOffers(): void {
    if (!this.user?.reviews || !this.user?.reservations) {
      this.userFilteredReviews = [];
      this.showFilteredReviews = true;
      return;
    }

    const userOfferIds = this.user.reservations.map(r => r.travelOfferId);
    this.userFilteredReviews = this.user.reviews.filter(r =>
      userOfferIds.includes(r.travelOfferId)
    );
    this.showFilteredReviews = true;
  }


  getCurrentReview(): Review {
    if (!this.user || this.currentTravelOfferId === undefined) {
      return { comment: '', rating: 1, travelOfferId: 0 };
    }

    if (this.editingReview) {
      const existingReview = this.user.reviews?.find(r => r.travelOfferId === this.currentTravelOfferId);
      if (existingReview) {
        return { ...existingReview };
      }
    }

    return { comment: '', rating: 1, travelOfferId: this.currentTravelOfferId };
  }
}
