using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;
using GameLocalization.Persistence.Postgres.Entities;
using GameLocalization.Persistence.Postgres.Interfaces.Infrastructure.Mapping;

namespace GameLocalization.Infrastructure.Mapping.Mappers;

internal sealed class ProjectMapper(IKeyMapper keyMapper) : IProjectMapper
{
    public Result<Project> ToDomain(ProjectEntity entity)
    {
        var projectResult = Project.Create(
            id: entity.Id,
            name: entity.Name,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt
        );
        
        if (projectResult.IsFailure) return Result<Project>.ValidationFailure(projectResult.ErrorMessage!);
        var project = projectResult.Value!;

        foreach (var keyEntity in entity.Keys)
        {
            var keyResult = keyMapper.ToDomain(keyEntity!);
            if (keyResult.IsFailure)
                return Result<Project>.Failure($"Failed to map key: {keyResult.ErrorMessage}");

            var addResult = project.AddKey(keyResult.Value!);
            if (addResult.IsFailure)
                return Result<Project>.Failure($"Failed to add key: {addResult.ErrorMessage}");
        }

        return Result<Project>.Success(project);
    }

    public Result<ProjectEntity> ToEntity(Project domain)
    {
        var entity = new ProjectEntity
        {
            Id = domain.Id,
            Name = domain.Name.Value,
            CreatedAt = domain.CreatedAt,
            UpdatedAt = domain.UpdatedAt,
            Keys = domain.Keys
                .Select(k => keyMapper.ToEntity(k).Value)
                .ToList()
        };

        return Result<ProjectEntity>.Success(entity);
    }
}