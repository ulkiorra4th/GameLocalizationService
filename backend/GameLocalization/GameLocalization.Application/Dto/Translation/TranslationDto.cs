namespace GameLocalization.Application.Dto.Translation;

public record TranslationDto(Guid KeyId, Guid LanguageId, string Value);