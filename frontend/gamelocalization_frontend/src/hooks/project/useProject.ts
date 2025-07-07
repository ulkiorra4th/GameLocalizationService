import { useQuery } from '@tanstack/react-query';
import { getProjectById } from '../../api/projects';

export function useProject(projectId: string) {
    return useQuery({
        queryKey: ['project', projectId],
        queryFn: () => getProjectById(projectId),
        enabled: !!projectId,
    });
} 