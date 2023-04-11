using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Yoh.Text.Json.NamingPolicies;
using JsonSerializer = Vonage.Common.JsonSerializer;

namespace Vonage.VerifyV2.StartVerification;

/// <inheritdoc />
public readonly struct StartVerificationRequest : IStartVerificationRequest

{
    /// <summary>
    ///     Gets the brand that is sending the verification request.
    /// </summary>
    public string Brand { get; internal init; }

    /// <summary>
    ///     Gets the wait time in seconds between attempts to delivery the verification code.
    /// </summary>
    public int ChannelTimeout { get; internal init; }

    /// <summary>
    ///     Gets the client reference.
    /// </summary>
    public Maybe<string> ClientReference { get; internal init; }

    /// <summary>
    ///     Gets the length of the code to send to the user
    /// </summary>
    public int CodeLength { get; internal init; }

    /// <summary>
    ///     Gets the request language.
    /// </summary>
    public Locale Locale { get; internal init; }

    /// <summary>
    ///     Gets verification workflows.
    /// </summary>
    public IVerificationWorkflow[] Workflows { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/v2/verify";

    private StringContent GetRequestContent()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicies.SnakeCaseLower,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters =
            {
                new MaybeJsonConverter<string>(),
                new PhoneNumberJsonConverter(),
                new EmailJsonConverter(),
                new LocaleJsonConverter(),
            },
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        };
        var serializer = new JsonSerializer(options);
        var values = new Dictionary<string, object>();
        values.Add("locale", this.Locale);
        values.Add("channel_timeout", this.ChannelTimeout);
        this.ClientReference.IfSome(some => values.Add("client_ref", some));
        values.Add("code_length", this.CodeLength);
        values.Add("brand", this.Brand);
        values.Add("workflow", this.Workflows
            .Select(workflow => workflow.Serialize(serializer))
            .Select(serializedString => serializer.DeserializeObject<dynamic>(serializedString))
            .Select(result => result.IfFailure(default)));
        return new StringContent(serializer.SerializeObject(values), Encoding.UTF8, "application/json");
    }
}