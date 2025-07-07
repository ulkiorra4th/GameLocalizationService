namespace GameLocalization.Application.Dto.Presentation;

public sealed record TableRowDto(KeyDto Key, List<TableCellDto> Translations);