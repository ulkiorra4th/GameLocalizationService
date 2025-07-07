import { useForm } from 'react-hook-form';
import { Button } from '../shared/Button.tsx';
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogFooter } from '../shared/Dialog.tsx';
import { useAddKey } from '../../hooks/key/useAddKey.ts';
import { Input } from '../shared/Input.tsx';

interface FormData {
    name: string;
}

interface AddKeyDialogProps {
    open: boolean;
    onClose: () => void;
    projectId: string;
    page: number;
    pageSize: number;
}

export default function AddKeyDialog({ open, onClose, projectId, page, pageSize }: AddKeyDialogProps) {
    const { mutate: addKey, isPending } = useAddKey();
    const { register, handleSubmit, reset } = useForm<FormData>({
        defaultValues: { name: '' },
    });

    const onSubmit = (data: FormData) => {
        if (!data.name.trim()) {
            return;
        }
        addKey({ projectId, name: data.name, page, pageSize }, { onSuccess: () => { onClose(); reset(); } });
    };

    return (
        <Dialog open={open} onOpenChange={onClose}>
            <DialogContent>
                <DialogHeader>
                    <DialogTitle>Add Key</DialogTitle>
                </DialogHeader>
                <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
                    <Input
                        label="Key Name"
                        className="text-white"
                        placeholder="Key name"
                        {...register('name')}
                    />
                    <DialogFooter>
                        <Button type="submit" disabled={isPending}>Add</Button>
                    </DialogFooter>
                </form>
            </DialogContent>
        </Dialog>
    );
}