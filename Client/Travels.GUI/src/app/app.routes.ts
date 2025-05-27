import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { TravelOfferDetailsComponent } from './travel-offer-details/travel-offer-details.component';


export const routes: Routes = [
  { path: 'login', component: LoginComponent }, 
  { path: 'home', component: HomeComponent },
  { path: 'travelOffers/:id', component: TravelOfferDetailsComponent },
  { path: '',redirectTo: '/login', pathMatch: 'full'}
];

