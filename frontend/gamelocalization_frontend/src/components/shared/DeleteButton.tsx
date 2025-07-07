import React from 'react';

interface DeleteButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
    className?: string;
}

export const DeleteButton: React.FC<DeleteButtonProps> = ({className, ...props}) => {
    const fullClassName = `hover:bg-red-500 text-neutral-200 rounded-md focus:outline-none ${className}`

    return (
        <button
            className={fullClassName}
            type="button"
            title="Delete"
            {...props}
        >
            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={2}
                 stroke="currentColor" className="text-neutral-400 hover:text-neutral-100" height="18" width="18">
                <path strokeLinecap="round" strokeLinejoin="round" d="M6 18L18 6M6 6l12 12"/>
            </svg>
        </button>
    );
};