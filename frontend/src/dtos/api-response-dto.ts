export interface IApiResponse<T> {
    success: boolean;
    response: T;
    message: string;
}