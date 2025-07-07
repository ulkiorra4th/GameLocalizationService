import SearchKey from './SearchKey';
import PageSizeSelector from './PageSizeSelector';
import { Button } from '../shared/Button';
import type {PaginatedTable} from "../../utils/types.ts";

interface TableToolbarProps {
    projectId: string;
    page: number;
    pageSize: number;
    query: string;
    onQueryChange: (query: string) => void;
    onResult: (data?: PaginatedTable) => void;
    onPageSizeChange: (size: number) => void;
    currentPage: number;
    totalPages: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
    onPageChange: (page: number) => void;
    className?: string;
}

export default function TableToolbar({
    projectId,
    page,
    pageSize,
    query,
    onQueryChange,
    onResult,
    onPageSizeChange,
    currentPage,
    totalPages,
    hasPreviousPage,
    hasNextPage,
    onPageChange,
    className = '',
}: TableToolbarProps) {
    return (
        <div className={`flex gap-4 justify-start mb-4 mt-24 relative ${className}`}>
            <SearchKey
                projectId={projectId}
                page={page}
                pageSize={pageSize}
                onResult={onResult}
                query={query}
                onQueryChange={onQueryChange}
                className="absolute bottom-0"
            />
            <div className="flex gap-4 items-center justify-between absolute right-0 bottom-0">
                <PageSizeSelector value={pageSize} onChange={onPageSizeChange} />
                <div className="flex justify-end items-center gap-2">
                    <Button
                        onClick={() => onPageChange(Math.max(currentPage - 1, 1))}
                        disabled={!hasPreviousPage}
                        variant="secondary"
                    >
                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={2} stroke="currentColor" className="w-5 h-5 mx-auto inline-block">
                            <path strokeLinecap="round" strokeLinejoin="round" d="M15.75 19.5L8.25 12l7.5-7.5" />
                        </svg>
                    </Button>
                    <span className="text-white">
                        {currentPage} of {totalPages}
                    </span>
                    <Button
                        onClick={() => onPageChange(currentPage + 1)}
                        disabled={!hasNextPage}
                        variant="secondary"
                    >
                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={2} stroke="currentColor" className="w-5 h-5 inline-block">
                            <path strokeLinecap="round" strokeLinejoin="round" d="M8.25 4.5l7.5 7.5-7.5 7.5" />
                        </svg>
                    </Button>
                </div>
            </div>
        </div>
    );
} 