import axios, { AxiosResponse } from 'axios';
import { getData } from '../utils';

const API_URL = 'https://5b6e-217-73-170-83.ngrok-free.app' + '/api';

const axiosInstance = axios.create({
  baseURL: API_URL,
  withCredentials: true,
  timeout: 50500
});

axiosInstance.interceptors.request.use(async (config) => {
    const AUTH_TOKEN = await getData('AUTH_TOKEN');
    if (AUTH_TOKEN) {
        config.headers.Authorization = `Bearer ${AUTH_TOKEN}`;
    }

    return config;
});

export const get = async <T>(endPoint: string): Promise<T> => {
    try {
        const response: AxiosResponse<T> = await axiosInstance.get(endPoint);
        return response.data;
    }
    catch (error: any) {
        throw new Error(error.message);
    }
};

export const post = async <T>(endPoint: string, data: any): Promise<T> => {
    try {
        console.log("data",data);
        const response: AxiosResponse<T> = await axiosInstance.post(endPoint, data);
        console.log("Response",response.data);
        return response.data;
    }
    catch (error: any) {
        console.log(error.message.toString());
        throw new Error(error.message);
        
    }
};

export const put = async <T>(endPoint: string, data: any): Promise<T> => {
    try {
        const response: AxiosResponse<T> = await axiosInstance.put(endPoint, data);
        return response.data;
    }
    catch (error: any) {
        throw new Error(error.message);
    }
};

export const deleteEntity = async (endPoint: string): Promise<void> => {
    try {
        await axiosInstance.delete(endPoint);
    }
    catch (error: any) {
        throw new Error(error.message);
    }
};
