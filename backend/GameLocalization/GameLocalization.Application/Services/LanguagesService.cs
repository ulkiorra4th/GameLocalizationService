using GameLocalization.Application.Dto.Language;
using GameLocalization.Application.Interfaces.Persistence;
using GameLocalization.Application.Services.Interfaces;
using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;

namespace GameLocalization.Application.Services;

internal sealed class LanguagesService(ILanguagesRepository languagesRepository) : ILanguagesService
{
    public async Task<Result<IEnumerable<Language>>> GetGlobalLanguagesAsync() =>
        await languagesRepository.GetGlobalLanguagesAsync();
    
    public async Task<Result<IEnumerable<Language>>> GetLanguagesInProjectAsync(Guid projectId) =>
        await languagesRepository.GetAvailableForProjectAsync(projectId);

    public async Task<Result<IEnumerable<Language>>> GetAddedToProjectLanguages(Guid projectId) =>
        await languagesRepository.GetAddedToProjectLanguages(projectId);

    public async Task<Result> AddLanguageToProject(Guid languageId, Guid projectId) =>
        await languagesRepository.AddLanguageToProject(languageId, projectId);

    public async Task<Result> RemoveLanguageFromProject(Guid languageId, Guid projectId) =>
        await languagesRepository.RemoveLanguageFromProject(languageId, projectId);

    public async Task<Result> DeleteAsync(Guid projectId, Guid languageId) =>
        await languagesRepository.DeleteAsync(projectId, languageId);

    public async Task<Result<Guid>> CreateAsync(CreateLanguageDto dto)
    {
        var languageResult = Language.Create(
            id: Guid.NewGuid(),
            code: dto.Code,
            name: dto.Name,
            isCustom: true,
            projectId: dto.ProjectId
        );

        return languageResult.IsFailure
            ? Result<Guid>.Failure(languageResult.ErrorMessage!, languageResult.Code)
            : await languagesRepository.CreateAsync(languageResult.Value!);
    }
}