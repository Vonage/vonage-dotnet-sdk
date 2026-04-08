#region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
#endregion

namespace Vonage.VerifyV2.StartVerification;

/// <summary>
///     Represents a request to initiate a verification by sending a PIN code to a user through one or more channels (SMS, Voice, Email, WhatsApp, or Silent Auth).
/// </summary>
public readonly struct StartVerificationRequest : IVonageRequest
{
    /// <summary>
    ///     The brand name that appears in the verification message. Limited to 16 characters. Cannot contain special characters: \ / { } : $.
    /// </summary>
    public string Brand { get; internal init; }

    /// <summary>
    ///     The wait time in seconds between attempts to deliver the verification code (between 15 and 900). Default is 300 seconds. On the Verify Success pricing model, minimum is fixed at 60 seconds.
    /// </summary>
    public int ChannelTimeout { get; internal init; }

    /// <summary>
    ///     An optional reference string (1-40 alphanumeric characters) that will be included in webhook callbacks for tracking purposes.
    /// </summary>
    public Maybe<string> ClientReference { get; internal init; }

    /// <summary>
    ///     An optional custom alphanumeric PIN code (4-10 characters) to use instead of an auto-generated code. Only available on the Verify Conversion pricing model and has no effect on Silent Auth.
    /// </summary>
    public Maybe<string> Code { get; internal init; }

    /// <summary>
    ///     The length of the PIN code to generate (4-10 digits). Default is 4. Applies to all channels in the workflow.
    /// </summary>
    public int CodeLength { get; internal init; }

    /// <summary>
    ///     When true (default), the request will perform fraud checking. Set to false to bypass network blocks for testing or special cases.
    /// </summary>
    public bool FraudCheck { get; internal init; }

    /// <summary>
    ///     The language locale for verification messages. Default is "en-us". Has no effect on Silent Auth channels.
    /// </summary>
    public Locale Locale { get; internal init; }

    /// <summary>
    ///     The sequence of verification workflows (1-3) that define how to reach the user. The first workflow is tried first; subsequent workflows are used as fallbacks.
    /// </summary>
    public IVerificationWorkflow[] Workflows { get; internal init; }

    /// <summary>
    ///     An optional custom template ID to use for SMS or Voice channel messages instead of the default template.
    /// </summary>
    public Maybe<Guid> TemplateId { get; internal init; }

    /// <summary>
    ///     Creates a new builder to construct a <see cref="StartVerificationRequest"/>.
    /// </summary>
    /// <returns>A builder interface to set the brand name.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = StartVerificationRequest.Build()
    ///     .WithBrand("MyApp")
    ///     .WithWorkflow(SmsWorkflow.Parse("447700900000"))
    ///     .WithFallbackWorkflow(VoiceWorkflow.Parse("447700900000"))
    ///     .WithLocale("en-us")
    ///     .WithCodeLength(6)
    ///     .Create();
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    public static IBuilderForBrand Build() => new StartVerificationRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, "/v2/verify")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent()
    {
        var serializer = JsonSerializerBuilder.BuildWithSnakeCase();
        var values = new Dictionary<string, object>();
        values.Add("locale", this.Locale);
        values.Add("channel_timeout", this.ChannelTimeout);
        this.ClientReference.IfSome(some => values.Add("client_ref", some));
        values.Add("code_length", this.CodeLength);
        values.Add("brand", this.Brand);
        this.Code.IfSome(some => values.Add("code", some));
        values.Add("workflow", this.Workflows
            .Select(workflow => workflow.Serialize(serializer))
            .Select(serializedString => serializer.DeserializeObject<dynamic>(serializedString))
            .Select(result => result.IfFailure(default)));
        if (!this.FraudCheck)
        {
            values.Add("fraud_check", false);
        }

        this.TemplateId.IfSome(some => values.Add("template_id", some));
        return new StringContent(serializer.SerializeObject(values), Encoding.UTF8, "application/json");
    }
}