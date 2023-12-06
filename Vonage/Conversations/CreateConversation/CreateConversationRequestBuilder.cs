using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Conversations.CreateConversation;

internal class CreateConversationRequestBuilder : IBuilderForOptional
{
    private const int DisplayNameMaxLength = 50;
    private const int NameMaxLength = 100;
    private Maybe<string> name;
    private Maybe<string> displayName;

    public Result<CreateConversationRequest> Create() => Result<CreateConversationRequest>.FromSuccess(
            new CreateConversationRequest
            {
                Name = this.name,
                DisplayName = this.displayName,
            })
        .Map(InputEvaluation<CreateConversationRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(
            VerifyName,
            VerifyNameLength,
            VerifyDisplayName,
            VerifyDisplayNameLength));

    public IBuilderForOptional WithDisplayName(string value)
    {
        this.displayName = value;
        return this;
    }

    public IBuilderForOptional WithName(string value)
    {
        this.name = value;
        return this;
    }

    private static Result<CreateConversationRequest> VerifyDisplayName(CreateConversationRequest request) =>
        request.DisplayName.Match(
            some => InputValidation.VerifyNotEmpty(request, some, nameof(request.DisplayName)),
            () => request);

    private static Result<CreateConversationRequest> VerifyDisplayNameLength(CreateConversationRequest request) =>
        request.DisplayName.Match(
            some => InputValidation.VerifyLengthLowerOrEqualThan(request, some, DisplayNameMaxLength,
                nameof(request.DisplayName)),
            () => request);

    private static Result<CreateConversationRequest> VerifyName(CreateConversationRequest request) =>
        request.Name.Match(
            some => InputValidation.VerifyNotEmpty(request, some, nameof(request.Name)),
            () => request);

    private static Result<CreateConversationRequest> VerifyNameLength(CreateConversationRequest request) =>
        request.Name.Match(
            some => InputValidation.VerifyLengthLowerOrEqualThan(request, some, NameMaxLength, nameof(request.Name)),
            () => request);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<CreateConversationRequest>
{
    /// <summary>
    ///     Sets the Display Name.
    /// </summary>
    /// <param name="value">The display name.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithDisplayName(string value);

    /// <summary>
    ///     Sets the Name
    /// </summary>
    /// <param name="value">The name.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithName(string value);
}