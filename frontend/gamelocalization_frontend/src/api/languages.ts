import { api } from './axiosClient';
import type {Language, LanguageDto, Result} from '../utils/types';

export const getGlobalLanguages = async (): Promise<Result<Language[]>> => {
    try {
        const response = await api.get('/v1/languages');
        const languages:Language[] = response.data.data.map((language: LanguageDto): Language =>
            ( {languageId: language.id, code: language.code, name: language.name} as Language));

        return { data: languages, error: null };
    } catch (error: any) {
        return { data: null, error: error.response?.data?.message || 'Ошибка при получении языков' };
    }
};

export const createLanguage = async (name: string, code: string): Promise<Result<Language>> => {
    try {
        const response = await api.post('/v1/languages', { name, code });
        return { data: response.data.data, error: null };
    } catch (error: any) {
        return { data: null, error: error.response?.data?.message || 'Ошибка при создании языка' };
    }
};

export const deleteLanguage = async (languageId: string): Promise<Result<null>> => {
    try {
        await api.delete(`/v1/languages/${languageId}`);
        return { data: null, error: null };
    } catch (error: any) {
        return { data: null, error: error.response?.data?.message || 'Ошибка при удалении языка' };
    }
};

export const getProjectLanguages = async (projectId: string): Promise<Result<Language[]>> => {
    try {
        const response = await api.get(`/v1/projects/${projectId}/languages`);
        const languages:Language[] = response.data.data.map((language: LanguageDto): Language =>
            ( {languageId: language.id, code: language.code, name: language.name} as Language));

        return { data: languages, error: null };
    } catch (error: any) {
        return { data: null, error: error.error.response?.data?.message || 'Ошибка при получении языков проекта' };
    }
};

export const addLanguageToProject = async (projectId: string, languageId: string): Promise<Result<null>> => {
    try {
        await api.post(`/v1/projects/${projectId}/languages/${languageId}`);
        return { data: null, error: null };
    } catch (error: any) {
        return { data: null, error: error.error.response?.data?.message || 'Ошибка при добавлении языка в проект' };
    }
};

export const removeLanguageFromProject = async (projectId: string, languageId: string): Promise<Result<null>> => {
    try {
        await api.delete(`/v1/projects/${projectId}/languages/${languageId}`);
        return { data: null, error: null };
    } catch (error: any) {
        return { data: null, error: error.error.response?.data?.message || 'Ошибка при удалении языка из проекта' };
    }
};