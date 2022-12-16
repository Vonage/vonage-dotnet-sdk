namespace Vonage.Video.Beta.Video.Sessions.GetStream;

public struct ErrorResponse
{
    public ErrorResponse(string code, string message)
    {
        this.Code = code;
        this.Message = message;
    }

    public string Code { get; set; }

    public string Message { get; set; }
}