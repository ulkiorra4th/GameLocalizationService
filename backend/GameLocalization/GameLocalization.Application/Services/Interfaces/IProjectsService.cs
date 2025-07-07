using GameLocalization.Application.Dto.Project;
using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;

namespace GameLocalization.Application.Services.Interfaces;

public interface IProjectsService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result<Project>> GetProjectByIdAsync(Guid id);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<Result<IEnumerable<Project>>> GetAllProjectsAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<Result<Guid>> CreateProjectAsync(CreateProjectDto dto);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<Result> UpdateProjectAsync(UpdateProjectDto dto);
}