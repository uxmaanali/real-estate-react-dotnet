// src/api/axiosConfig.ts
import axios from 'axios';
import { AuthorizedContext } from "../services/authorized-context.service";

// Create an Axios instance with default config
const axiosInstance = axios.create({
    baseURL: 'https://localhost:7094/api',
    timeout: 10000, // 10 seconds timeout
    headers: {
        'Content-Type': 'application/json',
    },
});

// Request interceptor
axiosInstance.interceptors.request.use(
    (config) => {
        const token = AuthorizedContext.getAuthToken();
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

// Response interceptor
axiosInstance.interceptors.response.use(
    (response) => {
        // You can modify successful responses here
        return response;
    },
    (error) => {
        // Handle errors globally
        if (error.response) {
            switch (error.response.status) {
                case 401:
                    // Handle unauthorized access
                    window.location.href = '/auth/login';
                    break;
                case 403:
                    // Handle forbidden access
                    console.error('Forbidden:', error.response.data.message);
                    break;
                case 404:
                    // Handle not found errors
                    console.error('Not Found:', error.config.url);
                    break;
                case 500:
                    // Handle server errors
                    console.error('Server Error:', error.response.data.message);
                    break;
                default:
                    console.error('Error:', error.response.data.message);
            }
        } else if (error.request) {
            // The request was made but no response was received
            console.error('No response received:', error.request);
        } else {
            // Something happened in setting up the request
            console.error('Request error:', error.message);
        }

        return Promise.reject(error);
    }
);

export default axiosInstance;