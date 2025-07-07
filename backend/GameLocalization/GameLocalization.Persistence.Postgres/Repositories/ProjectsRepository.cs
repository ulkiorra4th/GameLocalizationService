using GameLocalization.Application.Interfaces.Persistence;
using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;
using GameLocalization.Persistence.Postgres.Connection;
using GameLocalization.Persistence.Postgres.Interfaces.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameLocalization.Persistence.Postgres.Repositories;

internal sealed class ProjectsRepository(Context context, IProjectMapper mapper) : IProjectsRepository
{
    public async Task<Result<Project>> GetByIdAsync(Guid id)
    {
        var entity = await context.Projects
            .AsNoTracking()
            .Include(p => p.Keys)
                .ThenInclude(k => k.Translations)
                    .ThenInclude(t => t.Language)
            .Include(p => p.CustomLanguages)
            .FirstOrDefaultAsync(p => p.Id == id);

        return entity is null 
            ? Result<Project>.NotFound($"Project with Id {id} not found.") 
            : mapper.ToDomain(entity);
    }

    public async Task<Result<IEnumerable<Project>>> GetAllAsync()
    {
        var entities = await context.Projects
            .AsNoTracking()
            .ToListAsync();

        return mapper.ToDomain(entities);
    }

    public async Task<Result<Guid>> AddAsync(Project project)
    {
        var entityResult = mapper.ToEntity(project);
        if (entityResult.IsFailure) return Result<Guid>.Failure(entityResult.ErrorMessage!, entityResult.Code);

        var existingProject = await context.Projects
            .AnyAsync(p => p.Id == project.Id);

        if (existingProject)
            return Result<Guid>.Failure($"Project with ID {project.Id} " +
                                        $"or name {project.Name.Value} already exists.");

        context.Projects.Add(entityResult.Value!);
        await context.SaveChangesAsync();
        return Result<Guid>.Success(entityResult.Value!.Id);
    }

    public async Task<Result> UpdateAsync(Project project)
    {
        var projectExists = await context.Projects.AnyAsync(p => p.Id == project.Id);
        if (!projectExists) return Result.NotFound($"Project with Id {project.Id} not found.");
        
        var totalUpdated = await context.Projects
            .Where(p => p.Id == project.Id)
            .ExecuteUpdateAsync(p => p
                .SetProperty(entity => entity.Name, project.Name.Value)
                .SetProperty(entity => entity.UpdatedAt, project.UpdatedAt)
            );

        return totalUpdated == 0 
            ? Result.Failure("Failed to update project.") 
            : Result.Success();
    }
}