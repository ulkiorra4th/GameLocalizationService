import { api } from './axiosClient';
import type { Translation, Result } from '../utils/types';


export const updateTranslation = async (projectId: string, data: Translation): Promise<Result<Translation>> => {
    try {
        const response = await api.put(`/v1/projects/${projectId}/translations`, data);
        return { data: response.data.data, error: null };
    } catch (error: any) {
        return { data: null, error: error.response?.data?.message || 'Ошибка при обновлении перевода' };
    }
};