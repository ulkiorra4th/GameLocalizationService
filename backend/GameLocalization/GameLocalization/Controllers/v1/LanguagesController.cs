using Asp.Versioning;
using GameLocalization.Application.Dto.Language;
using GameLocalization.Application.Services.Interfaces;
using GameLocalization.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GameLocalization.Controllers.v1;

/// <summary>
/// Translation languages.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/projects/{projectId}/languages")]
[ApiVersion("1.0")]
public class LanguagesController(ILanguagesService languagesService, ILogger<LanguagesController> logger) : Controller
{
    /// <summary>
    /// Gets global languages
    /// </summary>
    /// <returns></returns>
    [HttpGet("/api/v{version:apiVersion}/languages")]
    public async Task<IActionResult> GetSharedLanguages()
    {
        var languagesResult = await languagesService.GetGlobalLanguagesAsync();
        
        if (languagesResult.IsFailure) logger.LogInformation(languagesResult.ErrorMessage);
        return languagesResult.ToActionResult();
    }
    
    /// <summary>
    /// Creates a new language
    /// </summary>
    /// <param name="dto">Adding language</param>
    /// <returns>Guid of created language</returns>
    [HttpPost("/api/v{version:apiVersion}/languages")]
    public async Task<IActionResult> Create([FromBody] CreateLanguageDto dto)
    {
        var addResult = await languagesService.CreateAsync(dto);
        return addResult.ToActionResult();
    }

    /// <summary>
    /// Deletes custom language
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpDelete("/api/v{version:apiVersion}/languages{languageId}")]
    public async Task<IActionResult> Delete([FromBody] DeleteLanguageDto dto)
    {
        var deleteResult = await languagesService.DeleteAsync(dto.ProjectId, dto.LanguageId);
        
        if (deleteResult.IsFailure) logger.LogInformation(deleteResult.ErrorMessage);
        return deleteResult.ToActionResult();
    }
    
    /// <summary>
    /// Gets available languages for project
    /// </summary>
    /// <param name="projectId">The identifier of the project</param>
    /// <returns>Collection of languages.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAvailable(Guid projectId)
    {
        var languagesResult = await languagesService.GetLanguagesInProjectAsync(projectId);
        
        if (languagesResult.IsFailure) logger.LogInformation(languagesResult.ErrorMessage);
        return languagesResult.ToActionResult();
    }
    
    /// <summary>
    /// Adds language to project
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="languageId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost("{languageId}")]
    public async Task<IActionResult> AddToProject(Guid projectId, Guid languageId)
    {
        var addResult = await languagesService.AddLanguageToProject(languageId, projectId);
        
        if (addResult.IsFailure) logger.LogInformation(addResult.ErrorMessage);
        return addResult.ToActionResult();
    }
    
    /// <summary>
    /// Removes language from the project
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="languageId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpDelete("{languageId}")]
    public async Task<IActionResult> RemoveFromProject(Guid projectId, Guid languageId)
    {
        var removeResult = await languagesService.RemoveLanguageFromProject(languageId, projectId);
        
        if (removeResult.IsFailure) logger.LogInformation(removeResult.ErrorMessage);
        return removeResult.ToActionResult();
    }
}