using GameLocalization.Application.Dto.Key;
using GameLocalization.Application.Interfaces.Persistence;
using GameLocalization.Application.Services.Interfaces;
using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;

namespace GameLocalization.Application.Services;

internal sealed class KeysService(IKeysRepository keysRepository) : IKeysService
{
    public async Task<Result<PaginatedResult<Key>>> GetKeysWithTranslationsAsync(Guid projectId, int page, 
        int pageSize) => await keysRepository.GetKeysWithTranslationsAsync(projectId, page, pageSize);
    
    public async Task<Result<PaginatedResult<Key>>> SearchKeysAsync(Guid projectId, string query, int page,
        int pageSize) => await keysRepository.SearchKeysAsync(projectId, query, page, pageSize);
    
    public async Task<Result<Key>> GetByIdAsync(Guid projectId, Guid id) => 
        await keysRepository.GetByIdAsync(projectId, id);

    public async Task<Result> DeleteAsync(Guid projectId, Guid keyId) => 
        await keysRepository.DeleteAsync(projectId, keyId);

    public async Task<Result<Guid>> AddAsync(CreateKeyDto dto)
    {
        var keyResult = Key.Create(
            projectId: dto.ProjectId,
            name: dto.Name,
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow);

        return keyResult.IsFailure
            ? Result<Guid>.Failure(keyResult.ErrorMessage!, keyResult.Code)
            : await keysRepository.AddAsync(keyResult.Value!);
    }
    
    public async Task<Result> UpdateAsync(UpdateKeyDto dto)
    {
        var keyResult = Key.Create(
            projectId: dto.ProjectId,
            id: dto.KeyId,
            name: dto.Name,
            updatedAt: DateTime.UtcNow);

        return keyResult.IsFailure
            ? Result.Failure(keyResult.ErrorMessage!, keyResult.Code)
            : await keysRepository.UpdateAsync(keyResult.Value!);
    }
}