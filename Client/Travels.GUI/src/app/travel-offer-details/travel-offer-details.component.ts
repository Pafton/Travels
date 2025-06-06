import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router'; // dodaj Router
import { TravelOffer } from '../Model/travelOffer.model';
import { HomeService } from '../Services/home.service';
import { CommonModule, NgIf } from '@angular/common';
import { ReviewComponent } from '../review/review.component';
import { AuthService } from '../auth/auth.service';
import { ReservationService } from '../Services/reservation.service';
import { Reservation } from '../Model/reservation.model';
import { NavbarComponent } from "../navbar/navbar.component";

@Component({
  selector: 'app-travel-offer-details',
  standalone: true,
  imports: [NgIf, ReviewComponent, CommonModule, NavbarComponent],
  templateUrl: './travel-offer-details.component.html',
  styleUrl: './travel-offer-details.component.css'
})
export class TravelOfferDetailsComponent {
  offer?: TravelOffer;
  isAdmin: boolean = false;
  selectedFile: File | null = null;

  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private homeService = inject(HomeService);
  private authService = inject(AuthService);
  private reservationService = inject(ReservationService);

  ngOnInit() {
    this.isAdmin = this.authService.isAdmin();

    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.homeService.getTravelOfferById(id).subscribe((res) => {
      this.offer = res;
    });
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    } else {
      this.selectedFile = null;
    }
  }

  uploadImage(): void {
    if (!this.selectedFile || !this.offer) return;

    const formData = new FormData();
    formData.append('TravelOfferId', this.offer.id.toString());
    formData.append('ImageFile', this.selectedFile);

    this.homeService.addImageToTravelOffer(formData).subscribe({
      next: () => {
        alert('Zdjęcie zostało dodane.');
        this.selectedFile = null;
        // Odśwież ofertę, aby załadować nowe zdjęcia (opcjonalne)
        this.homeService.getTravelOfferById(this.offer!.id).subscribe((offer) => (this.offer = offer));
      },
      error: (err) => {
        console.error('Błąd podczas dodawania zdjęcia:', err);
        alert('Nie udało się dodać zdjęcia.');
      },
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
