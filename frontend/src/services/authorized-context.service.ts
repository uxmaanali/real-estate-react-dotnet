export class AuthorizedContext {

    public static isAuthenticated(): boolean {
        return !!localStorage.getItem('authToken');
    }

    public static getAuthToken(): string | null {
        return localStorage.getItem('authToken');
    }

    public static setAuthToken(token: string): void {
        localStorage.setItem('authToken', token);
    }

    public static clearAuthToken(): void {
        localStorage.removeItem('authToken');
    }
}