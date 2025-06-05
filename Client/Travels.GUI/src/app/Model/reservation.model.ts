import { TravelOffer } from "./travelOffer.model";
import { User } from "./user.model";

export interface Reservation {
  id: number;
  userId: number;
  travelOfferId: number;
  reservationDate: string;
  status: boolean;
  user?: User;
  travelOffer?: TravelOffer;
}