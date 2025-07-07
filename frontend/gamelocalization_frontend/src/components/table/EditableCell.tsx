import { useState } from 'react';
import { useUpdateTranslation } from '../../hooks/translation/useUpdateTranslation.ts';

interface EditableCellProps {
    projectId: string;
    keyId: string;
    languageId: string;
    initialValue: string;
}

export default function EditableCell({ projectId, keyId, languageId, initialValue }: EditableCellProps) {
    const [value, setValue] = useState(initialValue);
    const [isEditing, setIsEditing] = useState(false);
    const updateTranslation = useUpdateTranslation();

    const viewTextClass = `px-2 block w-full truncate transition-opacity duration-100 ${isEditing 
        ? 'opacity-0 pointer-events-none' : 'opacity-100'}`;

    const editTextClass = `focus:outline-none focus:ring focus:ring-neutral-300 px-2 absolute inset-0 w-full h-full 
        border rounded-xs text-white opacity-0 text-slate-900 shadow z-5 transition-opacity duration-100 ${isEditing 
        ? 'opacity-100' : 'opacity-0 pointer-events-none'}`;

    const handleBlur = () => {
        setIsEditing(false);
        if (value !== initialValue) {
            updateTranslation.mutate({
                projectId,
                data: {
                    keyId,
                    languageId,
                    value,
                },
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
            {updateTranslation.isError && <span className="ml-2 text-xs text-red-500">Error!</span>}
        </div>
    );
}