using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;

namespace GameLocalization.Application.Interfaces.Persistence;

public interface ITranslationsRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyId"></param>
    /// <param name="languageId"></param>
    /// <returns></returns>
    Task<Result<Translation>> GetTranslationAsync(Guid keyId, Guid languageId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<Result<IEnumerable<Translation>>> GetTranslationsByProjectAsync(Guid projectId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="translation"></param>
    /// <returns></returns>
    Task<Result<Guid>> CreateOrUpdateTranslationAsync(Translation translation);
}