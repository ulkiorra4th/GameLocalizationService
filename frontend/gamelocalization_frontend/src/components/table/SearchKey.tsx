import {useSearchRows} from "../../hooks/table/useSearchRows.ts";
import {useEffect} from "react";
import type {PaginatedTable} from "../../utils/types.ts";

interface SearchKeyProps {
  onQueryChange: (query: string) => void;
  onResult: (data?: PaginatedTable) => void;
  page: number;
  pageSize: number;
  projectId: string;
  query: string;
  className?: string;
}

export default function SearchKey({ projectId, page, pageSize, onResult, query, onQueryChange, className }: SearchKeyProps) {
  const { data, isLoading } = useSearchRows({ projectId, query, page, pageSize });
  const baseClassName = "px-4 py-2 border rounded-4xl w-65 hover:border-neutral-300 focus:outline-none focus:ring " +
      "focus:ring-neutral-400 bg-neutral-900 h-8 border-neutral-400";
  
  useEffect(() => {
    if (query.trim() === '') onResult(undefined);
    else if (data && !isLoading) onResult(data.data!);
  }, [data, isLoading, onResult, query, page, pageSize]);

  return (
    <input
      type="search"
      name="search"
      placeholder="Search keys..."
      value={query}
      onChange={e => onQueryChange(e.target.value)}
      className={`${baseClassName} ${className}`}
    />
  );
}
