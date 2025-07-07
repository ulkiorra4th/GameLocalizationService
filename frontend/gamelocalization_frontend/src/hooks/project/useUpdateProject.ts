import { useMutation, useQueryClient } from '@tanstack/react-query';
import { updateProject } from '../../api/projects';
import type {UpdateProjectDto} from "../../utils/types.ts";

interface UpdateProjectParams {
    projectId: string;
    data: UpdateProjectDto;
}

export function useUpdateProject() {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({ projectId, data }: UpdateProjectParams) => updateProject(projectId, data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['projects'] });
            queryClient.invalidateQueries({ queryKey: ['project'] });
        },
    });
} 