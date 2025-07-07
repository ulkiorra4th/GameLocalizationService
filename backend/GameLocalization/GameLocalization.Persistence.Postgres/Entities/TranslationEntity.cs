namespace GameLocalization.Persistence.Postgres.Entities;

public sealed class TranslationEntity
{
    public Guid Id { get; set; }
    public Guid KeyId { get; set; }
    public Guid LanguageId { get; set; }
    public string Value { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public KeyEntity Key { get; set; } = null!;
    public LanguageEntity? Language { get; set; }
}