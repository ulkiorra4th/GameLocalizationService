namespace GameLocalization.Persistence.Postgres.Entities;

public sealed class LanguageEntity
{
    public Guid Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public bool IsCustom { get; set; }
    public Guid? ProjectId { get; set; }

    public ProjectEntity? ProjectCreated { get; set; }
    public ICollection<TranslationEntity> Translations { get; set; } = new List<TranslationEntity>();
    public List<ProjectEntity> UsingProjects { get; set; } = new();
}