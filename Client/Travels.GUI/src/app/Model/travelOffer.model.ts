import { TravelOfferImage } from "./TravelOfferImage.model";

export interface TravelOffer {
    id: number; 
    title: string;
    description: string;
    price : number
    begin: Date;
    end: Date;
    availableSpots: number;
    destinationId: number;
    travelOfferImages: TravelOfferImage[];
}
