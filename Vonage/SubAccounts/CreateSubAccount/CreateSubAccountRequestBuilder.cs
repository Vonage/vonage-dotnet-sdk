using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.SubAccounts.CreateSubAccount;

internal class CreateSubAccountRequestBuilder : IBuilderForName, IBuilderForOptional
{
    private const int NameMaxLength = 80;
    private bool useSharedBalance = true;
    private Maybe<string> secret;
    private string name;

    /// <inheritdoc />
    public Result<CreateSubAccountRequest> Create() =>
        Result<CreateSubAccountRequest>
            .FromSuccess(new CreateSubAccountRequest
            {
                Name = this.name,
                Secret = this.secret,
                UsePrimaryAccountBalance = this.useSharedBalance,
            })
            .Map(InputEvaluation<CreateSubAccountRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyName, VerifyNameLength));

    /// <inheritdoc />
    public IBuilderForOptional DisableSharedAccountBalance()
    {
        this.useSharedBalance = false;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithName(string value)
    {
        this.name = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithSecret(string value)
    {
        this.secret = value;
        return this;
    }

    private static Result<CreateSubAccountRequest> VerifyName(CreateSubAccountRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Name, nameof(request.Name));

    private static Result<CreateSubAccountRequest> VerifyNameLength(CreateSubAccountRequest request) =>
        InputValidation
            .VerifyLengthLowerOrEqualThan(request, request.Name, NameMaxLength, nameof(request.Name));
}

/// <summary>
///     Represents a builder for Name.
/// </summary>
public interface IBuilderForName
{
    /// <summary>
    ///     Sets the Name.
    /// </summary>
    /// <param name="value">The name.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithName(string value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<CreateSubAccountRequest>
{
    /// <summary>
    ///     Disables shared balance with primary account.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptional DisableSharedAccountBalance();

    /// <summary>
    ///     Sets the secret.
    /// </summary>
    /// <param name="value">The secret.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithSecret(string value);
}