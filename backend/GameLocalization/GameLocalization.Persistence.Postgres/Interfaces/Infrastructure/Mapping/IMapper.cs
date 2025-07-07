using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;
using GameLocalization.Persistence.Postgres.Entities;

namespace GameLocalization.Persistence.Postgres.Interfaces.Infrastructure.Mapping;

public interface IMapper<TEntity, TDomain>
{
    Result<TDomain> ToDomain(TEntity entity);
    Result<TEntity> ToEntity(TDomain domain);

    Result<IEnumerable<TDomain>> ToDomain(IEnumerable<TEntity> entities)
    {
        var resultList = new List<TDomain>();
        
        foreach (var entity in entities)
        {
            var result = ToDomain(entity);
            if (result.IsFailure) return Result<IEnumerable<TDomain>>.Failure(result.ErrorMessage!, result.Code);
            
            resultList.Add(result.Value!);
        }
        
        return Result<IEnumerable<TDomain>>.Success(resultList);
    }
    
    Result<IEnumerable<TEntity>> ToEntity(IEnumerable<TDomain> domains)
    {
        var resultList = new List<TEntity>();

        foreach (var domain in domains)
        {
            var result = ToEntity(domain);
            if (result.IsFailure) return Result<IEnumerable<TEntity>>.Failure(result.ErrorMessage!, result.Code);
            
            resultList.Add(result.Value!);
        }
        
        return Result<IEnumerable<TEntity>>.Success(resultList);
    }
}

public interface IProjectMapper : IMapper<ProjectEntity, Project>;
public interface IKeyMapper : IMapper<KeyEntity, Key>;
public interface ITranslationMapper : IMapper<TranslationEntity, Translation>;
public interface ILanguageMapper : IMapper<LanguageEntity, Language>;