#region
using System.Net;
using Vonage.Common.Failures;
#endregion

namespace Vonage.Common;

internal interface IApiError
{
    HttpFailure ToFailure();
}

internal record VideoApiError(HttpStatusCode Code, string Message) : IApiError
{
    public HttpFailure ToFailure() => HttpFailure.From(this.Code, this.Message, null);
}

internal record StandardApiError(string Type, string Title, string Detail, string Instance) : IApiError
{
    public HttpFailure ToFailure() => HttpFailure.From(HttpStatusCode.Accepted, this.Title, null);
}

internal record NetworkApiError(int Status, string Code, string Message) : IApiError
{
    public HttpFailure ToFailure() => HttpFailure.From(HttpStatusCode.Accepted, this.Message, null);
}