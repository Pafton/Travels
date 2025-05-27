import { TravelOffer } from './../Model/travelOffer.model';
import { Component, inject } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { HomeService } from '../Services/home.service';
import { NgFor, NgIf } from '@angular/common';
import { RouterLink } from '@angular/router';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ReviewComponent } from "../review/review.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NavbarComponent, NgFor, NgIf, RouterLink, ReviewComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})

export class HomeComponent {

  travelOffers: TravelOffer[] = [];
  currentImageIndex: number[] = [];
  private homeService = inject(HomeService);


  ngOnInit() {
    this.getTravelAllOffers();
  }

  getTravelAllOffers() {
    this.homeService.getTravelAllOffers().subscribe({
      next: response => {
        this.travelOffers = response;
        this.currentImageIndex = this.travelOffers.map(() => 0);
      },
      error: error => {
        console.error('Error fetching travelOffers: ', error);
      }
    });
  }

  getImageSrc(imageData: any): string {
    if (!imageData) return '';
    if (typeof imageData === 'string') {
      return `data:image/jpeg;base64,${imageData}`;
    }
    if (Array.isArray(imageData)) {
      let binary = '';
      imageData.forEach((b) => (binary += String.fromCharCode(b)));
      const base64String = window.btoa(binary);
      return `data:image/jpeg;base64,${base64String}`;
    }

    return '';
  }

  prevImage(index: number) {
    if (this.currentImageIndex[index] > 0) {
      this.currentImageIndex[index]--;
    }
  }

  nextImage(index: number, maxLength: number) {
    if (this.currentImageIndex[index] < maxLength - 1) {
      this.currentImageIndex[index]++;
    }
  }

}
