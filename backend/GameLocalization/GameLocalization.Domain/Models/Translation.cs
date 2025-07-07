using GameLocalization.Common.Results;
using GameLocalization.Domain.ValueObjects;

namespace GameLocalization.Domain.Models;

public sealed class Translation
{
    public Guid Id { get; private set; }
    public Guid KeyId { get; private set; }
    public Guid LanguageId { get; private set; }
    public TranslationValue Value { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    private Translation(Guid id, Guid keyId, Guid languageId, TranslationValue value, DateTime createdAt, 
        DateTime updatedAt)
    {
        Id = id;
        KeyId = keyId;
        LanguageId = languageId;
        Value = value;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Result<Translation> Create(Guid keyId, Guid languageId, string value, Guid? id = null,
        DateTime? createdAt = null, DateTime? updatedAt = null)
    {
        if (keyId == Guid.Empty)
            return Result<Translation>.ValidationFailure("KeyId cannot be empty.");

        var valueResult = TranslationValue.Create(value);
        if (valueResult.IsFailure) return Result<Translation>.ValidationFailure(valueResult.ErrorMessage!);
        
        if (id is not null && id == Guid.Empty) 
            return Result<Translation>.ValidationFailure("Translation id cannot be empty.");
        
        var translation = new Translation(
            id: id ?? Guid.NewGuid(),
            keyId: keyId,
            languageId: languageId,
            value: valueResult.Value!,
            createdAt: createdAt ?? new DateTime(),
            updatedAt: updatedAt ?? new DateTime()
        );

        return Result<Translation>.Success(translation);
    }

    public Result UpdateValue(string newValue)
    {
        var valueResult = TranslationValue.Create(newValue);
        if (valueResult.IsFailure) return Result<Translation>.ValidationFailure(valueResult.ErrorMessage!);
        
        Value = valueResult.Value!;
        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }
}