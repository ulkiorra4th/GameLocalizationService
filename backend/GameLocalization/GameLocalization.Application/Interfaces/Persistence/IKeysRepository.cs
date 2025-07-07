using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;

namespace GameLocalization.Application.Interfaces.Persistence;

public interface IKeysRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<Result<PaginatedResult<Key>>> GetKeysWithTranslationsAsync(Guid projectId, int page, int pageSize);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result<Key>> GetByIdAsync(Guid projectId, Guid id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="query"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<Result<PaginatedResult<Key>>> SearchKeysAsync(Guid projectId, string query, int page, int pageSize);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<Result<Guid>> AddAsync(Key key);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="keyId"></param>
    /// <returns></returns>
    Task<Result> DeleteAsync(Guid projectId, Guid keyId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<Result> UpdateAsync(Key key);
}