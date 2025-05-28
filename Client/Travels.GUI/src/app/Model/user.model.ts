import { Review } from "./review.model"
import { Reservation } from "./reservation.model";

export interface User {
  id: number;
  name: string;
  surname: string;
  email: string;
  password: string;
  reviews?: Review[]; 
  reservations?: Reservation[];
}
