import { api } from './axiosClient';
import type {Key, Result} from '../utils/types';



export const getKeys = async (projectId: string, page: number, pageSize: number): Promise<Result<Key[]>> => {
    try {
        const response = await api.get(`/v1/projects/${projectId}/keys`, { params: { page, pageSize } });
        return { data: response.data.data, error: null };
    } catch (error: any ) {
        return { data: null, error: error.response?.data?.message || 'Ошибка при получении ключей' };
    }
};

export const createKey = async (projectId: string, name: string): Promise<Result<Key>> => {
    try {
        const response = await api.post(`/v1/projects/${projectId}/keys`, { name });
        return { data: response.data.data, error: null };
    } catch (error: any) {
        return { data: null, error: error.response?.data?.message || 'Ошибка при создании ключа' };
    }
};

export const getKey = async (projectId: string, keyId: string): Promise<Result<Key>> => {
    try {
        const response = await api.get(`/v1/projects/${projectId}/keys/${keyId}`);
        return { data: response.data.data, error: null };
    } catch (error: any) {
        return { data: null, error: error.response?.data?.message || 'Ошибка при получении ключа' };
    }
};

export const updateKey = async (projectId: string, keyId: string, name: string): Promise<Result<Key>> => {
    try {
        const response = await api.put(`/v1/projects/${projectId}/keys/${keyId}`, { name });
        return { data: response.data.data, error: null };
    } catch (error: any) {
        return { data: null, error: error.response?.data?.message || 'Ошибка при обновлении ключа' };
    }
};

export const deleteKey = async (projectId: string, keyId: string): Promise<Result<null>> => {
    try {
        await api.delete(`/v1/projects/${projectId}/keys/${keyId}`);
        return { data: null, error: null };
    } catch (error: any) {
        return { data: null, error: error.response?.data?.message || 'Ошибка при удалении ключа' };
    }
};