import { useQuery } from '@tanstack/react-query';
import { getProjectLanguages } from '../../api/languages';

export function useProjectLanguages(projectId: string) {
    return useQuery({
        queryKey: ['languages', 'project', projectId],
        queryFn: () => getProjectLanguages(projectId),
        enabled: !!projectId,
    });
} 