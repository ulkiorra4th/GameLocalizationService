namespace GameLocalization.Persistence.Postgres.Entities;

public sealed class LanguageInTableEntity
{
    public Guid? ProjectId { get; set; }
    public Guid LanguageId { get; set; }

    public ProjectEntity Project { get; set; } = null!;
    public LanguageEntity Language { get; set; } = null!;

}