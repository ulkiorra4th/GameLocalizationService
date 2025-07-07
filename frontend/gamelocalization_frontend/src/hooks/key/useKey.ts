import { useQuery } from '@tanstack/react-query';
import { getKey } from '../../api/keys';

export function useKey(projectId: string, keyId: string) {
    return useQuery({
        queryKey: ['key', projectId, keyId],
        queryFn: () => getKey(projectId, keyId),
        enabled: !!projectId && !!keyId,
    });
} 