using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Vonage.Request;

internal class ThrottlingMessageHandler : DelegatingHandler
{
    private readonly TimeSpanSemaphore execTimeSpanSemaphore;

    public ThrottlingMessageHandler(TimeSpanSemaphore execTimeSpanSemaphore)
        : this(execTimeSpanSemaphore, new HttpClientHandler {AllowAutoRedirect = true})
    {
    }

    public ThrottlingMessageHandler(TimeSpanSemaphore execTimeSpanSemaphore, HttpMessageHandler innerHandler)
        : base(innerHandler) =>
        this.execTimeSpanSemaphore = execTimeSpanSemaphore;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken) =>
        this.execTimeSpanSemaphore?.RunAsync(base.SendAsync, request, cancellationToken) ??
        base.SendAsync(request, cancellationToken);
}