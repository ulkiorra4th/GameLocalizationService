import { Input } from '../shared/Input';
import { Button } from '../shared/Button';

interface ProjectHeaderProps {
    isEditing: boolean;
    editName: string;
    setEditName: (name: string) => void;
    onEdit: () => void;
    onSave: () => void;
    isPending: boolean;
    title: string;
    createdAt?: string;
}

export default function ProjectHeader({
    isEditing,
    editName,
    setEditName,
    onEdit,
    onSave,
    isPending,
    title,
    createdAt,
}: ProjectHeaderProps) {
    return (
        <div className="flex gap-6 my-4 items-center">
            {isEditing ? (
                <Input
                    className="text-2xl font-bold bg-neutral-900 text-white rounded px-2 py-1 border border-neutral-700
                    focus:outline-none focus:ring focus:ring-neutral-400 hover:border-neutral-300"
                    value={editName}
                    onChange={e => setEditName(e.target.value)}
                    autoFocus
                />
            ) : (
                <h1 className="text-2xl font-bold ">{title}</h1>
            )}
            {createdAt && (
                <h2 className="text-md text-neutral-400 mt-8">
                    created at: {createdAt.slice(0,10)} {createdAt.slice(11,16)}
                </h2>
            )}
            {isEditing ? (
                <Button className="my-auto mt-5 border-1 border-neutral-500 text-neutral-200 text-2xs"
                        onClick={onSave} disabled={isPending} variant="secondary">
                    {isPending ? 'Saving...' : 'Save'}
                </Button>
            ) : (
                <Button className="my-auto mt-5 border-1 border-neutral-500 text-neutral-200 text-2xs"
                        variant="secondary" onClick={onEdit}>
                    Edit
                </Button>
            )}
        </div>
    );
} 