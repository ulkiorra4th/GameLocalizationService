import { useMutation, useQueryClient } from '@tanstack/react-query';
import { removeLanguageFromProject } from '../../api/languages';

interface DeleteLanguageParams {
    projectId: string;
    languageId: string;
}

export function useDeleteLanguage() {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ projectId, languageId }: DeleteLanguageParams) => removeLanguageFromProject(projectId, languageId),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['languages'] });
            queryClient.invalidateQueries({ queryKey: ['table'] });
        },
    });
} 