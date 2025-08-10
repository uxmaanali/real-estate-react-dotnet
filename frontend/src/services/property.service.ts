import axiosInstance from '../configs/axios-config';
import { IApiResponse } from "../dtos/api-response-dto";
import { IPropertyFiltersDto } from "../dtos/property/propert-filters-dto";
import { IPropertyDto } from "../dtos/property/property-dto";

export class PropertyServiceImplementation {
	public async GetProperties(
		filters: IPropertyFiltersDto
	): Promise<IApiResponse<IPropertyDto[]>> {
		try {
			const params = new URLSearchParams();
			Object.entries(filters).forEach(([key, value]) => {
				if (value !== null && value !== undefined) {
					params.append(key, value.toString());
				}
			});

			const response = await axiosInstance.get<IApiResponse<IPropertyDto[]>>(
				'/Properties',
				{
					params: params
				});
			return response.data;
		} catch (error) {
			throw this.handleError(error);
		}
	}

	public async GetProperty(
		propertyId: number
	): Promise<IApiResponse<IPropertyDto>> {
		try {
			const response = await axiosInstance.get<IApiResponse<IPropertyDto>>(
				`/Properties/${propertyId}`);
			return response.data;
		} catch (error) {
			throw this.handleError(error);
		}
	}

	public async GetFavoriteProperties(
	): Promise<IApiResponse<IPropertyDto[]>> {
		try {
			const response = await axiosInstance.get<IApiResponse<IPropertyDto[]>>(
				'/Favorites');
			return response.data;
		} catch (error) {
			throw this.handleError(error);
		}
	}

	public async AddRemoveFavoriteProperty(
		propertyId: number
	): Promise<IApiResponse<boolean>> {
		try {
			const response = await axiosInstance.post<IApiResponse<boolean>>(
				`/Favorites/${propertyId}`);
			return response.data;
		} catch (error) {
			throw this.handleError(error);
		}
	}

	private handleError(error: unknown): Error {
		return new Error('An unexpected error occurred');
	}
}

export const PropertyService = new PropertyServiceImplementation();