#region
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Serialization;
#endregion

namespace Vonage.VerifyV2.UpdateTemplate;

/// <inheritdoc />
public readonly struct UpdateTemplateRequest : IVonageRequest
{
    /// <summary>
    ///     Reference name for template.
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> Name { get; internal init; }

    /// <summary>
    ///     ID of the template.
    /// </summary>
    [JsonIgnore]
    public Guid TemplateId { get; internal init; }

    /// <summary>
    ///     Whether the template is the default template for that locale/channel combination.
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<bool>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<bool> IsDefault { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(new HttpMethod("PATCH"), this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/verify/templates/{this.TemplateId}";

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns></returns>
    public static IBuilderForId Build() => new UpdateTemplateRequestBuilder();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");
}