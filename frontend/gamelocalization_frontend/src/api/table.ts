import {api} from "./axiosClient";
import type {PaginatedTable, Result} from '../utils/types';

export const getTable = async (projectId: string, page: number, pageSize: number): Promise<Result<PaginatedTable>> => {
    try {
        const response = await api.get(`/v1/projects/${projectId}/table`, { params: { page, pageSize } });
        return { data: response.data.data, error: null };
    } catch (error: any) {
        return { data: null, error: error.response?.data?.message || 'Ошибка при получении таблицы' };
    }
};

export const searchRows = async (projectId: string, query: string, page: number, pageSize: number): Promise<Result<PaginatedTable>> => {
    try {
        const response = await api.get(`/v1/projects/${projectId}/table/rows`, { params: { query, page, pageSize } });
        return { data: response.data.data, error: null };
    } catch (error: any) {
        return { data: null, error: error.response?.data?.message || 'Ошибка при поиске ключей' };
    }
};
