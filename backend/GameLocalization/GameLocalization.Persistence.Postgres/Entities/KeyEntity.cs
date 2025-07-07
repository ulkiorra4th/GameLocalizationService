namespace GameLocalization.Persistence.Postgres.Entities;

public sealed class KeyEntity
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public ProjectEntity? Project { get; set; }
    public ICollection<TranslationEntity?> Translations { get; set; } = new List<TranslationEntity?>();
}