using GameLocalization.Common.Results;

namespace GameLocalization.Domain.Models;

public sealed class Language
{
    public Guid Id { get; }
    public string Code { get; }
    public string Name { get; }
    public bool IsCustom { get; }
    public Guid? ProjectId { get; }

    private Language(Guid id, string code, string name, bool isCustom, Guid? projectId = null)
    {
        Id = id;
        Code = code;
        Name = name;
        IsCustom = isCustom;
        ProjectId = projectId;
    }

    public static Result<Language> Create(Guid id, string code, string name, bool isCustom, Guid? projectId = null)
    {
        if (id == Guid.Empty)
            return Result<Language>.ValidationFailure("LanguageId cannot be empty.");

        var codeResult = ValidateCode(code);
        if (codeResult.IsFailure) return Result<Language>.ValidationFailure(codeResult.ErrorMessage!);

        var nameResult = ValidateName(name);
        if (nameResult.IsFailure) return Result<Language>.ValidationFailure(nameResult.ErrorMessage!);

        if (projectId is not null)
        {
            if (projectId == Guid.Empty) return Result<Language>.ValidationFailure("Project Id cannot be empty.");
            isCustom = true;
        }
        else if (isCustom)
        {
            return Result<Language>.ValidationFailure("Custom language requires Project Id.");
        }
        
        return Result<Language>.Success(new Language(id, codeResult.Value!, nameResult.Value!, isCustom, projectId));
    }

    private static Result<string> ValidateCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            return Result<string>.ValidationFailure("Language code cannot be empty or whitespace.");

        if (code.Length is < 2 or > 3) 
            return Result<string>.ValidationFailure("Language code must be 2 or 3 characters long.");

        return Result<string>.Success(code.ToLowerInvariant());
    }

    private static Result<string> ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<string>.ValidationFailure("Language name cannot be empty or whitespace.");

        if (name.Length > 100)
            return Result<string>.ValidationFailure("Language name cannot exceed 100 characters.");

        return Result<string>.Success(name.Trim());
    }

    public override bool Equals(object? obj) =>
        obj is Language other && Id == other.Id && Code == other.Code && Name == other.Name && IsCustom == other.IsCustom;

    public override int GetHashCode() => HashCode.Combine(Id, Code, Name, IsCustom);
}