using GameLocalization.Application.Dto.Key;
using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;

namespace GameLocalization.Application.Services.Interfaces;

public interface IKeysService
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
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<Result<Guid>> AddAsync(CreateKeyDto dto);

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
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<Result> UpdateAsync(UpdateKeyDto dto);
}