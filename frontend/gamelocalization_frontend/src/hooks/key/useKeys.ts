import { useQuery } from '@tanstack/react-query';
import { getKeys } from '../../api/keys';

export function useKeys(projectId: string, page: number, pageSize: number) {
    return useQuery({
        queryKey: ['keys', projectId, page, pageSize],
        queryFn: () => getKeys(projectId, page, pageSize),
        enabled: !!projectId,
    });
} 