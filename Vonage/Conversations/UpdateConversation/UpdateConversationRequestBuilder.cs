﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Conversations.UpdateConversation;

internal class UpdateConversationRequestBuilder : IBuilderForConversationId, IBuilderForOptional
{
    private const int CallbackEventMask = 200;
    private const int DisplayNameMaxLength = 50;
    private const int NameMaxLength = 100;
    private const int PropertiesCustomSortKeyMaxLength = 200;
    private const int PropertiesTypeMaxLength = 200;

    private readonly IEnumerable<HttpMethod> allowedMethods = new[] {HttpMethod.Get, HttpMethod.Post};
    private readonly List<INumber> numbers = new();
    private Maybe<Callback> callback;
    private Maybe<Properties> properties;
    private Maybe<string> name;
    private Maybe<string> displayName;
    private Maybe<Uri> uri;
    private string conversationId;

    /// <inheritdoc />
    public Result<UpdateConversationRequest> Create() => Result<UpdateConversationRequest>.FromSuccess(
            new UpdateConversationRequest
            {
                ConversationId = this.conversationId,
                Name = this.name,
                DisplayName = this.displayName,
                ImageUrl = this.uri,
                Properties = this.properties,
                Callback = this.callback,
                Numbers = this.numbers.Any() ? this.numbers : Maybe<IEnumerable<INumber>>.None,
            })
        .Map(InputEvaluation<UpdateConversationRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(
            VerifyConversationId,
            VerifyName,
            VerifyNameLength,
            VerifyDisplayName,
            VerifyDisplayNameLength,
            this.VerifyCallbackHttpMethod,
            VerifyPropertiesTypeLength,
            VerifyPropertiesCustomSortKeyLength,
            VerifyCallbackEventMaskLength));

    /// <inheritdoc />
    public IBuilderForOptional WithCallback(Callback value)
    {
        this.callback = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithConversationId(string value)
    {
        this.conversationId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithDisplayName(string value)
    {
        this.displayName = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithImageUrl(Uri value)
    {
        this.uri = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithName(string value)
    {
        this.name = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithNumber(INumber value)
    {
        this.numbers.Add(value);
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithProperties(Properties value)
    {
        this.properties = value;
        return this;
    }

    private static Result<UpdateConversationRequest> VerifyCallbackEventMaskLength(UpdateConversationRequest request) =>
        request.Callback.Match(
            some => InputValidation.VerifyLengthLowerOrEqualThan(request, some.EventMask, CallbackEventMask,
                $"{nameof(request.Callback)} {nameof(some.EventMask)}"),
            () => request);

    private Result<UpdateConversationRequest> VerifyCallbackHttpMethod(UpdateConversationRequest request) =>
        request.Callback.Match(
            some => this.allowedMethods.Contains(some.Method)
                ? Result<UpdateConversationRequest>.FromSuccess(request)
                : ResultFailure.FromErrorMessage("Callback HttpMethod must be GET or POST.")
                    .ToResult<UpdateConversationRequest>(),
            () => request);

    private static Result<UpdateConversationRequest> VerifyConversationId(UpdateConversationRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConversationId, nameof(request.ConversationId));

    private static Result<UpdateConversationRequest> VerifyDisplayName(UpdateConversationRequest request) =>
        request.DisplayName.Match(
            some => InputValidation.VerifyNotEmpty(request, some, nameof(request.DisplayName)),
            () => request);

    private static Result<UpdateConversationRequest> VerifyDisplayNameLength(UpdateConversationRequest request) =>
        request.DisplayName.Match(
            some => InputValidation.VerifyLengthLowerOrEqualThan(request, some, DisplayNameMaxLength,
                nameof(request.DisplayName)),
            () => request);

    private static Result<UpdateConversationRequest> VerifyName(UpdateConversationRequest request) =>
        request.Name.Match(
            some => InputValidation.VerifyNotEmpty(request, some, nameof(request.Name)),
            () => request);

    private static Result<UpdateConversationRequest> VerifyNameLength(UpdateConversationRequest request) =>
        request.Name.Match(
            some => InputValidation.VerifyLengthLowerOrEqualThan(request, some, NameMaxLength, nameof(request.Name)),
            () => request);

    private static Result<UpdateConversationRequest> VerifyPropertiesCustomSortKeyLength(
        UpdateConversationRequest request) =>
        request.Properties.Match(
            some => InputValidation.VerifyLengthLowerOrEqualThan(request, some.CustomSortKey,
                PropertiesCustomSortKeyMaxLength,
                $"{nameof(request.Properties)} {nameof(some.CustomSortKey)}"),
            () => request);

    private static Result<UpdateConversationRequest> VerifyPropertiesTypeLength(UpdateConversationRequest request) =>
        request.Properties.Match(
            some => InputValidation.VerifyLengthLowerOrEqualThan(request, some.Type, PropertiesTypeMaxLength,
                $"{nameof(request.Properties)} {nameof(some.Type)}"),
            () => request);
}

/// <summary>
///     Represents a builder for the ConversationId.
/// </summary>
public interface IBuilderForConversationId
{
    /// <summary>
    ///     Sets the Conversation Id.
    /// </summary>
    /// <param name="value">The conversation id.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithConversationId(string value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<UpdateConversationRequest>
{
    /// <summary>
    ///     Sets the Callback.
    /// </summary>
    /// <param name="value">The callback.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithCallback(Callback value);

    /// <summary>
    ///     Sets the Display Name.
    /// </summary>
    /// <param name="value">The display name.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithDisplayName(string value);

    /// <summary>
    ///     Sets the Image Url.
    /// </summary>
    /// <param name="value">The Image Url.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithImageUrl(Uri value);

    /// <summary>
    ///     Sets the Name
    /// </summary>
    /// <param name="value">The name.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithName(string value);

    /// <summary>
    ///     Set a Number to the conversation.
    /// </summary>
    /// <param name="value">The number.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithNumber(INumber value);

    /// <summary>
    ///     Sets the Properties.
    /// </summary>
    /// <param name="value">The properties.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithProperties(Properties value);
}