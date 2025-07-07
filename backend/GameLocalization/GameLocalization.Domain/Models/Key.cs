using GameLocalization.Common.Results;
using GameLocalization.Domain.ValueObjects;

namespace GameLocalization.Domain.Models;

public sealed class Key
{
    private readonly List<Translation> _translations;
    
    public Guid Id { get; private set; }
    public Guid ProjectId { get; private set; }
    public KeyName Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyCollection<Translation> Translations => _translations.AsReadOnly();

    private Key(Guid id, Guid projectId, KeyName name, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        ProjectId = projectId;
        Name = name;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _translations = new List<Translation>();
    }
    
    public static Result<Key> Create(Guid projectId, string name, Guid? id = null, DateTime? createdAt = null, 
        DateTime? updatedAt = null)
    {
        if (projectId == Guid.Empty)
            return Result<Key>.Failure("ProjectId cannot be empty.");

        var keyNameResult = KeyName.Create(name);
        if (keyNameResult.IsFailure) return Result<Key>.ValidationFailure(keyNameResult.ErrorMessage!);
        
        if (id is not null && id == Guid.Empty) return Result<Key>.ValidationFailure("Key id cannot be empty.");
        
        var key = new Key(
            id: id ?? Guid.NewGuid(),
            projectId: projectId,
            name: keyNameResult.Value!,
            createdAt: createdAt ?? new DateTime(),
            updatedAt: updatedAt ?? new DateTime()
        );

        return Result<Key>.Success(key);
    }

    public Result UpdateName(string newName)
    {
        var keyNameResult = KeyName.Create(newName);
        if (keyNameResult.IsFailure) return Result<Key>.ValidationFailure(keyNameResult.ErrorMessage!);
        
        Name = keyNameResult.Value!;
        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }

    public Result AddTranslation(Translation translation)
    {
        if (_translations.Any(t => t.LanguageId == translation.LanguageId))
            return Result.Failure($"Translation for language id {translation.LanguageId} already exists.");

        _translations.Add(translation);
        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }

    public Result RemoveTranslation(Guid languageId)
    {
        var translation = _translations.FirstOrDefault(t => t.LanguageId == languageId);
        if (translation == null)
            return Result.NotFound($"Translation for language ID {languageId} not found.");

        _translations.Remove(translation);
        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }
}