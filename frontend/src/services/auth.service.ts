import axiosInstance from '../configs/axios-config';
import { IRegisterRequestDto } from "../dtos/register/request-dto";
import { IApiResponse } from "../dtos/api-response-dto";
import { IRegisterResponseDto } from "../dtos/register/response-dto";
import { ILoginRequestDto } from "../dtos/login/request-dto";
import { ILoginResponseDto } from "../dtos/login/response-dto";
import { AuthorizedContext } from "./authorized-context.service";

export class AuthenticationService {
	public async register(
		data: IRegisterRequestDto
	): Promise<IApiResponse<IRegisterResponseDto>> {
		try {
			const response = await axiosInstance.post<IApiResponse<IRegisterResponseDto>>(
				'/auth/register',
				data
			);
			return response.data;
		} catch (error) {
			throw this.handleError(error);
		}
	}

	public async login(
		data: ILoginRequestDto
	): Promise<IApiResponse<ILoginResponseDto>> {
		try {
			const response = await axiosInstance.post<IApiResponse<ILoginResponseDto>>(
				'/auth/login',
				data
			);
			return response.data;
		} catch (error) {
			throw this.handleError(error);
		}
	}

	public logout(): void {
		try {
			AuthorizedContext.clearAuthToken();
		} catch (error) {
			console.error('Logout failed:', error);
			// Still clear token even if logout API fails
			AuthorizedContext.clearAuthToken();
		}
	}

	private handleError(error: unknown): Error {
		return new Error('An unexpected error occurred');
	}
}

// Export a singleton instance
export const AuthService = new AuthenticationService();