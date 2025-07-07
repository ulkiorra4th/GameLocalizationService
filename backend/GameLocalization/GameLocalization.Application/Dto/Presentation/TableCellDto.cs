namespace GameLocalization.Application.Dto.Presentation;

public sealed record TableCellDto(Guid KeyId, Guid LanguageId, string? Value);