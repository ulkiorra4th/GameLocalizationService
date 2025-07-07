using GameLocalization.Common.Enums;

namespace GameLocalization.Common.Results;

public class Result
{
    public bool IsSuccess { get; protected set; }
    public string? ErrorMessage { get; protected set; }
    public ErrorCode Code { get; protected set; } = ErrorCode.Unknown;

    public bool IsFailure => !IsSuccess;

    protected Result() { }

    public static Result Success() => new()
    {
        IsSuccess = true
    };
    
    public static Result Failure(string message, ErrorCode code = ErrorCode.Unknown) => new()
    {
        IsSuccess = false,
        ErrorMessage = message,
        Code = code
    };

    public static Result ValidationFailure(string message = "Validation failed.") => new()
    {
        IsSuccess = false,
        ErrorMessage = message,
        Code = ErrorCode.Validation,
    };

    public static Result NotFound(string message = "Resource not found") => Failure(message, ErrorCode.NotFound);

    public static Result Unauthorized(string message = "Unauthorized") => Failure(message, ErrorCode.Unauthorized);
}

public sealed class Result<T> : Result
{
    public T? Value { get; private set; }
    
    private Result() { }

    public static Result<T> Success(T value) => new()
    {
        IsSuccess = true,
        Value = value
    };
    
    public new static Result<T> Failure(string message, ErrorCode code = ErrorCode.Unknown) => new() 
    {
        IsSuccess = false,
        ErrorMessage = message,
        Code = code
    };

    public new static Result<T> ValidationFailure(string message = "Validation failed.") => new()
    {
        IsSuccess = false,
        ErrorMessage = message,
        Code = ErrorCode.Validation,
    };
    
    public new static Result<T> NotFound(string message = "Resource not found") => Failure(message, ErrorCode.NotFound);

    public new static Result<T> Unauthorized(string message = "Unauthorized") => Failure(message, ErrorCode.Unauthorized);
}