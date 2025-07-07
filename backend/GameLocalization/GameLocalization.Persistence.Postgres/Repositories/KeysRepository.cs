using GameLocalization.Application.Interfaces.Persistence;
using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;
using GameLocalization.Persistence.Postgres.Connection;
using GameLocalization.Persistence.Postgres.Entities;
using GameLocalization.Persistence.Postgres.Interfaces.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameLocalization.Persistence.Postgres.Repositories;

internal sealed class KeysRepository(Context context, IKeyMapper keyMapper) : IKeysRepository
{
    public async Task<Result<PaginatedResult<Key>>> GetKeysWithTranslationsAsync(Guid projectId, int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0)
            return Result<PaginatedResult<Key>>.Failure("Invalid pagination parameters.");

        var query = context.Keys
            .AsNoTracking()
            .Include(k => k.Translations)
            .Where(k => k.ProjectId == projectId)
            .OrderBy(k => k.CreatedAt);

        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var keysResult = keyMapper.ToDomain(items);
        if (keysResult.IsFailure) return Result<PaginatedResult<Key>>.Failure(keysResult.ErrorMessage!, keysResult.Code);
        
        var result = PaginatedResult<Key>.Create(keysResult.Value!, total, page, pageSize);
        return Result<PaginatedResult<Key>>.Success(result);
    }
    
    public async Task<Result<PaginatedResult<Key>>> SearchKeysAsync(Guid projectId, string query, int page, 
        int pageSize)
    {
        if (page <= 0 || pageSize <= 0)
            return Result<PaginatedResult<Key>>.Failure("Invalid pagination parameters.");
        
        if (string.IsNullOrWhiteSpace(query))
            return Result<PaginatedResult<Key>>.ValidationFailure("Search query must not be empty");

        var dataQuery = context.Keys
            .AsNoTracking()
            .Include(k => k.Translations)
            .Where(k => k.ProjectId == projectId && k.Name.Contains(query))
            .OrderBy(k => k.CreatedAt);
        
        var total = await dataQuery.CountAsync();

        var items = await dataQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var keysResult = keyMapper.ToDomain(items);
        if (keysResult.IsFailure) return Result<PaginatedResult<Key>>.Failure(keysResult.ErrorMessage!, keysResult.Code);
        
        var result = PaginatedResult<Key>.Create(keysResult.Value!, total, page, pageSize);
        return Result<PaginatedResult<Key>>.Success(result);
    }
    
    public async Task<Result<Key>> GetByIdAsync(Guid projectId, Guid id)
    {
        var keyEntity = await context.Keys
            .AsNoTracking()
            .Include(k => k.Translations)
            .FirstOrDefaultAsync(k => k.Id == id && k.ProjectId == projectId);

        var keyResult = keyMapper.ToDomain(keyEntity ?? new KeyEntity());
        return keyResult.IsFailure 
            ? Result<Key>.Failure(keyResult.ErrorMessage!, keyResult.Code) 
            : Result<Key>.Success(keyResult.Value!);
    }

    public async Task<Result<Guid>> AddAsync(Key key)
    {
        var existingProject = await context.Projects.AnyAsync(p => p.Id == key.ProjectId);
        if (!existingProject) return Result<Guid>.NotFound($"Project with id {key.ProjectId} not found.");
        
        var exists = await context.Keys
            .AnyAsync(k => k.ProjectId == key.ProjectId && k.Name == key.Name.Value);
        if (exists) return Result<Guid>.Failure($"Key {key.Name.Value} already exists in project");

        var keyEntityResult = keyMapper.ToEntity(key);
        if (keyEntityResult.IsFailure) return Result<Guid>.Failure(keyEntityResult.ErrorMessage!, keyEntityResult.Code);
        
        context.Keys.Add(keyEntityResult.Value!);
        await context.SaveChangesAsync();
        
        return Result<Guid>.Success(key.Id);
    }

    public async Task<Result> DeleteAsync(Guid projectId, Guid keyId)
    {
        var key = await context.Keys.FirstOrDefaultAsync(k => k.Id == keyId && k.ProjectId == projectId);
        if (key is null) return Result.NotFound("Key not found");

        context.Keys.Remove(key);
        await context.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result> UpdateAsync(Key key)
    {
        var duplicateExists = await context.Keys
            .AnyAsync(k => k.ProjectId == key.ProjectId && k.Name == key.Name.Value && k.Id != key.Id);
        
        if (duplicateExists)
            return Result.Failure($"A key with name \"{key.Name.Value}\" already exists in this project.");
        
        var totalUpdated = await context.Keys
            .Where(k => k.Id == key.Id && k.ProjectId == key.ProjectId)
            .ExecuteUpdateAsync(k => k
                .SetProperty(entity => entity.Name, key.Name.Value)
                .SetProperty(entity => entity.UpdatedAt, key.UpdatedAt)
            );
        
        return totalUpdated == 0 
            ? Result.NotFound($"Key with id {key.Id} not found.") 
            : Result.Success();
    }
}