namespace GameLocalization.Dto.Common;

public record Response(string Status, string? Message);

public sealed record Response<T>(string Status, string? Message, T Data) : Response(Status, Message);