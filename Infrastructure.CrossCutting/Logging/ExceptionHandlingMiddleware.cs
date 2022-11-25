using Infrastructure.CrossCutting.CustomExections;
using Infrastructure.CrossCutting.Wrapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace Infrastructure.CrossCutting.Logging;

public class ExceptionHandlingMiddleware
{
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
        _logger = Log.Logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (WarningException ex)
        {
            var completeMessage = GetCompleteExceptionMessage(ex);
            _logger.Warning($"{completeMessage}\n{ex.StackTrace}");

            await HandleExceptionAsync(context, new JsonResult<string>
            {
                Message = ex.Message,
                Warning = true
            }, ex.HttpStatusCode);
        }
        catch (ControlledException ex)
        {
            var completeMessage = GetCompleteExceptionMessage(ex);
            _logger.Error($"{completeMessage}\n{ex.StackTrace}");

            await HandleExceptionAsync(context, ex.Message, ex.HttpStatusCode);
        }
        catch (Exception ex)
        {
            var completeMessage = GetCompleteExceptionMessage(ex);
            _logger.Error($"{completeMessage}\n{ex.StackTrace}");

            if (bool.Parse(_configuration.GetSection("ExceptionSettings:ShowCustomMessage").Value))
                completeMessage = _configuration.GetSection("ExceptionSettings:CustomMessage").Value;

            await HandleExceptionAsync(context, completeMessage, StatusCodes.Status500InternalServerError);
        }
    }

    private string GetCompleteExceptionMessage(Exception ex)
    {
        if (ex.InnerException == null)
            return ex.Message;
        var errorMessage = $"{ex.Message}\n{GetCompleteExceptionMessage(ex.InnerException)}";

        return errorMessage;
    }

    private Task HandleExceptionAsync(HttpContext context, string error, int httpStatusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = httpStatusCode;

        return context.Response.WriteAsync(JsonConvert.SerializeObject(new JsonResult<string> {Message = error},
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
    }

    private Task HandleExceptionAsync(HttpContext context, JsonResult<string> data, int httpStatusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = httpStatusCode;

        return context.Response.WriteAsync(JsonConvert.SerializeObject(data,
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
    }
}