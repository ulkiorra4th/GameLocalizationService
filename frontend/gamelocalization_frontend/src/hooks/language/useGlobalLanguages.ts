import { useQuery } from '@tanstack/react-query';
import { getGlobalLanguages } from '../../api/languages';

export function useGlobalLanguages() {
    return useQuery({
        queryKey: ['languages', 'global'],
        queryFn: getGlobalLanguages,
    });
} 