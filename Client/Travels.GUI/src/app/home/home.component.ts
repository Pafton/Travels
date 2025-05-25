import { TravelOffer } from './../Model/travelOffer.model';
import { Component, inject } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { HomeService } from '../Services/home.service';
import { NgFor, NgIf } from '@angular/common';
import { RouterLink } from '@angular/router';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NavbarComponent,NgFor,NgIf,RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css', 
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
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

}
