import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { TravelOfferDetailsComponent } from './travel-offer-details/travel-offer-details.component';
import { MyProfileComponent } from './my-profile/my-profile.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from './forgot-password-component/forgot-password-component.component';


export const routes: Routes = [
  { path: 'login', component: LoginComponent }, 
  { path: 'register', component: RegisterComponent },
  { path: 'home', component: HomeComponent },
  { path: 'travelOffers/:id', component: TravelOfferDetailsComponent },
  { path: 'my-profile', component: MyProfileComponent },
  { path: 'reservation/new/:travelOfferId', component: TravelOfferDetailsComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: '',redirectTo: '/home', pathMatch: 'full'}
];

