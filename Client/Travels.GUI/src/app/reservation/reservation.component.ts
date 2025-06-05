import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ReservationService } from '../Services/reservation.service';
import { AuthService } from '../auth/auth.service';
import { TravelOffer } from '../Model/travelOffer.model';
import { User } from '../Model/user.model';
import { Reservation } from '../Model/reservation.model';
import { HomeService } from '../Services/home.service';
import { NavbarComponent } from '../navbar/navbar.component';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-reservation',
  standalone: true,
  imports: [NavbarComponent, CommonModule, FormsModule],
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.css'],
})
export class ReservationComponent implements OnInit {
  travelOffer?: TravelOffer;
  userId?: number;
  user?: User;
  reservationDate: string = '';
  availableDates: string[] = [];


  constructor(
    private route: ActivatedRoute,
    private reservationService: ReservationService,
    private authService: AuthService,
    private homeService: HomeService,
    private router: Router
  ) { }

  ngOnInit(): void {

  }

}