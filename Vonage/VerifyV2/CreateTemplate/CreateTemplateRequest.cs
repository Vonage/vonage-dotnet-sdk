#region
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Serialization;
#endregion

namespace Vonage.VerifyV2.CreateTemplate;

/// <inheritdoc />
public readonly struct CreateTemplateRequest : IVonageRequest
{
    /// <summary>
    ///     Reference name for template.
    /// </summary>
    public string Name { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/v2/verify/templates";

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns></returns>
    public static IBuildForName Build() => new CreateTemplateRequestBuilder();
}