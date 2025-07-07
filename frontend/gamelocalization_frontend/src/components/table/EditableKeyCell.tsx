import { useState } from 'react';
import { useUpdateKey } from '../../hooks/key/useUpdateKey';

interface EditableKeyCellProps {
    projectId: string;
    keyId: string;
    initialValue: string;
}

export default function EditableKeyCell({ projectId, keyId, initialValue }: EditableKeyCellProps) {
    const [value, setValue] = useState(initialValue);
    const [isEditing, setIsEditing] = useState(false);
    const updateKey = useUpdateKey();

    const viewTextClass = `pl-2 pr-7 block w-full truncate transition-opacity duration-100 ${isEditing 
        ? 'opacity-0 pointer-events-none' : 'opacity-100'}`;

    const editTextClass = `focus:outline-none focus:ring focus:ring-neutral-300 px-2 absolute inset-0 w-full h-full 
        border rounded-xs text-white opacity-0 text-slate-900 shadow z-5 transition-opacity duration-100 ${isEditing 
        ? 'opacity-100' : 'opacity-0 pointer-events-none'}`;

    const handleBlur = () => {
        setIsEditing(false);
        if (value !== initialValue) {
            updateKey.mutate({
                projectId,
                keyId,
                name: value,
            });
        }
    };

    return (
        <div className="relative w-full py-2 h-full min-h-[1.5em] hover:bg-neutral-600">
            <span className={viewTextClass} onClick={() => setIsEditing(true)}>
                {value || '—'}
            </span>
            <input
                value={value}
                onChange={(e) => setValue(e.target.value)}
                onBlur={handleBlur}
                autoFocus={isEditing}
                className={editTextClass}
                tabIndex={isEditing ? 0 : -1}
            />
            {updateKey.isError && <span className="ml-2 text-xs text-red-500">Error!</span>}
        </div>
    );
} 