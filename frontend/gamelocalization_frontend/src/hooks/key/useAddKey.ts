import {useMutation, useQueryClient} from '@tanstack/react-query';
import { createKey } from '../../api/keys';

interface AddKeyParams {
    projectId: string;
    name: string;
}

export function useAddKey() {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ projectId, name}: AddKeyParams & { page?: number; pageSize?: number }) => createKey(projectId, name),
        onSuccess: (_data, variables) => {
            queryClient.invalidateQueries({ queryKey: ['translations'] });
            queryClient.invalidateQueries({ queryKey: ['keys'] });
            if (variables?.projectId && variables?.page && variables?.pageSize) {
                queryClient.invalidateQueries({ queryKey: ['table', variables.projectId, variables.page, variables.pageSize] });
            }
        },
    });
}