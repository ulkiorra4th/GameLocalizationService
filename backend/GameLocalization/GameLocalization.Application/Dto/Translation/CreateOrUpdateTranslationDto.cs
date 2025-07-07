namespace GameLocalization.Application.Dto.Translation;

public sealed record CreateOrUpdateTranslationDto(Guid KeyId, Guid LanguageId, string Value);