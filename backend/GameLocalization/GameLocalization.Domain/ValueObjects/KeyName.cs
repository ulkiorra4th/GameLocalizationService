using GameLocalization.Common.Results;

namespace GameLocalization.Domain.ValueObjects;

public sealed class KeyName
{
    public string Value { get; }

    private KeyName(string value)
    {
        Value = value;
    }

    public static Result<KeyName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<KeyName>.ValidationFailure("Key name cannot be empty.");

        if (value.Length > 200)
            return Result<KeyName>.ValidationFailure("Key name cannot exceed 200 characters.");

        return Result<KeyName>.Success(new KeyName(value.Trim()));
    }

    public override bool Equals(object? obj) =>
        obj is KeyName other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
}