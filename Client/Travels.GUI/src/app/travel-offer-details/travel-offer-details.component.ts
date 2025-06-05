import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router'; // dodaj Router
import { TravelOffer } from '../Model/travelOffer.model';
import { HomeService } from '../Services/home.service';
import { CommonModule, NgIf } from '@angular/common';
import { ReviewComponent } from '../review/review.component';
import { AuthService } from '../auth/auth.service';
import { ReservationService } from '../Services/reservation.service';
import { Reservation } from '../Model/reservation.model';

@Component({
  selector: 'app-travel-offer-details',
  standalone: true,
  imports: [NgIf, ReviewComponent, CommonModule],
  templateUrl: './travel-offer-details.component.html',
  styleUrl: './travel-offer-details.component.css'
})
export class TravelOfferDetailsComponent {
  offer?: TravelOffer;

  private route = inject(ActivatedRoute);   
  private router = inject(Router);          
  private homeService = inject(HomeService);
  private authService = inject(AuthService);
  private reservationService = inject(ReservationService);

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.homeService.getTravelOfferById(id).subscribe((res) => {
      this.offer = res;
    });
  }

  makeReservation(): void {
    if (!this.offer) return;

    if (!this.authService.isLoggedIn()) {
      alert('Musisz się zalogować, aby dokonać rezerwacji.');
      this.router.navigate(['/login']);
      return;
    }

    const userId = this.authService.getUserId();
    if (!userId) {
      alert('Nie udało się pobrać danych użytkownika.');
      return;
    }

    const reservation: Reservation = {
      id: 0,
      userId: userId,
      travelOfferId: this.offer.id,
      reservationDate: new Date().toISOString(),
      status: true
    };

    this.reservationService.startReservation(reservation).subscribe({
      next: () => {
        alert('Rezerwacja zakończona sukcesem!');
        this.router.navigate(['/home']);
      },
      error: (err) => {
        console.error('Błąd podczas rezerwacji:', err);
        alert('Wystąpił błąd podczas rezerwacji.');
      }
    });
  }
}
