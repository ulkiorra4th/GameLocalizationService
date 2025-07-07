import {Button} from "../shared/Button.tsx";

interface ProjectCardProps {
    projectId: string;
    name: string;
    lastUpdated: string;
    isCurrent?: boolean;
    onOpen?: (projectId: string) => void;
}

export default function ProjectCard({ projectId, name, lastUpdated, isCurrent, onOpen }: ProjectCardProps) {
    return (
        <div className={`relative h-30 mb-2 p-7 rounded-2xl hover:bg-neutral-800 ${isCurrent ? 'bg-neutral-800' : 'bg-neutral-900'}`}>
            <div className="mt-2 my-auto" key={projectId}>
                <span className="flex relative ">
                    <div>
                        <h3 className="my-auto h-7 w-full text-lg text-neutral-100 font-bold">{name}</h3>
                        <h4 className="text-xs text-neutral-400">{lastUpdated}</h4>
                    </div>

                    {!isCurrent ? (
                        <Button className="absolute right-4 top-0 bottom-0 my-auto" onClick={() => onOpen?.(projectId)}>
                            Select
                        </Button>
                    ) : (
                        <h4 className="text-md text-neutral-400 absolute right-5 top-0 bottom-0 my-2">Current</h4>
                    )}
                </span>
            </div>
        </div>
    );
}