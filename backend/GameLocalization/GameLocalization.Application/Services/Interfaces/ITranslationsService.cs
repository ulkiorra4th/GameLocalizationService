using GameLocalization.Application.Dto.Translation;
using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;

namespace GameLocalization.Application.Services.Interfaces;

public interface ITranslationsService
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
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<Result<Guid>> CreateOrUpdateTranslationAsync(CreateOrUpdateTranslationDto dto);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<Result<IEnumerable<Translation>>> GetTranslationsByProjectAsync(Guid projectId);
}