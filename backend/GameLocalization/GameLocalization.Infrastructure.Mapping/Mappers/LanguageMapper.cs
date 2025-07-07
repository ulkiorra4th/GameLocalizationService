using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;
using GameLocalization.Persistence.Postgres.Entities;
using GameLocalization.Persistence.Postgres.Interfaces.Infrastructure.Mapping;

namespace GameLocalization.Infrastructure.Mapping.Mappers;

internal sealed class LanguageMapper : ILanguageMapper
{
    public Result<Language> ToDomain(LanguageEntity entity) => 
        Language.Create(id: entity.Id, code: entity.Code, name: entity.Name, isCustom: entity.IsCustom, projectId: 
            entity.ProjectId);
    
    public Result<LanguageEntity> ToEntity(Language domain) =>
        Result<LanguageEntity>.Success(new LanguageEntity
        {
            Id = domain.Id,
            Code = domain.Code,
            Name = domain.Name,
            IsCustom = domain.IsCustom,
            ProjectId = domain.ProjectId
        });
}