export interface IPropertyDto {
	id: number;
	title: string
	price: number;
	address: string;
	listingType: string;
	bedrooms: number;
	bathrooms: number;
	carspots: number;
	description: string;
	images: string[];
	isFavorite: boolean;
}