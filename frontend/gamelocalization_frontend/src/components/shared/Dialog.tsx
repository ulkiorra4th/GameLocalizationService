import React, { type ReactNode } from 'react';
import { CloseButton } from './CloseButton.tsx';

interface DialogProps {
    open: boolean;
    onOpenChange: (open: boolean) => void;
    children: ReactNode;
}

export const Dialog: React.FC<DialogProps> = ({ open, children, onOpenChange }) => {
    if (!open) return null;
    return (
        <div className="fixed inset-0 bg-black/70 flex items-center justify-center z-50">
            <div className="bg-neutral-900 rounded-xl p-8 w-full max-w-md shadow-2xl border-2 border-neutral-800 relative">
                <CloseButton onClick={() => onOpenChange(false)}></CloseButton>
                <div className="text-slate-900 text-base">
                    {children}
                </div>
            </div>
        </div>
    );
};

export const DialogContent: React.FC<{ children: ReactNode }> = ({ children }) =>
    <>{children}</>;
export const DialogHeader: React.FC<{ children: ReactNode }> = ({ children }) =>
    <div className="mb-4 text-white">{children}</div>;
export const DialogTitle: React.FC<{ children: ReactNode }> = ({ children }) =>
    <h3 className="text-lg font-semibold text-white">{children}</h3>;
export const DialogFooter: React.FC<{ children: ReactNode }> = ({ children }) =>
    <div className="flex justify-end mt-4 text-white">{children}</div>;