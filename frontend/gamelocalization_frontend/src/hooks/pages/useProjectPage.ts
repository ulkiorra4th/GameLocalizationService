import { useState } from 'react';
import { useTable } from '../table/useTable.ts';
import { useProject } from '../project/useProject.ts';
import { useUpdateProject } from '../project/useUpdateProject.ts';
import type { PaginatedTable } from '../../utils/types.ts';

export function useProjectPage() {
    const [projectId, setProject] = useState(() => localStorage.getItem('projectId')
        || '11111111-1111-1111-1111-111111111111'); // Demo-project by default
    const [page, setPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [showAddLanguage, setShowAddLanguage] = useState(false);
    const [showAddKey, setShowAddKey] = useState(false);
    const [searchResults, setSearchResults] = useState<PaginatedTable | undefined>(undefined);
    const [query, setQuery] = useState('');
    const { data, error } = useTable(projectId, page, pageSize);
    const project = useProject(projectId);
    const [isEditing, setIsEditing] = useState(false);
    const [editName, setEditName] = useState('');
    const updateProject = useUpdateProject();

    function handleQueryChange(newQuery: string) {
        setQuery(newQuery);
        setPage(1);
    }

    function handlePageSizeChange(newPageSize: number) {
        setPage(1);
        setPageSize(newPageSize);
    }

    function handleEditClick() {
        setEditName(project.data?.data?.name?.value || '');
        setIsEditing(true);
    }

    function handleSaveClick() {
        if (!editName.trim()) return;
        updateProject.mutate({
            projectId,
            data: { name: editName  }
        }, {
            onSuccess: () => setIsEditing(false)
        });
    }

    function handleProjectChange(newProjectId: string) {
        setProject(newProjectId);
        localStorage.setItem('projectId', newProjectId);
    }

    const isSearching = query.trim() !== '' && searchResults;
    const currentPage = isSearching ? searchResults?.rows?.page || 1 : data?.data?.rows?.page || 1;
    const totalPages = isSearching ? searchResults?.rows?.totalPages || 1 : data?.data?.rows?.totalPages || 1;
    const hasPreviousPage = isSearching ? searchResults?.rows?.hasPreviousPage : data?.data?.rows?.hasPreviousPage;
    const hasNextPage = isSearching ? searchResults?.rows?.hasNextPage : data?.data?.rows?.hasNextPage;

    return {
        projectId,
        setProject,
        page,
        setPage,
        pageSize,
        setPageSize,
        showAddLanguage,
        setShowAddLanguage,
        showAddKey,
        setShowAddKey,
        searchResults,
        setSearchResults,
        query,
        setQuery,
        handleQueryChange,
        handlePageSizeChange,
        handleEditClick,
        handleSaveClick,
        handleProjectChange,
        isEditing,
        setIsEditing,
        editName,
        setEditName,
        updateProject,
        project,
        data,
        error,
        isSearching,
        currentPage,
        totalPages,
        hasPreviousPage,
        hasNextPage,
    };
} 