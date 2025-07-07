using Asp.Versioning;
using GameLocalization.Application.Dto.Translation;
using GameLocalization.Application.Services.Interfaces;
using GameLocalization.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GameLocalization.Controllers.v1;

/// <summary>
/// Project translations.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/projects/{projectId}/translations")]
[ApiVersion("1.0")]
public class TranslationsController(ITranslationsService translationsService, ILogger<TranslationsController> logger) 
    : Controller
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPut]
    public async Task<IActionResult> Update([FromRoute] Guid projectId, [FromBody] CreateOrUpdateTranslationDto dto)
    {
        var updateResult = await translationsService.CreateOrUpdateTranslationAsync(dto);
        
        if (updateResult.IsFailure) logger.LogInformation(updateResult.ErrorMessage);
        return updateResult.ToActionResult();
    }
}