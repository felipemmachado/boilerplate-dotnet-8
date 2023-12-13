using Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace API;
public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var erroResult = new ErrorDetails();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    erroResult.Message = string.IsNullOrEmpty(contextFeature.Error?.Message) ? "Erro ocorrido, mensagem não capturada." : contextFeature.Error.Message;



                    if (contextFeature.Error is ValidationException validator)
                    {
                        erroResult.Message = "Algumas validações falharam.";
                        erroResult.Errors = validator.Errors;
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }


                    if (contextFeature?.Error?.InnerException != null)
                    {
                        erroResult.Message += $"<br/><br/>{contextFeature.Error.InnerException.Message}";
                    }


                    await context.Response.WriteAsync(erroResult.ToString());
                }
            });
        });
    }
}

public class ErrorDetails
{
    public string? Message { get; set; }

    public List<ErrorModel>? Errors { get; set; }

    public override string ToString()
    {
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        return JsonSerializer.Serialize(this, serializeOptions);
    }
}

