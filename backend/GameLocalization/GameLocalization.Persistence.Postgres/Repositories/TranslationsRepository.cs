using GameLocalization.Application.Interfaces.Persistence;
using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;
using GameLocalization.Persistence.Postgres.Connection;
using GameLocalization.Persistence.Postgres.Interfaces.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameLocalization.Persistence.Postgres.Repositories;

internal sealed class TranslationsRepository(Context context, ITranslationMapper mapper) : ITranslationsRepository
{
    public async Task<Result<Translation>> GetTranslationAsync(Guid keyId, Guid languageId)
    {
        var translationEntity = await context.Translations
            .AsNoTracking()
            .Where(t => t.KeyId == keyId && t.LanguageId == languageId)
            .FirstOrDefaultAsync();
        
        if (translationEntity is null)
            return Result<Translation>.NotFound($"Translation with key id {keyId} and " +
                                                $"language id {languageId} not found");

        var translationResult = mapper.ToDomain(translationEntity);
        return translationResult.IsFailure 
            ? Result<Translation>.Failure(translationResult.ErrorMessage!, translationResult.Code) 
            : Result<Translation>.Success(translationResult.Value!);
    }

    public async Task<Result<IEnumerable<Translation>>> GetTranslationsByProjectAsync(Guid projectId)
    {
        var entities = await context.Translations
            .AsNoTracking()
            .Include(t => t.Key)
                .ThenInclude(k => k.Project)
            .Include(t => t.Language)
            .Where(t => t.Key.ProjectId == projectId)
            .ToListAsync();

        var translationsResult = mapper.ToDomain(entities);
        
        return translationsResult.IsFailure 
            ? Result<IEnumerable<Translation>>.Failure(translationsResult.ErrorMessage!, translationsResult.Code) 
            : Result<IEnumerable<Translation>>.Success(translationsResult.Value!);
    }

    // TODO: optimize
    // TODO: check projectId
    public async Task<Result<Guid>> CreateOrUpdateTranslationAsync(Translation translation)
    {
        var keyExists = await context.Keys.AnyAsync(k => k.Id == translation.KeyId);
        if (!keyExists) return Result<Guid>.NotFound($"Key with Id {translation.KeyId} not found.");

        var languageExists = await context.Languages.AnyAsync(l => l.Id == translation.LanguageId);
        if (!languageExists) return Result<Guid>.NotFound($"Language with id {translation.LanguageId} not found.");

        var translationExists = await context.Translations
            .AnyAsync(t => t.KeyId == translation.KeyId && t.LanguageId == translation.LanguageId);
        
        if (translationExists)
        {
            var totalUpdated = await context.Translations
                .Where(t => t.KeyId == translation.KeyId && t.LanguageId == translation.LanguageId)
                .ExecuteUpdateAsync(p => p
                    .SetProperty(entity => entity.Value, translation.Value.Value)
                    .SetProperty(entity => entity.UpdatedAt, translation.UpdatedAt)
                );
            
            return totalUpdated == 0 
                ? Result<Guid>.Failure("Failed to update translation.") 
                : Result<Guid>.Success(translation.Id);
        }

        var translationEntityResult = mapper.ToEntity(translation);
        if (translationEntityResult.IsFailure)
            return Result<Guid>.Failure(translationEntityResult.ErrorMessage!, translationEntityResult.Code);

        translationEntityResult.Value!.CreatedAt = translationEntityResult.Value!.UpdatedAt;
        context.Translations.Add(translationEntityResult.Value!);
        await context.SaveChangesAsync();
        return Result<Guid>.Success(translationEntityResult.Value!.Id);
    }
}