using GameLocalization.Common.Results;

namespace GameLocalization.Domain.ValueObjects;

public sealed class ProjectName
{
    public string Value { get; }

    private ProjectName(string value)
    {
        Value = value;
    }
    
    public static Result<ProjectName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<ProjectName>.ValidationFailure("Project name cannot be empty.");
        
        if (value.Length > 200) 
            return Result<ProjectName>.ValidationFailure("Project name cannot be exceed 200 characters.");
        
        return Result<ProjectName>.Success(new ProjectName(value));
    }

    public override bool Equals(object? obj) =>
        obj is ProjectName other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
}