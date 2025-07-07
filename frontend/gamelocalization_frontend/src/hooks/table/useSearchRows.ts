import { useQuery } from '@tanstack/react-query';
import { searchRows } from '../../api/table.ts';

interface UseSearchRowsParams {
    projectId: string;
    query: string;
    page: number;
    pageSize: number;
}

export function useSearchRows({ projectId, query, page, pageSize }: UseSearchRowsParams) {
    return useQuery({
        queryKey: ['keys', 'search', projectId, query, page, pageSize],
        queryFn: () => searchRows(projectId, query, page, pageSize),
        enabled: !!projectId && !!query,
    });
}