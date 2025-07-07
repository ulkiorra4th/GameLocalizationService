using Asp.Versioning;
using GameLocalization.Application.Dto.Project;
using GameLocalization.Application.Services.Interfaces;
using GameLocalization.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GameLocalization.Controllers.v1;

/// <summary>
/// By default, there is a Demo Project in system. You cannot create a new one.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/projects/")]
[ApiVersion("1.0")]
public class ProjectsController(IProjectsService projectsService, ILogger<ProjectsController> logger) : Controller
{
    /// <summary>
    /// Gets all projects
    /// </summary>
    /// <returns>Project</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var getResult = await projectsService.GetAllProjectsAsync();
        
        if (getResult.IsFailure) logger.LogInformation(getResult.ErrorMessage);
        return getResult.ToActionResult();
    }
    
    /// <summary>
    /// Gets the project by id
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns>Project</returns>
    [HttpGet("{projectId}")]
    public async Task<IActionResult> Get(Guid projectId)
    {
        var getResult = await projectsService.GetProjectByIdAsync(projectId);
        
        if (getResult.IsFailure) logger.LogInformation(getResult.ErrorMessage);
        return getResult.ToActionResult();
    }

    /// <summary>
    /// Updates project by id
    /// </summary>
    /// <param name="projectId">The identifier of the project</param>
    /// <param name="dto">Updating fields</param>
    [HttpPut("{projectId}")]
    public async Task<IActionResult> Update(Guid projectId, [FromBody] ProjectDto dto)
    {
        var updateProjectDto = new UpdateProjectDto(projectId, dto.Name);
        var updateResult = await projectsService.UpdateProjectAsync(updateProjectDto);
        
        if (updateResult.IsFailure) logger.LogInformation(updateResult.ErrorMessage);
        return updateResult.ToActionResult();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProjectDto dto)
    {
        var createProjectDto = new CreateProjectDto(dto.Name);
        var createResult = await projectsService.CreateProjectAsync(createProjectDto);
        
        if (createResult.IsFailure) logger.LogInformation(createResult.ErrorMessage);
        return createResult.ToActionResult();
    }
}