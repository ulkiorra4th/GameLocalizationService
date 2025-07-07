using GameLocalization.Application.Interfaces.Persistence;
using GameLocalization.Domain.Models;
using GameLocalization.Common.Results;
using GameLocalization.Persistence.Postgres.Connection;
using GameLocalization.Persistence.Postgres.Entities;
using GameLocalization.Persistence.Postgres.Interfaces.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameLocalization.Persistence.Postgres.Repositories;

internal sealed class LanguagesRepository(Context context, ILanguageMapper mapper) : ILanguagesRepository
{
    public async Task<Result<IEnumerable<Language>>> GetAvailableForProjectAsync(Guid projectId)
    {
        var projectExists = await context.Projects.AnyAsync(p => p.Id == projectId);
        if (!projectExists)
            return Result<IEnumerable<Language>>.NotFound($"Project with Id {projectId} not found.");

        var entities = await context.Languages
            .AsNoTracking()
            .Where(l => l.IsCustom == false || (l.ProjectId != null && l.ProjectId == projectId))
            .ToListAsync();

        return mapper.ToDomain(entities);
    }

    public async Task<Result<IEnumerable<Language>>> GetAddedToProjectLanguages(Guid projectId)
    {
        var existingProject = await context.Projects.AnyAsync(p => p.Id == projectId);
        if (!existingProject) return Result<IEnumerable<Language>>.NotFound($"Project with id {projectId} not found.");
        
        var entities = await context.Languages
            .Where(l => l.UsingProjects.Any(p => p.Id == projectId))
            .ToListAsync();

        return mapper.ToDomain(entities);
    }
    
    public async Task<Result> AddLanguageToProject(Guid languageId, Guid projectId)
    {
        var existingProject = await context.Projects.AnyAsync(p => p.Id == projectId);
        if (!existingProject) return Result.NotFound($"Project with id {projectId} not found.");

        var existingLanguage = await context.Languages.AnyAsync(l => l.Id == languageId);
        if (!existingLanguage) return Result.NotFound($"Language with Id {languageId} not found.");
        
        var languageInTableEntity = new LanguageInTableEntity
        {
            LanguageId = languageId,
            ProjectId = projectId
        };

        context.LanguagesInTables.Add(languageInTableEntity);
        await context.SaveChangesAsync();
        return Result.Success();
    }
    
    public async Task<Result> RemoveLanguageFromProject(Guid languageId, Guid projectId)
    {
        var existingRelation = await context.LanguagesInTables
            .AnyAsync(l => l.ProjectId == projectId && l.LanguageId == languageId);
        if (!existingRelation) 
            return Result.Failure($"Language with Id {languageId} not associated " +
                                  $"with Project with id {projectId}.");
        
        var languageInTableEntity = new LanguageInTableEntity
        {
            LanguageId = languageId,
            ProjectId = projectId
        };

        var removingTranslations = await context.Translations
            .Include(t => t.Key)
            .Where(t => t.LanguageId == languageId && t.Key.ProjectId == projectId)
            .ToListAsync();
            
        context.LanguagesInTables.Remove(languageInTableEntity);
        context.Translations.RemoveRange(removingTranslations);
        
        await context.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid projectId, Guid languageId)
    {
        var language = await context.Languages
            .AsNoTracking()
            .Include(l => l.ProjectCreated)
            .Where(l => l.IsCustom == true && l.ProjectId == projectId && l.Id == languageId)
            .FirstOrDefaultAsync();

        if (language is null)
            return Result.NotFound($"Language with Id {languageId} is not associated with project {projectId}.");

        context.Languages.Remove(language);
        await context.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<Guid>> CreateAsync(Language language)
    {
        var entityResult = mapper.ToEntity(language);
        if (entityResult.IsFailure) return Result<Guid>.Failure(entityResult.ErrorMessage!, entityResult.Code);

        var existingProject = await context.Projects.AnyAsync(p => p.Id == language.ProjectId);
        if (!existingProject) return Result<Guid>.NotFound($"Project with id {language.ProjectId} not found.");
        
        var existingLanguage = await context.Languages
            .AnyAsync(l => l.Code == language.Code || l.Name == language.Name);
        
        if (existingLanguage)
            return Result<Guid>.Failure($"Language {language.Name} or code {language.Code} already exists.");

        context.Languages.Add(entityResult.Value!);
        await context.SaveChangesAsync();
        return Result<Guid>.Success(entityResult.Value!.Id);
    }

    public async Task<Result<IEnumerable<Language>>> GetGlobalLanguagesAsync()
    {
        var entities = await context.Languages
            .AsNoTracking()
            .Where(l => !l.IsCustom)
            .ToListAsync();

        return mapper.ToDomain(entities);
    }
}