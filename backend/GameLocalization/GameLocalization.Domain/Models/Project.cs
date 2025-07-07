using GameLocalization.Common.Results;
using GameLocalization.Domain.ValueObjects;

namespace GameLocalization.Domain.Models;

public sealed class Project
{
    private readonly List<Key> _keys;
    public Guid Id { get; private set; }
    public ProjectName Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyCollection<Key> Keys => _keys.AsReadOnly();

    private Project(Guid id, ProjectName name, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _keys = new List<Key>();
    }

    public static Result<Project> Create(string name, Guid? id = null, DateTime? createdAt = null, 
        DateTime? updatedAt = null)
    {
        var projectNameResult = ProjectName.Create(name);
        if (projectNameResult.IsFailure) return Result<Project>.ValidationFailure(projectNameResult.ErrorMessage!);
        if (id is not null && id == Guid.Empty) return Result<Project>.ValidationFailure("Project id cannot be empty.");
        
        var project = new Project(
            id: id ?? Guid.NewGuid(),
            name: projectNameResult.Value!,
            createdAt: createdAt ?? new DateTime(),
            updatedAt: updatedAt ?? new DateTime()
        );

        return Result<Project>.Success(project);
    }

    public Result UpdateName(string newName)
    {
        var projectNameResult = ProjectName.Create(newName);
        if (projectNameResult.IsFailure) return Result<Project>.ValidationFailure(projectNameResult.ErrorMessage!);
        
        Name = projectNameResult.Value!;
        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }

    public Result AddKey(Key key)
    {
        if (key.ProjectId != Id)
            return Result.Failure($"Key ProjectId {key.ProjectId} does not match Project Id {Id}.");

        if (_keys.Any(k => k.Name.Value == key.Name.Value))
            return Result.Failure($"Key with name {key.Name.Value} already exists in project.");

        _keys.Add(key);
        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }

    public Result RemoveKey(Guid keyId)
    {
        var key = _keys.FirstOrDefault(k => k.Id == keyId);
        if (key == null)
            return Result.NotFound($"Key with ID {keyId} not found in project.");

        _keys.Remove(key);
        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }
}