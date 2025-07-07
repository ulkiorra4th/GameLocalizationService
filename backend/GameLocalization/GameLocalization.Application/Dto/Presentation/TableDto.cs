using GameLocalization.Common.Results;

namespace GameLocalization.Application.Dto.Presentation;

public sealed record TableDto(
    IEnumerable<LanguageDto> Languages,
    PaginatedResult<TableRowDto> Rows
);