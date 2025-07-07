namespace GameLocalization.Application.Dto.Language;

public sealed record CreateLanguageDto(Guid ProjectId, string Code, string Name);