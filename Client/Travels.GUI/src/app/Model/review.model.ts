export interface Review {
  id?: number;
  comment: string;
  rating: number;
  date?: string;
  userId?: number | null;
  notLogginUser?: string | null;
  userName?: string;
  travelOfferId: number;
}