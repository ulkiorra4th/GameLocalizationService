namespace GameLocalization.Application.Dto.Key;

public sealed record UpdateKeyDto(Guid ProjectId, Guid KeyId, string Name);