using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;
using GameLocalization.Persistence.Postgres.Entities;
using GameLocalization.Persistence.Postgres.Interfaces.Infrastructure.Mapping;

namespace GameLocalization.Infrastructure.Mapping.Mappers;

internal sealed class TranslationMapper : ITranslationMapper
{
    public Result<Translation> ToDomain(TranslationEntity entity)
    {
        var translationResult = Translation.Create(
            id: entity.Id,
            keyId: entity.KeyId,
            languageId: entity.LanguageId,
            value: entity.Value,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt
        );

        return translationResult.IsFailure 
            ? Result<Translation>.Failure(translationResult.ErrorMessage!) 
            : Result<Translation>.Success(translationResult.Value!);
    }

    public Result<TranslationEntity> ToEntity(Translation domain)
    {
        var entity = new TranslationEntity
        {
            Id = domain.Id,
            KeyId = domain.KeyId,
            LanguageId = domain.LanguageId,
            Value = domain.Value.Value,
            CreatedAt = domain.CreatedAt,
            UpdatedAt = domain.UpdatedAt
        };

        return Result<TranslationEntity>.Success(entity);
    }
}