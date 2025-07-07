using GameLocalization.Common.Enums;
using GameLocalization.Common.Results;
using GameLocalization.Dto.Common;
using Microsoft.AspNetCore.Mvc;

namespace GameLocalization.Extensions;

public static class ResultExtensions
{
    private const string Success = "success";
    private const string Error = "error";

    public static IActionResult ToActionResult<T>(this Result<T> result, string? message = null) =>
        result.IsSuccess
            ? new OkObjectResult(new Response<T>(Success, message, result.Value!))
            : result.Code switch
            {
                ErrorCode.Unknown => new ObjectResult(new Response(Error, result.ErrorMessage))
                    { StatusCode = StatusCodes.Status500InternalServerError },
                ErrorCode.Validation => new BadRequestObjectResult(new Response(Error, result.ErrorMessage)),
                ErrorCode.NotFound => new NotFoundObjectResult(new Response(Error, result.ErrorMessage)),
                ErrorCode.Conflict => new ConflictObjectResult(new Response(Error, result.ErrorMessage)),
                ErrorCode.Unauthorized => new UnauthorizedObjectResult(new Response(Error, result.ErrorMessage)),
                ErrorCode.Forbidden => new ForbidResult(),
                ErrorCode.BadRequest => new BadRequestObjectResult(new Response(Error, result.ErrorMessage)),
                _ => throw new ArgumentOutOfRangeException()
            };

    public static IActionResult ToActionResult(this Result result, string? message = null) =>
        result.IsSuccess
            ? new OkObjectResult(new Response(Success, message))
            : result.Code switch
            {
                ErrorCode.Unknown => new ObjectResult(new Response(Error, result.ErrorMessage))
                    { StatusCode = StatusCodes.Status500InternalServerError },
                ErrorCode.Validation => new BadRequestObjectResult(new Response(Error, result.ErrorMessage)),
                ErrorCode.NotFound => new NotFoundObjectResult(new Response(Error, result.ErrorMessage)),
                ErrorCode.Conflict => new ConflictObjectResult(new Response(Error, result.ErrorMessage)),
                ErrorCode.Unauthorized => new UnauthorizedObjectResult(new Response(Error, result.ErrorMessage)),
                ErrorCode.Forbidden => new ForbidResult(),
                ErrorCode.BadRequest => new BadRequestObjectResult(new Response(Error, result.ErrorMessage)),
                _ => throw new ArgumentOutOfRangeException()
            };
}