export interface Review {
  id: number;
  comment: string;
  rating: number;
  date: string;
  userName?: string;
  travelOfferId: number;
}