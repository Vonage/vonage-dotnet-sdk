using System;
using System.Collections.Generic;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Users.CreateUser;

internal class CreateUserRequestBuilder : IBuilderForOptional
{
    private readonly Dictionary<string, object> userProperties = new();
    private readonly List<ChannelPstn> pstnChannels = new();
    private readonly List<ChannelSip> sipChannels = new();
    private Maybe<string> name;
    private Maybe<string> displayName;
    private Maybe<Uri> imageUrl;
    private readonly List<ChannelVbc> vbcChannels = new();
    private readonly List<ChannelWebSocket> webSocketChannels = new();
    private readonly List<ChannelSms> smsChannels = new();
    private readonly List<ChannelMms> mmsChannels = new();
    private readonly List<ChannelWhatsApp> whatsAppChannels = new();
    private readonly List<ChannelViber> viberChannels = new();
    private readonly List<ChannelMessenger> messengerChannels = new();

    public Result<CreateUserRequest> Create() => Result<CreateUserRequest>.FromSuccess(new CreateUserRequest
        {
            Name = this.name,
            DisplayName = this.displayName,
            ImageUrl = this.imageUrl,
            Properties = this.userProperties,
            Channels = new UserChannels(this.pstnChannels, this.sipChannels, this.vbcChannels,
                this.webSocketChannels, this.smsChannels, this.mmsChannels,
                this.whatsAppChannels, this.viberChannels,
                this.messengerChannels),
        })
        .Map(InputEvaluation<CreateUserRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(VerifyName, VerifyDisplayName));

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
        this.userProperties.Add(key, value);
        return this;
    }

    private static Result<CreateUserRequest> VerifyDisplayName(CreateUserRequest request) =>
        request.DisplayName.Match(
            value => InputValidation.VerifyNotEmpty(request, value, nameof(CreateUserRequest.DisplayName)),
            () => request);

    private static Result<CreateUserRequest> VerifyName(CreateUserRequest request) =>
        request.Name.Match(
            value => InputValidation.VerifyNotEmpty(request, value, nameof(CreateUserRequest.Name)),
            () => request);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<CreateUserRequest>
{
    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    IBuilderForOptional WithChannel(ChannelMessenger value);

    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    IBuilderForOptional WithChannel(ChannelPstn value);

    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    IBuilderForOptional WithChannel(ChannelSip value);

    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    IBuilderForOptional WithChannel(ChannelSms value);

    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    IBuilderForOptional WithChannel(ChannelMms value);

    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    IBuilderForOptional WithChannel(ChannelVbc value);

    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    IBuilderForOptional WithChannel(ChannelViber value);

    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    IBuilderForOptional WithChannel(ChannelWebSocket value);

    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    IBuilderForOptional WithChannel(ChannelWhatsApp value);

    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    IBuilderForOptional WithDisplayName(string value);

    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    IBuilderForOptional WithImageUrl(Uri value);

    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    IBuilderForOptional WithName(string value);

    /// <summary>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    IBuilderForOptional WithUserProperty(string key, object value);
}