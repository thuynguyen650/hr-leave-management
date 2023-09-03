using HR.LeaveManagement.Api.Models;
using HR.LeaveManagement.Application.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace HR.LeaveManagement.Api.Middleware;

public class ExceptionMiddleware
{
    // the next action along the pipeline
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex) {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        CustomProblemDetails problem = new();

        switch(ex)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomProblemDetails
                {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Detail = badRequestException.InnerException?.Message,
                    Type = nameof(BadRequestException),
                    Errors = badRequestException.ValidationErrors
                };
                break;
            case NotFoundException notFound:
                statusCode = HttpStatusCode.NotFound;
                problem = new CustomProblemDetails
                {
                    Title = notFound.Message,
                    Status = (int)statusCode,
                    Detail = notFound.InnerException?.Message,
                    Type = nameof(NotFoundException)
                };
                break;
            default:
                problem = new CustomProblemDetails
                {
                    Title = ex.Message,
                    Status = (int)statusCode,
                    Detail = ex.StackTrace
                };
                break;
        }

        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsJsonAsync(problem);
    }
}
