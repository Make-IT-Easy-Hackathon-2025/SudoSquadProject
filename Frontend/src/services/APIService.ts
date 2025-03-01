import axios, { AxiosResponse, AxiosRequestConfig, Axios } from 'axios';

const API_URL = 'http://localhost:8083';

const createApiClient = axios.create({
  baseURL: API_URL, 
});

export const get = async <T>(endPoint: string, token: number): Promise<T> => {
    const config: AxiosRequestConfig = {
        headers: {
            'Content-Type': 'application/json',
            Accept: 'application/json',
            Authorization: token ? `Bearer ${token}` : '',
        },
    };

    try {
        const response: AxiosResponse<T> = await createApiClient.get(endPoint, config);
        return response.data;
    }
    catch (error: any) {
        if(axios.isAxiosError(error)) {
            throw new Error(error.response?.data);
        }
        throw new Error("Error while fetching data", error.message);
    }
};

export const post = async <T>(endPoint: string, token?: number, data?: any): Promise<T> => {
    const config: AxiosRequestConfig = {
        headers: {
            'Content-Type': 'application/json',
            Accept: 'application/json',
            Authorization: token ?  `Bearer ${token}` : '',
        },
    };

    try {
        const response: AxiosResponse<T> = await createApiClient.post(endPoint, config, data);
        return response.data;
    }
    catch (error: any) {
        if(axios.isAxiosError(error)) {
            throw new Error(error.response?.data);
        }
        throw new Error("Error while posting data", error.message);
    }
};

export const put = async <T>(endPoint: string, token: number, data?: any): Promise<T> => {
    const config: AxiosRequestConfig = {
        headers: {
            'Content-Type': 'application/json',
            Accept: 'application/json',
            Authorization: token ?  `Bearer ${token}` : '',
        },
    };

    try {
        const response: AxiosResponse<T> = await createApiClient.put(endPoint, config, data);
        return response.data;
    }
    catch (error: any) {
        if(axios.isAxiosError(error)) {
            throw new Error(error.response?.data);
        }
        throw new Error("Error while putting data", error.message);
    }
};

