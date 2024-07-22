
using System.Net;
using ARCN.API.ErrorHandlers;
using ValidationException = ARCN.Application.Exceptions.ValidationException;

namespace Microsoft.AspNetCore.Diagnostics
{

    public static class GlobalExceptionHandler
    {
        public static void UseGlobalException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exception = context.Features.Get<IExceptionHandlerFeature>();
                    if (exception != null)
                    {

                        var problemDetails = new ARCNProblemDetails();
                        if (exception.Error.GetType() == typeof(ValidationException))
                        {
                            ValidationException err = exception.Error as ValidationException;
                            if (err != null)
                            {
                                problemDetails.Errors = err.Errors;
                                problemDetails.Detail = "one or more validation error occurred";
                                problemDetails.Status = 400;
                                problemDetails.Type = err.HelpLink;
                                problemDetails.Title = "Validation Error";
                                problemDetails.Instance = context.Request.Path;//err.Instance ?? "100";
                                problemDetails.TraceIdentifier = context.TraceIdentifier;
                            }
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            problemDetails.Detail = "internal server error";
                            problemDetails.Status = 500;
                            problemDetails.Type = "";
                            problemDetails.Title = "internal server error.";
                            problemDetails.TraceIdentifier = context.TraceIdentifier;
                            problemDetails.Path = context.Request.Path;
                        }

                        await context.Response.WriteAsJsonAsync(problemDetails);
                    }

                });
            });
        }
    }
}