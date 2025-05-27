import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TravelOffer } from '../Model/travelOffer.model';
import { HomeService } from '../Services/home.service';
import { CommonModule, NgIf } from '@angular/common';
import { ReviewComponent } from '../review/review.component';

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
  private homeService = inject(HomeService);

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.homeService.getTravelOfferById(id).subscribe((res) => {
      this.offer = res;
    });
  }
}
