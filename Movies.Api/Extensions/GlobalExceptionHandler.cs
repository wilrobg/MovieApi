using Microsoft.AspNetCore.Diagnostics;
using Movies.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace Movies.Api.Extensions;

public static class GlobalExceptionHandler
{
    private class MessageError
    {
        public string Message { get; set; }
    }

    private class MessageErrorStack : MessageError
    {
        public string StackTrace { get; set; }
    }

    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app, bool isDevelopmentEnvironment)
    {
        _ = app.UseExceptionHandler(appError => appError.Run(async context =>
        {
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

            if (contextFeature != null)
            {

                // Set the Http Status Code
                var statusCode = contextFeature.Error switch
                {
                    MoviesException ex => ex.HttpStatusCode,
                    _ => HttpStatusCode.InternalServerError
                };

                // Prepare Generic Error
                object messageError;
                if (isDevelopmentEnvironment)
                    messageError = new MessageErrorStack { Message = contextFeature.Error.Message, StackTrace = contextFeature.Error.StackTrace };
                else
                    messageError = new MessageError { Message = contextFeature.Error.Message };

                // Set Response Details
                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";

                var r = messageError.GetType;

                // Return the Serialized Generic Error
                await context.Response.WriteAsync(JsonSerializer.Serialize(messageError));
            }
        }));

        return app;
    }
}
