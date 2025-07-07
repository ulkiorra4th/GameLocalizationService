using GameLocalization.Application.Dto.Language;
using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;

namespace GameLocalization.Application.Services.Interfaces;

public interface ILanguagesService
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<Result<IEnumerable<Language>>> GetGlobalLanguagesAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<Result<IEnumerable<Language>>> GetLanguagesInProjectAsync(Guid projectId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<Result<IEnumerable<Language>>> GetAddedToProjectLanguages(Guid projectId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="languageId"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<Result> AddLanguageToProject(Guid languageId, Guid projectId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="languageId"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<Result> RemoveLanguageFromProject(Guid languageId, Guid projectId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<Result<Guid>> CreateAsync(CreateLanguageDto dto);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="languageId"></param>
    /// <returns></returns>
    Task<Result> DeleteAsync(Guid projectId, Guid languageId);
}
