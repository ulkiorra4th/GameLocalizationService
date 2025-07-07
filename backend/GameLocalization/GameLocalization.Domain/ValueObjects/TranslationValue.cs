using GameLocalization.Common.Results;

namespace GameLocalization.Domain.ValueObjects;

public sealed class TranslationValue
{
    public string Value { get; }

    private TranslationValue(string value)
    {
        Value = value;
    }

    public static Result<TranslationValue> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Result<TranslationValue>.ValidationFailure("Translation value cannot be empty.");

        if (value.Length > 500)
            return Result<TranslationValue>.ValidationFailure("Translation value cannot exceed 500 characters.");

        return Result<TranslationValue>.Success(new TranslationValue(value));
    }

    public override bool Equals(object? obj) =>
        obj is TranslationValue other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
}