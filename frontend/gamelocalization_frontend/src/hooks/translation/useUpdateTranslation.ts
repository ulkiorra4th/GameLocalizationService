import { useMutation, useQueryClient } from '@tanstack/react-query';
import { updateTranslation } from '../../api/translations';
import type { Translation } from '../../utils/types';

interface UpdateTranslationParams {
    projectId: string;
    data: Translation;
}

export function useUpdateTranslation() {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ projectId, data }: UpdateTranslationParams) => updateTranslation(projectId, data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['table'] });
            queryClient.invalidateQueries({ queryKey: ['translations'] });
        },
    });
}
