using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;

namespace GameLocalization.Application.Interfaces.Persistence;

public interface ILanguagesRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<Result<IEnumerable<Language>>> GetAvailableForProjectAsync(Guid projectId);

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
    /// <returns></returns>
    Task<Result<IEnumerable<Language>>> GetGlobalLanguagesAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="language"></param>
    /// <returns></returns>
    Task<Result<Guid>> CreateAsync(Language language);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="languageId"></param>
    /// <returns></returns>
    Task<Result> DeleteAsync(Guid projectId, Guid languageId);
}