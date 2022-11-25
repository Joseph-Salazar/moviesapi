using Microsoft.AspNetCore.Http;

namespace Infrastructure.CrossCutting.CustomExections;

public class WarningException : Exception
{
    public WarningException(string message, int httpStatusCode = StatusCodes.Status200OK)
        : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }

    public WarningException(string message, Exception innerException,
        int httpStatusCode = StatusCodes.Status200OK)
        : base(message, innerException)
    {
        HttpStatusCode = httpStatusCode;
    }

    public int HttpStatusCode { get; }
}