using GameLocalization.Application.Dto.Project;
using GameLocalization.Application.Interfaces.Persistence;
using GameLocalization.Application.Services.Interfaces;
using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;

namespace GameLocalization.Application.Services;

internal sealed class ProjectsService(IProjectsRepository projectsRepository) : IProjectsService
{
    public async Task<Result<Project>> GetProjectByIdAsync(Guid id) => 
        await projectsRepository.GetByIdAsync(id);

    public async Task<Result<IEnumerable<Project>>> GetAllProjectsAsync() => 
        await projectsRepository.GetAllAsync();
    
    public async Task<Result<Guid>> CreateProjectAsync(CreateProjectDto dto)
    {
        var projectResult = Project.Create(
            id: Guid.NewGuid(),
            name: dto.Name.Trim(),
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow
        );

        return projectResult.IsFailure
            ? Result<Guid>.Failure(projectResult.ErrorMessage!, projectResult.Code)
            : await projectsRepository.AddAsync(projectResult.Value!);
    }

    public async Task<Result> UpdateProjectAsync(UpdateProjectDto dto)
    {
        var projectResult = Project.Create(
            id: dto.ProjectId,
            name: dto.Name.Trim(),
            updatedAt: DateTime.UtcNow
        );

        return projectResult.IsFailure
            ? Result.Failure(projectResult.ErrorMessage!, projectResult.Code)
            : await projectsRepository.UpdateAsync(projectResult.Value!);
    }
}