using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;

namespace GameLocalization.Application.Interfaces.Persistence;

public interface IProjectsRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result<Project>> GetByIdAsync(Guid id);
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<Result<IEnumerable<Project>>> GetAllAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    Task<Result<Guid>> AddAsync(Project project);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    Task<Result> UpdateAsync(Project project);
}