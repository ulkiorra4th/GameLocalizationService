using GameLocalization.Application.Dto.Presentation;
using GameLocalization.Application.Interfaces.Persistence;
using GameLocalization.Application.Services.Interfaces;
using GameLocalization.Common.Results;

namespace GameLocalization.Application.Services;

internal sealed class TableService(
    ITranslationsRepository translationsRepository,
    ILanguagesRepository languagesRepository,
    IKeysRepository keysRepository)
    : ITableService
{
    public async Task<Result<TableDto>> GetTableAsync(Guid projectId, int page, int pageSize)
    {
        var languagesResult = await languagesRepository.GetAddedToProjectLanguages(projectId);
        if (languagesResult.IsFailure) return Result<TableDto>.Failure(languagesResult.ErrorMessage!, 
            languagesResult.Code);

        var languages = languagesResult.Value!.Select(l =>
            new LanguageDto(l.Id, l.Code, l.Name)).ToList();
        
        var keysResult = await keysRepository.GetKeysWithTranslationsAsync(projectId, page, pageSize);
        if (keysResult.IsFailure) return Result<TableDto>.Failure(keysResult.ErrorMessage!, keysResult.Code);
        
        var keys = keysResult.Value!.Items.ToList();

        var translationsResult = await translationsRepository.GetTranslationsByProjectAsync(projectId);
        if (translationsResult.IsFailure) 
            return Result<TableDto>.Failure(translationsResult.ErrorMessage!, translationsResult.Code);

        var translationsLookup = translationsResult.Value!
            .GroupBy(t => (t.KeyId, t.LanguageId))
            .ToDictionary(g => g.Key, g => g.First().Value);
        
        var rows = keys.Select(k =>
        {
            var cells = languages.Select(lang =>
            {
                translationsLookup.TryGetValue((k.Id, lang.LanguageId), out var value);
                return new TableCellDto(k.Id, lang.LanguageId, value?.Value);
            }).ToList();

            return new TableRowDto(new KeyDto(k.Id, k.Name.Value), cells);
        });
        
        var paginatedRows = PaginatedResult<TableRowDto>.Create(
            rows,
            keysResult.Value.TotalCount,
            page,
            pageSize
        );
        
        return Result<TableDto>.Success(new TableDto(languages, paginatedRows));
    }
    
    public async Task<Result<TableDto>> SearchRowsAsync(Guid projectId, string query, int page, int pageSize)
    {
        var languagesResult = await languagesRepository.GetAddedToProjectLanguages(projectId);
        if (languagesResult.IsFailure) return Result<TableDto>.Failure(languagesResult.ErrorMessage!, 
            languagesResult.Code);

        var languages = languagesResult.Value!.Select(l =>
            new LanguageDto(l.Id, l.Code, l.Name)).ToList();
        
        var keysResult = await keysRepository.SearchKeysAsync(projectId, query, page, pageSize);
        if (keysResult.IsFailure) return Result<TableDto>.Failure(keysResult.ErrorMessage!, keysResult.Code);
        
        var keys = keysResult.Value!.Items.ToList();

        var translationsResult = await translationsRepository.GetTranslationsByProjectAsync(projectId);
        if (translationsResult.IsFailure) 
            return Result<TableDto>.Failure(translationsResult.ErrorMessage!, translationsResult.Code);

        var translationsLookup = translationsResult.Value!
            .GroupBy(t => (t.KeyId, t.LanguageId))
            .ToDictionary(g => g.Key, g => g.First().Value);
        
        var rows = keys.Select(k =>
        {
            var cells = languages.Select(lang =>
            {
                translationsLookup.TryGetValue((k.Id, lang.LanguageId), out var value);
                return new TableCellDto(k.Id, lang.LanguageId, value?.Value);
            }).ToList();

            return new TableRowDto(new KeyDto(k.Id, k.Name.Value), cells);
        });
        
        var paginatedRows = PaginatedResult<TableRowDto>.Create(
            rows,
            keysResult.Value.TotalCount,
            page,
            pageSize
        );
        
        return Result<TableDto>.Success(new TableDto(languages, paginatedRows));
    }
}