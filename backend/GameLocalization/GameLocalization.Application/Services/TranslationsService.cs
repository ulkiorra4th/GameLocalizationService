using GameLocalization.Application.Dto.Translation;
using GameLocalization.Application.Interfaces.Persistence;
using GameLocalization.Application.Services.Interfaces;
using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;

namespace GameLocalization.Application.Services;

internal sealed class TranslationsService(ITranslationsRepository translationsRepository) : ITranslationsService
{
    public async Task<Result<Translation>> GetTranslationAsync(Guid keyId, Guid languageId) => 
        await translationsRepository.GetTranslationAsync(keyId, languageId);
    
    public async Task<Result<IEnumerable<Translation>>> GetTranslationsByProjectAsync(Guid projectId) =>
        await translationsRepository.GetTranslationsByProjectAsync(projectId);
    
    public async Task<Result<Guid>> CreateOrUpdateTranslationAsync(CreateOrUpdateTranslationDto dto)
    {
        var translationResult = Translation.Create(
            keyId: dto.KeyId,
            languageId: dto.LanguageId,
            value: dto.Value.Trim(),
            updatedAt: DateTime.UtcNow);
        
        return translationResult.IsFailure
            ? Result<Guid>.Failure(translationResult.ErrorMessage!, translationResult.Code)
            : await translationsRepository.CreateOrUpdateTranslationAsync(translationResult.Value!);
    }
}