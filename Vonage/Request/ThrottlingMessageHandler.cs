using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Vonage.Request;

internal class ThrottlingMessageHandler(TimeSpanSemaphore execTimeSpanSemaphore, HttpMessageHandler innerHandler)
    : DelegatingHandler(innerHandler)
{
    public ThrottlingMessageHandler(TimeSpanSemaphore execTimeSpanSemaphore)
        : this(execTimeSpanSemaphore, new HttpClientHandler {AllowAutoRedirect = false})
    {
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken) =>
        execTimeSpanSemaphore?.RunAsync(base.SendAsync, request, cancellationToken) ??
        base.SendAsync(request, cancellationToken);
}