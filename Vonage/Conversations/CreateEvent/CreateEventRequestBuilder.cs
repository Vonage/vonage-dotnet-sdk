using System.Text.Json;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Conversations.CreateEvent;

internal struct CreateEventRequestBuilder : IBuilderForType, IBuilderForBody, IBuilderForOptional,
    IBuilderForConversationId
{
    private string conversationId = null;
    private string type = null;
    private Maybe<string> from = Maybe<string>.None;
    private JsonElement body = new JsonElement();

    public CreateEventRequestBuilder()
    {
    }

    public IBuilderForBody WithType(string value) => this with {type = value};

    public IBuilderForOptional WithBody(object value) => this with {body = JsonSerializer.SerializeToElement(value)};

    public Result<CreateEventRequest> Create() => Result<CreateEventRequest>.FromSuccess(new CreateEventRequest
        {
            Body = this.body,
            ConversationId = this.conversationId,
            From = this.from,
            Type = this.type,
        })
        .Map(InputEvaluation<CreateEventRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(VerifyConversationId, VerifyType));

    public IVonageRequestBuilder<CreateEventRequest> WithFrom(string value) => this with {from = value};

    public IBuilderForType WithConversationId(string value) => this with {conversationId = value};

    private static Result<CreateEventRequest> VerifyConversationId(CreateEventRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConversationId, nameof(request.ConversationId));

    private static Result<CreateEventRequest> VerifyType(CreateEventRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Type, nameof(request.Type));
}

/// <summary>
///     Represents a builder for ConversationId.
/// </summary>
public interface IBuilderForConversationId
{
    /// <summary>
    ///     Sets the ConversationId on the builder.
    /// </summary>
    /// <param name="value">The conversation Id.</param>
    /// <returns>The builder.</returns>
    IBuilderForType WithConversationId(string value);
}

/// <summary>
///     Represents a builder for Type.
/// </summary>
public interface IBuilderForType
{
    /// <summary>
    ///     Sets the event type on the builder.
    /// </summary>
    /// <param name="value">The type.</param>
    /// <returns>The builder.</returns>
    IBuilderForBody WithType(string value);
}

/// <summary>
///     Represents a builder for Body.
/// </summary>
public interface IBuilderForBody
{
    /// <summary>
    ///     Sets the event body on the builder.
    /// </summary>
    /// <param name="value">The body.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithBody(object value);
}

/// <summary>
///     Represents a builder for From.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<CreateEventRequest>
{
    /// <summary>
    ///     Sets the event from on the builder.
    /// </summary>
    /// <param name="value">The from.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<CreateEventRequest> WithFrom(string value);
}