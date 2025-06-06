import { Component, inject } from '@angular/core';
import { ReservationService } from '../Services/reservation.service';
import { Reservation } from '../Model/reservation.model';
import { NavbarComponent } from "../navbar/navbar.component";
import { NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-reservation',
  imports: [NavbarComponent, NgIf, NgFor],
  templateUrl: './reservation.component.html',
  styleUrl: './reservation.component.css'
})
export class ReservationComponent {

  reservations: Reservation[] = [];
  isLoading = false;
  errorMessage: string | null = null;

  private reservationService = inject(ReservationService)

  ngOnInit(): void {
    this.loadReservations();
  }

  loadReservations(): void {
    this.isLoading = true;
    this.errorMessage = null;
    this.reservationService.getMyReservations().subscribe({
      next: (data) => {
        this.reservations = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = "Błąd podczas ładowania rezerwacji.";
        this.isLoading = false;
        console.error(err);
      }
    });
  }

  cancelReservation(id: number): void {
    if (confirm("Czy na pewno chcesz anulować tę rezerwację?")) {
      this.reservationService.cancelReservation(id).subscribe({
        next: () => {
          this.loadReservations();
        },
        error: (err) => {
          this.errorMessage = "Błąd podczas anulowania rezerwacji.";
          console.error(err);
        }
      });
    }
  }
}
