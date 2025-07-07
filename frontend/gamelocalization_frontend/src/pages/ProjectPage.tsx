import TableComponent from '../components/table/Table.tsx';
import { Button } from '../components/shared/Button.tsx';
import AddLanguageDialog from '../components/dialogs/AddLanguageDialog.tsx';
import AddKeyDialog from '../components/dialogs/AddKeyDialog.tsx';
import ProjectsList from "../components/projects/ProjectsList.tsx";
import { useProjectPage } from '../hooks/pages/useProjectPage.ts';
import TableToolbar from '../components/table/TableToolbar.tsx';
import ProjectHeader from '../components/projects/ProjectHeader.tsx';

export function ProjectPage() {
    const {
        projectId,
        page,
        pageSize,
        showAddLanguage,
        setShowAddLanguage,
        showAddKey,
        setShowAddKey,
        searchResults,
        setSearchResults,
        query,
        handleQueryChange,
        handlePageSizeChange,
        handleEditClick,
        handleSaveClick,
        handleProjectChange,
        isEditing,
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
        setPage,
    } = useProjectPage();

    if (error) return <div className="container mx-auto p-4 text-red-600">Error: {error.message}</div>;

    return (
        <div className="min-h-screen max-w-screen container bg-neutral-900 flex gap-4">
            <main className='px-8 min-w-4xl max-w-350 w-full' >
                <div className="flex gap-6 my-16 items-center">
                    <ProjectHeader
                        isEditing={isEditing}
                        editName={editName}
                        setEditName={setEditName}
                        onEdit={handleEditClick}
                        onSave={handleSaveClick}
                        isPending={updateProject.isPending}
                        title={project.data?.data?.name?.value || 'loading...'}
                        createdAt={project.data?.data?.createdAt}
                    />
                </div>

                <div className='px-6'>
                    <TableToolbar
                        projectId={projectId}
                        page={page}
                        pageSize={pageSize}
                        query={query}
                        onQueryChange={handleQueryChange}
                        onResult={setSearchResults}
                        onPageSizeChange={handlePageSizeChange}
                        currentPage={currentPage}
                        totalPages={totalPages}
                        hasPreviousPage={!!hasPreviousPage}
                        hasNextPage={!!hasNextPage}
                        onPageChange={page => setPage(() => page)}
                    />

                    {(isSearching && searchResults) ? (
                        <TableComponent onAddLanguageButtonClick={setShowAddLanguage} data={{ ...searchResults, rows: {
                            ...searchResults.rows,
                                items: searchResults.rows.items
                        } }} projectId={projectId} page={page} pageSize={pageSize} />
                    ) : (data && data.data && (
                        <TableComponent onAddLanguageButtonClick={setShowAddLanguage} data={data.data} projectId={projectId} page={page} pageSize={pageSize} />
                    ))}

                    <Button className="mt-4 w-63" onClick={() => setShowAddKey(true)}>
                        Add key
                    </Button>
                </div>
            </main>

            <ProjectsList currentProjectId={projectId} onProjectOpen={handleProjectChange}/>

            <AddLanguageDialog
                open={showAddLanguage}
                onClose={() => setShowAddLanguage(false)}
                projectId={projectId}
            />

            <AddKeyDialog
                open={showAddKey}
                onClose={() => setShowAddKey(false)}
                projectId={projectId}
                page={page}
                pageSize={pageSize}
            />
        </div>
    );
}