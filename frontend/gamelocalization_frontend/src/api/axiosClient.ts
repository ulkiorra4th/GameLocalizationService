import axios from 'axios';

export const api = axios.create({
    baseURL: 'http://localhost:8081/api',
    withCredentials: false,
    timeout: 10000,
});

api.interceptors.response.use(
    response => response,
    error => {
        if (error?.message) {
            window.alert(error.response?.data?.message);
        }
        return Promise.reject(error);
    }
);