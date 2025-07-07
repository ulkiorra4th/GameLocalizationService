import { useQuery } from '@tanstack/react-query';
import { getTable } from '../../api/table';

export function useTable(projectId: string, page: number, pageSize: number) {
    return useQuery({
        queryKey: ['table', projectId, page, pageSize],
        queryFn: () => getTable(projectId, page, pageSize),
        enabled: !!projectId && page > 0 && pageSize > 0,
    });
} 