using Asp.Versioning;
using GameLocalization.Application.Dto.Key;
using GameLocalization.Application.Services.Interfaces;
using GameLocalization.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GameLocalization.Controllers.v1;

/// <summary>
/// Translation keys.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/projects/{projectId}/keys")]
[ApiVersion("1.0")]
public class KeysController(IKeysService keysService, ILogger<KeysController> logger) : Controller
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromRoute] Guid projectId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var getResult = await keysService.GetKeysWithTranslationsAsync(projectId, page, pageSize);
        if (getResult.IsFailure) logger.LogInformation(getResult.ErrorMessage);
        return getResult.ToActionResult();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="keyId"></param>
    /// <returns></returns>
    [HttpGet("{keyId}")]
    public async Task<IActionResult> GetById([FromRoute] Guid projectId, Guid keyId)
    {
        var getResult = await keysService.GetByIdAsync(projectId, keyId);
        if (getResult.IsFailure) logger.LogInformation(getResult.ErrorMessage);
        return getResult.ToActionResult();
    }

    /// <summary>
    /// Creates a new key in a project
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="dto"></param>
    /// <returns>Guid of the created key</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromRoute] Guid projectId, [FromBody] KeyDto dto)
    {
        var createKeyDto = new CreateKeyDto(projectId, dto.Name);
        var createResult = await keysService.AddAsync(createKeyDto);
        
        if (createResult.IsFailure) logger.LogInformation(createResult.ErrorMessage);
        return createResult.ToActionResult();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="keyId"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{keyId}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid projectId, 
        [FromRoute] Guid keyId, 
        [FromBody] KeyDto dto)
    {
        var updateKeyDto = new UpdateKeyDto(projectId, keyId, dto.Name);
        var updateResult = await keysService.UpdateAsync(updateKeyDto);

        if (updateResult.IsFailure) logger.LogInformation(updateResult.ErrorMessage);
        return updateResult.ToActionResult();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="keyId"></param>
    /// <returns></returns>
    [HttpDelete("{keyId}")]
    public async Task<IActionResult> Delete([FromRoute] Guid projectId, [FromRoute] Guid keyId)
    {
        var deleteResult = await keysService.DeleteAsync(projectId, keyId);
        
        if (deleteResult.IsFailure) logger.LogInformation(deleteResult.ErrorMessage);
        return deleteResult.ToActionResult();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="query"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromRoute] Guid projectId,
        [FromQuery] string query,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var searchResult = await keysService.SearchKeysAsync(projectId, query, page, pageSize);
        
        if (searchResult.IsFailure) logger.LogInformation(searchResult.ErrorMessage);
        return searchResult.ToActionResult();
    }
}