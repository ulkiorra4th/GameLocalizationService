namespace GameLocalization.Persistence.Postgres.Entities;

public sealed class ProjectEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<KeyEntity?> Keys { get; set; } = new List<KeyEntity?>();

    public ICollection<LanguageEntity?> CustomLanguages { get; set; } = new List<LanguageEntity?>();
    public List<LanguageEntity> LanguagesInTable { get; set; } = new();
}