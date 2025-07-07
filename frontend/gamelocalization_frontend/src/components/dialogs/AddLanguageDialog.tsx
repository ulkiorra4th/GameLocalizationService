import { useForm } from 'react-hook-form';
import { Button } from '../shared/Button.tsx';
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogFooter } from '../shared/Dialog.tsx';
import { useAddLanguage } from '../../hooks/language/useAddLanguage.ts';
import {useProjectLanguages} from "../../hooks/language/useProjectLanguages.ts";
import type {Language} from "../../utils/types.ts";
import { useTable } from '../../hooks/table/useTable';

interface FormData {
    languageId: string;
}

interface AddLanguageDialogProps {
    open: boolean;
    onClose: () => void;
    projectId: string;
}

export default function AddLanguageDialog({ open, onClose, projectId }: AddLanguageDialogProps) {
    const { mutate: addLanguage, isPending } = useAddLanguage();
    const { register, handleSubmit, reset } = useForm<FormData>({
        defaultValues: { languageId: '' },
    });
    const {data: availableLangsData} = useProjectLanguages(projectId);
    const {data: tableData} = useTable(projectId, 1, 1);

    const availableLanguages = availableLangsData?.data ?? [];
    const tableLanguages = tableData?.data?.languages ?? [];

    // Исключаем языки, которые уже есть в таблице
    const filteredLanguages = availableLanguages.filter(
        (lang) => !tableLanguages.some((tLang) => tLang.languageId === lang.languageId)
    );

    const onSubmit = (data: FormData) => {
        if (!data.languageId) {
            return; 
        }
        console.log(filteredLanguages);
        addLanguage({ projectId, languageId: data.languageId }, { onSuccess: () => { onClose(); reset(); } });
    };

    return (
        <Dialog open={open} onOpenChange={onClose}>
            <DialogContent>
                <DialogHeader>
                    <DialogTitle>Add Language</DialogTitle>
                </DialogHeader>
                <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
                    <select
                        {...register('languageId')}
                        className="hover:border-neutral-300 border bg-neutral-900 border-neutral-400 rounded-xl px-2
                        py-1 w-full text-neutral-200 outline-none focus:outline-none focus:ring
                    focus:ring-neutral-400"
                    >
                        <option value="">Select a language</option>
                        {(filteredLanguages as Language[]).map((lang) => (
                            <option key={lang.languageId} value={lang.languageId}>{lang.name}</option>
                        ))}
                    </select>
                    <DialogFooter>
                        <Button type="submit" disabled={isPending}>Add</Button>
                    </DialogFooter>
                </form>
            </DialogContent>
        </Dialog>
    );
}