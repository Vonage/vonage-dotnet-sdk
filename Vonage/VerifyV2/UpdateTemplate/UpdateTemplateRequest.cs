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

/// <summary>
///     Represents a request to update a custom verification template's name or default status.
/// </summary>
public readonly struct UpdateTemplateRequest : IVonageRequest
{
    /// <summary>
    ///     The new reference name for the template. Must be 1-64 characters matching the pattern ^[A-Za-z0-9_-]+$ and unique within the account.
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> Name { get; internal init; }

    /// <summary>
    ///     The unique identifier (UUID) of the template to update.
    /// </summary>
    [JsonIgnore]
    public Guid TemplateId { get; internal init; }

    /// <summary>
    ///     Whether this template should be used as the default when no template_id is specified in verification requests.
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<bool>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<bool> IsDefault { get; internal init; }

    /// <summary>
    ///     Creates a new builder to construct an <see cref="UpdateTemplateRequest"/>.
    /// </summary>
    /// <returns>A builder interface to set the template ID.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = UpdateTemplateRequest.Build()
    ///     .WithId(Guid.Parse("8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9"))
    ///     .WithName("new-template-name")
    ///     .SetAsDefaultTemplate()
    ///     .Create();
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    public static IBuilderForId Build() => new UpdateTemplateRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(new HttpMethod("PATCH"), $"/v2/verify/templates/{this.TemplateId}")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");
}