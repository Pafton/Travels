import { TravelOffer } from './../Model/travelOffer.model';
import { Component, inject } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { HomeService } from '../Services/home.service';
import { NgFor, NgIf } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [NavbarComponent,NgFor,NgIf,RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  //constructor(private s: HomeService){}

  travelOffers: TravelOffer[] = [];

  private homeService = inject(HomeService);

  ngOnInit(){
    this.getTravelAllOffers();
  }

  getTravelOfferById(id: number){
    
  }

  getTravelAllOffers(){
    this.homeService.getTravelAllOffers().subscribe(
      {
        next: response => {
          this.travelOffers = response;
          this.travelOffers.forEach(element => {
            console.log(element.travelOfferImages)
          });
        },
        error: error => {
          console.error('Error featching travelOffers, ' , error)
        }
      }
    )
  }

getImageSrc(imageData: any): string {
  console.log('imageData:', imageData);
  
  if (!imageData) return '';

  // jeśli to string base64
  if (typeof imageData === 'string') {
    // zakładam, że już jest base64
    return `data:image/jpeg;base64,${imageData}`;
  }

  // jeśli to tablica liczb
  if (Array.isArray(imageData)) {
    let binary = '';
    imageData.forEach((b) => (binary += String.fromCharCode(b)));
    const base64String = window.btoa(binary);
    return `data:image/jpeg;base64,${base64String}`;
  }

  return '';
}

}
