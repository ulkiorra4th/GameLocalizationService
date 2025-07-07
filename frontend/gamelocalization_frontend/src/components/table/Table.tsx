import React, { type ReactNode, useMemo } from 'react';
import EditableCell from './EditableCell.tsx';
import type { PaginatedTable } from '../../utils/types.ts';
import { useDeleteKey } from '../../hooks/key/useDeleteKey.ts';
import {DeleteButton} from "../shared/DeleteButton.tsx";
import {Button} from "../shared/Button.tsx";
import { useDeleteLanguage } from '../../hooks/language/useDeleteLanguage.ts';
import EditableKeyCell from './EditableKeyCell.tsx';

const cellBase = 'border-b-2 border-r-2 border-neutral-600';
const headBase = `${cellBase} bg-neutral-800 text-left text-white`;
const rowHeadBase = `${cellBase} text-left text-white`;

interface TableProps {
    data: PaginatedTable;
    projectId: string;
    isError?: boolean;
    page?: number;
    pageSize?: number;
    onAddLanguageButtonClick: (clicked: boolean) => void;
}

export default function TableComponent({ data, projectId, isError, page, pageSize, onAddLanguageButtonClick }: TableProps) {
    const deleteKeyMutation = useDeleteKey();
    const deleteLanguageMutation = useDeleteLanguage();

    const evenRow = 'bg-neutral-700';
    const oddRow = 'bg-neutral-800';
    const langCellText = 'text-neutral-200';
    const keyCellWidth = 'w-68 min-w-68';
    const langCellWidth = 'w-120';
    const actionCellWidth = 'w-12';
    const addLangBtn = 'hover:bg-neutral-700 bg-neutral-800 w-full rounded-xs mx-0 my-0 h-10';

    const translationsMap = useMemo(() => {
        return data.rows?.items?.reduce((acc, row) => {
            row.translations.forEach((t) => {
                acc[t.keyId] = acc[t.keyId] || {};
                acc[t.keyId][t.languageId] = t.value;
            });
            return acc;
        }, {} as Record<string, Record<string, string | null>>);
    }, [data.rows?.items]);

    if (isError) return <div className="p-4 text-center text-red-500">Ошибка загрузки данных</div>;

    return (
        <div className="overflow-x-auto max-h-[740px] rounded-sm bg-neutral-800 border-t-2 border-l-2 border-neutral-600">
            <table className="w-full min-w-[800px] max-w-[1500px] border-separate border-spacing-0">
                <thead>
                    <tr>
                        <TableHead custClassName={`${keyCellWidth} px-4 py-2 sticky left-0 top-0 z-20`}>
                            Key
                        </TableHead>

                        {data?.languages?.map((lang) => (
                            <TableHead custClassName={`px-4 py-2 sticky top-0 z-15 relative`} key={lang.languageId}>
                                {lang.name}
                                <DeleteButton className="absolute right-2 top-3"
                                    onClick={() => deleteLanguageMutation.mutate({ projectId, languageId: lang.languageId })}
                                    disabled={deleteLanguageMutation.isPending}
                                >
                                </DeleteButton>
                            </TableHead>
                        ))}

                        <TableHead custClassName={`border-0 sticky left-0 top-0 z-15 w-15 min-w-15`}>
                            <Button className={addLangBtn}
                                onClick={() => onAddLanguageButtonClick(true)}>
                                +
                            </Button>
                        </TableHead>
                    </tr>
                </thead>
                <tbody>
                    {data?.rows?.items?.map((row, idx) => (
                        <TableRow key={row.key.keyId}>

                            <TableRowHead custClassName={`relative font-medium sticky left-0 z-10 
                                ${keyCellWidth} ${(idx % 2 === 0 ? evenRow : oddRow)} text-white`}>
                                <div className="flex items-center relative w-full">
                                    <div className="flex-1 min-w-0">
                                        <EditableKeyCell
                                            projectId={projectId}
                                            keyId={row.key.keyId}
                                            initialValue={row.key.name}
                                        />
                                    </div>
                                    <DeleteButton className="absolute right-1 top-1/2 -translate-y-1/2"
                                        onClick={() =>
                                            deleteKeyMutation.mutate({ projectId, keyId: row.key.keyId, page, pageSize })
                                        }
                                        disabled={deleteKeyMutation.isPending}
                                    >
                                    </DeleteButton>
                                </div>
                            </TableRowHead>

                            {data?.languages?.map((lang) => (
                                <TableCell
                                    key={`${row.key.keyId}-${lang.languageId}`}
                                    custClassName={`${langCellWidth} ${(idx % 2 === 0 ? evenRow : oddRow)} ${langCellText}`}
                                >
                                    <EditableCell
                                        projectId={projectId}
                                        keyId={row.key.keyId}
                                        languageId={lang.languageId}
                                        initialValue={translationsMap[row.key.keyId]?.[lang.languageId] || ''}
                                    />
                                </TableCell>
                            ))}

                            <TableCell custClassName={`${actionCellWidth} ${(idx % 2 === 0 ? evenRow : oddRow)} ${langCellText}`}>{null}</TableCell>
                        </TableRow>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export const TableRow: React.FC<{ children: ReactNode }> = ({ children }) => <tr>{children}</tr>;

export const TableCell: React.FC<{ children: ReactNode; custClassName?: string }> = ({ children, custClassName = '' }) =>
    <td className={`${cellBase} ${custClassName}`}>
        {children}
    </td>;

export const TableHead: React.FC<{ children: ReactNode; custClassName?: string }> = ({ children, custClassName = '' }) =>
    <th className={`${headBase} ${custClassName}`}>
        {children}
    </th>;

export const TableRowHead: React.FC<{ children: ReactNode; custClassName?: string }> = ({ children, custClassName = '' }) =>
    <th className={`${rowHeadBase} ${custClassName}`}>
        {children}
    </th>;