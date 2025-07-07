using GameLocalization.Application.Dto.Presentation;
using GameLocalization.Common.Results;

namespace GameLocalization.Application.Services.Interfaces;

public interface ITableService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<Result<TableDto>> GetTableAsync(Guid projectId, int page, int pageSize);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="query"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<Result<TableDto>> SearchRowsAsync(Guid projectId, string query, int page, int pageSize);
}