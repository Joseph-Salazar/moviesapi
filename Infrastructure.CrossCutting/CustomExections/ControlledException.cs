using Microsoft.AspNetCore.Http;

namespace Infrastructure.CrossCutting.CustomExections;

public class ControlledException : Exception
{
    public ControlledException(string message, int httpStatusCode = StatusCodes.Status400BadRequest)
        : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }

    public ControlledException(string message, Exception innerException,
        int httpStatusCode = StatusCodes.Status400BadRequest)
        : base(message, innerException)
    {
        HttpStatusCode = httpStatusCode;
    }

    public int HttpStatusCode { get; }
}