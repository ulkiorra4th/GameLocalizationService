import ProjectCard from "./ProjectCard.tsx";
import { useProjects } from "../../hooks/project/useProjects";

interface ProjectsListProps {
    className?: string;
    currentProjectId?: string;
    onProjectOpen?: (projectId: string) => void;
}

export default function ProjectsList({ className, currentProjectId, onProjectOpen }: ProjectsListProps) {
    const { data, isLoading, error } = useProjects();

    return (
        <aside className={`min-w-3xs max-w-md w-md bg-neutral-950 h-screen flex flex-col ${className}`}>
            <div className="px-2 flex flex-col h-full">
                <div className="relative my-3 h-15 bg-neutral-900 rounded-xl flex-shrink-0">
                    <h2 className="text-center mx-auto my-auto h-8 w-20 text-xl font-bold absolute top-0 bottom-0 left-0 right-0">
                        Projects
                    </h2>
                </div>
                <div title="Create project (soon)" className="relative mb-3 h-10 bg-neutral-900 rounded-xl flex-shrink-0 hover:bg-neutral-800">
                    <h2 className="text-center mx-auto my-auto h-8 w-20 text-xl font-bold absolute top-0 bottom-0 left-0 right-0">
                        +
                    </h2>
                </div>

                {isLoading && <h3 className="mt-5 text-neutral-400 text-md text-center flex-shrink-0">Loading projects...</h3>}
                {error && <div className="text-red-500 text-center flex-shrink-0">Error</div>}
                {data?.data && data.data.length === 0 && (
                    <h3 className="text-neutral-100 text-center flex-shrink-0">No projects</h3>
                )}

                <div className="flex-1 overflow-y-auto">
                    {data?.data && data.data.slice().sort((a, b) =>
                        new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()).reverse().map((project) => {
                        const createdAt = new Date(project.createdAt);
                        const formattedDate = createdAt.toDateString();
                        const isCurrent = currentProjectId === project.id;
                        return (
                            <ProjectCard
                                key={project.id}
                                projectId={project.id}
                                name={project.name.value}
                                lastUpdated={formattedDate}
                                isCurrent={isCurrent}
                                onOpen={onProjectOpen}
                            />
                        );
                    })}
                </div>
            </div>
        </aside>
    );
}