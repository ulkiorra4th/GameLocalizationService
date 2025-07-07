import { useMutation, useQueryClient } from '@tanstack/react-query';
import { addLanguageToProject } from '../../api/languages';

interface AddLanguageParams {
    projectId: string;
    languageId: string;
}

export function useAddLanguage() {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ projectId, languageId }: AddLanguageParams) => addLanguageToProject(projectId, languageId),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['languages'] });
            queryClient.invalidateQueries({ queryKey: ['table'] });
        },
    });
}