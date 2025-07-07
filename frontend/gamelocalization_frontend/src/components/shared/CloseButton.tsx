import React from 'react';

export const CloseButton: React.FC<React.ButtonHTMLAttributes<HTMLButtonElement>> = ({...props}) => {
    return (
        <button
            className="absolute top-3 right-3 text-neutral-400 hover:text-red-500 text-2xl font-bold focus:outline-none"
            aria-label="Закрыть"
            type="button"
            {...props}
        >
            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={2}
                 stroke="currentColor" className="w-6 h-6">
                <path strokeLinecap="round" strokeLinejoin="round" d="M6 18L18 6M6 6l12 12"/>
            </svg>
        </button>
    );
};