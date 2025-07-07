using Asp.Versioning;
using GameLocalization.Application.Services.Interfaces;
using GameLocalization.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GameLocalization.Controllers.v1;

/// <summary>
/// Presentation of all keys, languages and translations.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/projects/{projectId}/table")]
[ApiVersion("1.0")]
public class TableController(ITableService tableService, ILogger<TableController> logger) : Controller
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
        var getTableResult = await tableService.GetTableAsync(projectId, page, pageSize);
        
        if (getTableResult.IsFailure) logger.LogInformation(getTableResult.ErrorMessage);
        return getTableResult.ToActionResult();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="query"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet("rows")]
    public async Task<IActionResult> Search(
        [FromRoute] Guid projectId, 
        [FromQuery] string query,
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 20)
    {
        var getTableResult = string.IsNullOrWhiteSpace(query)
        ? await tableService.GetTableAsync(projectId, page, pageSize)
        : await tableService.SearchRowsAsync(projectId, query, page, pageSize);
        
        if (getTableResult.IsFailure) logger.LogInformation(getTableResult.ErrorMessage);
        return getTableResult.ToActionResult();
    }
}