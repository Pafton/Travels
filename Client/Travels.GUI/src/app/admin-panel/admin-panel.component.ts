import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { UserService } from '../Services/user.service';
import { NavbarComponent } from '../navbar/navbar.component';
import { CommonModule, NgIf } from '@angular/common';
import { FormsModule, NgModel } from '@angular/forms';
import { TravelOffer } from '../Model/travelOffer.model';
import { TravelOfferService } from '../Services/travel-offer.service';
import { Destination } from '../Model/destination.model';


@Component({
  selector: 'app-admin-panel',
  imports: [NavbarComponent, NgIf, CommonModule, FormsModule],
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

  private authService = inject(AuthService);
  private router = inject(Router);
  private userService = inject(UserService);
  private travelOfferService = inject(TravelOfferService)

  userId!: number;
  result: any;
  offer: TravelOffer = {} as TravelOffer;
  destinations: Destination[] = [];
  editOfferId!: number;
  resultOffer: any;


  ngOnInit(): void {
    if (!this.authService.isAdmin()) {
      alert('Brak dostępu do panelu administratora.');
      this.router.navigate(['/home']);
    }
    this.loadDestinations();
  }

  loadDestinations() {
    this.travelOfferService.getDestinations().subscribe({
      next: (data) => this.destinations = data,
      error: (err) => console.error('Błąd ładowania destynacji:', err)
    });
  }


  loadOfferById() {
    if (!this.editOfferId) {
      alert('Podaj ID oferty!');
      return;
    }

    this.travelOfferService.getOfferById(this.editOfferId).subscribe({
      next: offer => {
        this.offer = offer;
        this.resultOffer = offer;
      },
      error: err => {
        this.resultOffer = `Błąd ładowania oferty: ${err.message}`;
      }
    });
  }

  getAllUsers() {
    this.userService.getAllUsers().subscribe({
      next: users => this.result = users,
      error: err => this.result = `Błąd pobierania użytkowników: ${err.message}`
    });
  }

  getUserById() {
    if (!this.userId) {
      alert('Podaj ID użytkownika!');
      return;
    }
    this.userService.getUserById(this.userId).subscribe({
      next: user => this.result = user,
      error: err => this.result = `Błąd pobierania użytkownika: ${err.message}`
    });
  }

  deleteUser() {
    if (!this.userId) {
      alert('Podaj ID użytkownika!');
      return;
    }
    this.userService.deleteUser(this.userId).subscribe({
      next: res => {
        this.result = `Użytkownik o ID ${this.userId} został usunięty.`;
        this.getAllUsers();
      },
      error: err => this.result = `Błąd usuwania użytkownika: ${err.message}`
    });
  }

  activateUser() {
    if (!this.userId) {
      alert('Podaj ID użytkownika!');
      return;
    }
    this.userService.activateUser(this.userId).subscribe({
      next: res => {
        this.result = `Użytkownik o ID ${this.userId} został aktywowany.`;
        this.getAllUsers();
      },
      error: err => this.result = `Błąd aktywacji użytkownika: ${err.message}`
    });
  }

  deactivateUser() {
    if (!this.userId) {
      alert('Podaj ID użytkownika!');
      return;
    }
    this.userService.deactivateUser(this.userId).subscribe({
      next: res => {
        this.result = `Użytkownik o ID ${this.userId} został dezaktywowany.`;
        this.getAllUsers();
      },
      error: err => this.result = `Błąd dezaktywacji użytkownika: ${err.message}`
    });
  }

  submitOffer() {
    if (this.offer.id) {
      this.travelOfferService.updateOffer(this.offer.id, this.offer).subscribe({
        next: updated => {
          this.result = `Zaktualizowano ofertę o ID ${this.offer.id}.`;
          this.offer = {} as TravelOffer;
        },
        error: err => {
          this.result = `Błąd aktualizacji oferty: ${err.message}`;
        }
      });
    } else {
      this.travelOfferService.createOffer(this.offer).subscribe({
        next: created => {
          this.result = `Utworzono nową ofertę o ID ${created.id}.`;
          this.offer = {} as TravelOffer;
        },
        error: err => {
          this.result = `Błąd tworzenia oferty: ${err.message}`;
        }
      });
    }
  }

}
