import React from 'react';

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
    variant?: 'primary' | 'secondary';
}

export const Button: React.FC<ButtonProps> = ({variant = 'primary', className = '', children, ...props}) => {
    const baseStyles = 'font-medium focus:outline-none disabled:opacity-50 rounded-xl transition h-9 px-2';
    const variantStyles = variant === 'primary'
        ? 'bg-neutral-700 text-white hover:bg-neutral-600'
        : 'text-white-800 hover:bg-neutral-500 hover:border-0 box-border';

    return (
        <button className={` ${variantStyles} ${baseStyles} ${className}`} {...props}>
            {children}
        </button>
    );
};