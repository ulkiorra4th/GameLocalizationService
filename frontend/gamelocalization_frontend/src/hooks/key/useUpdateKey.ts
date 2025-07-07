import { useMutation, useQueryClient } from '@tanstack/react-query';
import { updateKey } from '../../api/keys';

interface UpdateKeyParams {
    projectId: string;
    keyId: string;
    name: string;
}

export function useUpdateKey() {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ projectId, keyId, name }: UpdateKeyParams) => updateKey(projectId, keyId, name),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['keys'] });
            queryClient.invalidateQueries({ queryKey: ['key'] });
        },
    });
} 