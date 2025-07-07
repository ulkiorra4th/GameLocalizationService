using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;
using GameLocalization.Persistence.Postgres.Entities;
using GameLocalization.Persistence.Postgres.Interfaces.Infrastructure.Mapping;

namespace GameLocalization.Infrastructure.Mapping.Mappers;

internal sealed class KeyMapper(ITranslationMapper translationMapper) : IKeyMapper
{
    public Result<Key> ToDomain(KeyEntity entity)
    {
        var keyResult = Key.Create(
            id: entity.Id,
            projectId: entity.ProjectId,
            name: entity.Name,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt
        );
        
        if (keyResult.IsFailure) return Result<Key>.ValidationFailure(keyResult.ErrorMessage!);

        var key = keyResult.Value!;

        foreach (var translationEntity in entity.Translations)
        {
            var translationResult = translationMapper.ToDomain(translationEntity!);
            if (translationResult.IsFailure) return Result<Key>.ValidationFailure(translationResult.ErrorMessage!);

            var addResult = key.AddTranslation(translationResult.Value!);
            if (addResult.IsFailure)
                return Result<Key>.Failure(addResult.ErrorMessage!);
        }

        return Result<Key>.Success(key);
    }

    public Result<KeyEntity> ToEntity(Key domain)
    {
        var entity = new KeyEntity
        {
            Id = domain.Id,
            ProjectId = domain.ProjectId,
            Name = domain.Name.Value,
            CreatedAt = domain.CreatedAt,
            UpdatedAt = domain.UpdatedAt,
            Translations = domain.Translations
                .Select(t => translationMapper.ToEntity(t).Value)
                .ToList()
        };

        return Result<KeyEntity>.Success(entity);
    }
}