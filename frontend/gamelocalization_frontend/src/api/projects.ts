import { api } from './axiosClient';
import type {Project, Result, UpdateProjectDto} from '../utils/types';

export const getProjects = async (): Promise<Result<Project[]>> => {
    try {
        const response = await api.get('/v1/projects');
        return { data: response.data.data, error: null };
    } catch (error: any) {
        return { data: null, error: error.response?.data?.message || 'Ошибка при получении проектов' };
    }
};

export const getProjectById = async (projectId: string): Promise<Result<Project>> => {
    try {
        const response = await api.get(`/v1/projects/${projectId}`);
        return { data: response.data.data, error: null };
    } catch (error: any) {
        return { data: null, error: error.response?.data?.message || 'Ошибка при получении проекта' };
    }
};

export const updateProject = async (projectId: string, data: Partial<UpdateProjectDto>):
    Promise<Result<unknown>> => {
    try {
        const response = await api.put(`/v1/projects/${projectId}`, data);
        return { data: response.data.data, error: null };
    } catch (error: any) {
        return { data: null, error: error.response?.data?.message || 'Ошибка при обновлении проекта' };
    }
};