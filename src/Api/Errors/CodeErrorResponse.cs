using Newtonsoft.Json;

namespace Ecommerce.Api.Errors;

public class CodeErrorResponse
{
    [JsonProperty("code")]
    public int StatusCode { get; set; }

    [JsonProperty("message")]
    public string[]? Message { get; set; }

    public CodeErrorResponse(int statusCode, string[]? message = null)
    {
        StatusCode = statusCode;

        message ??= new string[0];
        var text = GetDefaultMessageForStatusCode(statusCode);
        message[0] = text ?? message[0];

        if (message != null)
            Message = message;
    }

    private string? GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "A bad request, you have made",
            401 => "Authorized, you are not",
            404 => "Resource found, it was not",
            500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leads to career change",
            _ => null
        };
    }
}
