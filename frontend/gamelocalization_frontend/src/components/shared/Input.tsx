import React from 'react';

interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {
    label?: string;
}

export const Input = React.forwardRef<HTMLInputElement, InputProps>(
    ({ label, className = '', ...props }, ref) => (
        <div className="flex flex-col gap-1">
            {label && <label className="text-sm text-white hidden">{label}</label>}
            <input
                ref={ref}
                className={`hover:border-neutral-300 border border-neutral-400 rounded-xl px-2 py-1 outline-none 
                focus:outline-none focus:ring focus:ring-neutral-400 ${className}`}
                {...props}
            />
        </div>
    )
);

Input.displayName = 'Input'; 