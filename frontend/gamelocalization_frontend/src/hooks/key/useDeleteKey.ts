import { useMutation, useQueryClient } from '@tanstack/react-query';
import { deleteKey } from '../../api/keys';

interface DeleteKeyParams {
    projectId: string;
    keyId: string;
}

export function useDeleteKey() {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ projectId, keyId}: DeleteKeyParams & { page?: number; pageSize?: number }) => deleteKey(projectId, keyId),
        onSuccess: (_data, variables) => {
            queryClient.invalidateQueries({ queryKey: ['keys'] });
            queryClient.invalidateQueries({ queryKey: ['key'] });
            if (variables?.projectId && variables?.page && variables?.pageSize) {
                queryClient.invalidateQueries({ queryKey: ['table', variables.projectId, variables.page, variables.pageSize] });
            }
        },
    });
} 