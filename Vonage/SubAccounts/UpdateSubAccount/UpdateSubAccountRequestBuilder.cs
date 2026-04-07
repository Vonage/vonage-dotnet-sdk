using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.SubAccounts.UpdateSubAccount;

internal class UpdateSubAccountRequestBuilder : IBuilderForOptional, IBuilderForSubAccountKey
{
    private const int NameMaxLength = 80;
    private Maybe<bool> useSharedBalance;
    private Maybe<bool> isSuspended;
    private Maybe<string> name;
    private string subAccountKey;

    /// <inheritdoc />
    public Result<UpdateSubAccountRequest> Create() =>
        Result<UpdateSubAccountRequest>
            .FromSuccess(new UpdateSubAccountRequest
            {
                SubAccountKey = this.subAccountKey,
                Name = this.name,
                UsePrimaryAccountBalance = this.useSharedBalance,
                Suspended = this.isSuspended,
            })
            .Map(InputEvaluation<UpdateSubAccountRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(
                VerifySubAccountKey,
                VerifyName,
                VerifyNameLength,
                VerifyAtLeastOneUpdate));

    /// <inheritdoc />
    public IBuilderForOptional DisableSharedAccountBalance()
    {
        this.useSharedBalance = false;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional EnableAccount()
    {
        this.isSuspended = false;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional EnableSharedAccountBalance()
    {
        this.useSharedBalance = true;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional SuspendAccount()
    {
        this.isSuspended = true;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithName(string value)
    {
        this.name = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithSubAccountKey(string value)
    {
        this.subAccountKey = value;
        return this;
    }

    private static Result<UpdateSubAccountRequest> VerifyAtLeastOneUpdate(UpdateSubAccountRequest request) =>
        request.Name.IsSome || request.UsePrimaryAccountBalance.IsSome || request.Suspended.IsSome
            ? request
            : Result<UpdateSubAccountRequest>.FromFailure(ResultFailure.FromErrorMessage("No property was modified."));

    private static Result<UpdateSubAccountRequest> VerifyName(UpdateSubAccountRequest request) =>
        request.Name.Match(
            name => InputValidation.VerifyNotEmpty(request, name, nameof(request.Name)),
            () => request);

    private static Result<UpdateSubAccountRequest> VerifyNameLength(UpdateSubAccountRequest request) =>
        request.Name.Match(
            name => InputValidation.VerifyLengthLowerOrEqualThan(request, name, NameMaxLength, nameof(request.Name)),
            () => request);

    private static Result<UpdateSubAccountRequest> VerifySubAccountKey(UpdateSubAccountRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SubAccountKey, nameof(request.SubAccountKey));
}

/// <summary>
///     Represents the first step of the builder requiring the subaccount API key.
/// </summary>
public interface IBuilderForSubAccountKey
{
    /// <summary>
    ///     Sets the unique API key of the subaccount to update.
    /// </summary>
    /// <param name="value">The subaccount API key.</param>
    /// <returns>The builder for optional properties.</returns>
    IBuilderForOptional WithSubAccountKey(string value);
}

/// <summary>
///     Represents the builder step for optional update properties. At least one property must be modified.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<UpdateSubAccountRequest>
{
    /// <summary>
    ///     Disables balance sharing with the primary account. The subaccount will have its own separate balance.
    /// </summary>
    /// <returns>The builder for additional optional properties.</returns>
    IBuilderForOptional DisableSharedAccountBalance();

    /// <summary>
    ///     Enables the account by removing its suspended status.
    /// </summary>
    /// <returns>The builder for additional optional properties.</returns>
    IBuilderForOptional EnableAccount();

    /// <summary>
    ///     Enables balance sharing with the primary account.
    /// </summary>
    /// <returns>The builder for additional optional properties.</returns>
    IBuilderForOptional EnableSharedAccountBalance();

    /// <summary>
    ///     Suspends the subaccount, preventing it from making API calls.
    /// </summary>
    /// <returns>The builder for additional optional properties.</returns>
    IBuilderForOptional SuspendAccount();

    /// <summary>
    ///     Sets a new name for the subaccount; limited to 80 characters.
    /// </summary>
    /// <param name="value">The new name for the subaccount.</param>
    /// <returns>The builder for additional optional properties.</returns>
    IBuilderForOptional WithName(string value);
}