using System;
using System.Collections.Generic;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Users.UpdateUser;

internal class UpdateUserRequestBuilder : IBuilderForId, IBuilderForOptional
{
    private readonly UserProperty userProperties = new(new Dictionary<string, object>());
    private readonly List<ChannelMessenger> messengerChannels = new();
    private readonly List<ChannelMms> mmsChannels = new();
    private readonly List<ChannelPstn> pstnChannels = new();
    private readonly List<ChannelSip> sipChannels = new();
    private readonly List<ChannelSms> smsChannels = new();
    private readonly List<ChannelVbc> vbcChannels = new();
    private readonly List<ChannelViber> viberChannels = new();
    private readonly List<ChannelWebSocket> webSocketChannels = new();
    private readonly List<ChannelWhatsApp> whatsAppChannels = new();
    private Maybe<string> name;
    private Maybe<string> displayName;
    private Maybe<Uri> imageUrl;
    private string id;

    public Result<UpdateUserRequest> Create() => Result<UpdateUserRequest>.FromSuccess(new UpdateUserRequest
        {
            Id = this.id,
            Name = this.name,
            DisplayName = this.displayName,
            ImageUrl = this.imageUrl,
            Properties = this.userProperties,
            Channels = new UserChannels(this.pstnChannels, this.sipChannels, this.vbcChannels,
                this.webSocketChannels, this.smsChannels, this.mmsChannels,
                this.whatsAppChannels, this.viberChannels,
                this.messengerChannels),
        })
        .Map(InputEvaluation<UpdateUserRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(VerifyId, VerifyName, VerifyDisplayName));

    public IBuilderForOptional WithChannel(ChannelMessenger value)
    {
        this.messengerChannels.Add(value);
        return this;
    }

    public IBuilderForOptional WithChannel(ChannelPstn value)
    {
        this.pstnChannels.Add(value);
        return this;
    }

    public IBuilderForOptional WithChannel(ChannelSip value)
    {
        this.sipChannels.Add(value);
        return this;
    }

    public IBuilderForOptional WithChannel(ChannelSms value)
    {
        this.smsChannels.Add(value);
        return this;
    }

    public IBuilderForOptional WithChannel(ChannelMms value)
    {
        this.mmsChannels.Add(value);
        return this;
    }

    public IBuilderForOptional WithChannel(ChannelVbc value)
    {
        this.vbcChannels.Add(value);
        return this;
    }

    public IBuilderForOptional WithChannel(ChannelViber value)
    {
        this.viberChannels.Add(value);
        return this;
    }

    public IBuilderForOptional WithChannel(ChannelWebSocket value)
    {
        this.webSocketChannels.Add(value);
        return this;
    }

    public IBuilderForOptional WithChannel(ChannelWhatsApp value)
    {
        this.whatsAppChannels.Add(value);
        return this;
    }

    public IBuilderForOptional WithDisplayName(string value)
    {
        this.displayName = value;
        return this;
    }

    public IBuilderForOptional WithId(string value)
    {
        this.id = value;
        return this;
    }

    public IBuilderForOptional WithImageUrl(Uri value)
    {
        this.imageUrl = value;
        return this;
    }

    public IBuilderForOptional WithName(string value)
    {
        this.name = value;
        return this;
    }

    public IBuilderForOptional WithUserProperty(string key, object value)
    {
        this.userProperties.CustomData.Add(key, value);
        return this;
    }

    private static Result<UpdateUserRequest> VerifyDisplayName(UpdateUserRequest request) =>
        request.DisplayName.Match(
            value => InputValidation.VerifyNotEmpty(request, value, nameof(UpdateUserRequest.DisplayName)),
            () => request);

    private static Result<UpdateUserRequest> VerifyId(UpdateUserRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Id, nameof(request.Id));

    private static Result<UpdateUserRequest> VerifyName(UpdateUserRequest request) =>
        request.Name.Match(
            value => InputValidation.VerifyNotEmpty(request, value, nameof(UpdateUserRequest.Name)),
            () => request);
}

/// <summary>
///     Represents the initial builder state requiring the user ID to be specified before optional parameters.
/// </summary>
public interface IBuilderForId
{
    /// <summary>
    ///     Sets the unique identifier of the user to update.
    /// </summary>
    /// <param name="value">The user ID (e.g., "USR-12345678-1234-1234-1234-123456789012"). Must not be empty.</param>
    /// <returns>The builder instance for configuring optional parameters.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithId("USR-12345678-1234-1234-1234-123456789012")
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithId(string value);
}

/// <summary>
///     Represents a builder for configuring optional parameters when updating an existing user.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<UpdateUserRequest>
{
    /// <summary>
    ///     Adds a Facebook Messenger channel to the user.
    /// </summary>
    /// <param name="value">The Messenger channel configuration containing the user's Messenger ID.</param>
    /// <returns>The builder instance for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithChannel(new ChannelMessenger("messenger-user-id"))
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithChannel(ChannelMessenger value);

    /// <summary>
    ///     Adds a PSTN (landline/mobile) channel to the user for voice communication.
    /// </summary>
    /// <param name="value">The PSTN channel configuration containing the phone number.</param>
    /// <returns>The builder instance for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithChannel(new ChannelPstn(14155550100))
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithChannel(ChannelPstn value);

    /// <summary>
    ///     Adds a SIP channel to the user for VoIP communication.
    /// </summary>
    /// <param name="value">The SIP channel configuration containing the URI and authentication credentials.</param>
    /// <returns>The builder instance for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithChannel(new ChannelSip("sip:user@domain.com", "username", "password"))
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithChannel(ChannelSip value);

    /// <summary>
    ///     Adds an SMS channel to the user for text messaging.
    /// </summary>
    /// <param name="value">The SMS channel configuration containing the phone number.</param>
    /// <returns>The builder instance for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithChannel(new ChannelSms("14155550100"))
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithChannel(ChannelSms value);

    /// <summary>
    ///     Adds an MMS channel to the user for multimedia messaging.
    /// </summary>
    /// <param name="value">The MMS channel configuration containing the phone number.</param>
    /// <returns>The builder instance for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithChannel(new ChannelMms("14155550100"))
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithChannel(ChannelMms value);

    /// <summary>
    ///     Adds a VBC (Vonage Business Communications) channel to the user.
    /// </summary>
    /// <param name="value">The VBC channel configuration containing the extension number.</param>
    /// <returns>The builder instance for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithChannel(new ChannelVbc("1234"))
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithChannel(ChannelVbc value);

    /// <summary>
    ///     Adds a Viber channel to the user for messaging.
    /// </summary>
    /// <param name="value">The Viber channel configuration containing the phone number.</param>
    /// <returns>The builder instance for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithChannel(new ChannelViber("14155550100"))
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithChannel(ChannelViber value);

    /// <summary>
    ///     Adds a WebSocket channel to the user for real-time audio streaming.
    /// </summary>
    /// <param name="value">The WebSocket channel configuration containing the URI, content type, and optional headers.</param>
    /// <returns>The builder instance for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithChannel(new ChannelWebSocket("wss://example.com/socket", "audio/l16;rate=16000", new Dictionary<string, string>()))
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithChannel(ChannelWebSocket value);

    /// <summary>
    ///     Adds a WhatsApp channel to the user for messaging.
    /// </summary>
    /// <param name="value">The WhatsApp channel configuration containing the phone number.</param>
    /// <returns>The builder instance for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithChannel(new ChannelWhatsApp("14155550100"))
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithChannel(ChannelWhatsApp value);

    /// <summary>
    ///     Sets a human-readable display name for the user. Unlike the unique name, this does not need to be unique.
    /// </summary>
    /// <param name="value">The display name to show for the user. Must not be empty if provided.</param>
    /// <returns>The builder instance for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithDisplayName("John Doe")
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithDisplayName(string value);

    /// <summary>
    ///     Sets a profile image URL for the user.
    /// </summary>
    /// <param name="value">The URL pointing to the user's profile image or avatar.</param>
    /// <returns>The builder instance for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithImageUrl(new Uri("https://example.com/avatar.png"))
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithImageUrl(Uri value);

    /// <summary>
    ///     Sets the unique name for the user within the Vonage platform.
    /// </summary>
    /// <param name="value">The unique name to identify the user. Must not be empty if provided.</param>
    /// <returns>The builder instance for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithName("my-user")
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithName(string value);

    /// <summary>
    ///     Adds a custom property to the user for storing application-specific data.
    /// </summary>
    /// <param name="key">The property key.</param>
    /// <param name="value">The property value, which can be any serializable object.</param>
    /// <returns>The builder instance for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithUserProperty("department", "Engineering")
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithUserProperty(string key, object value);
}