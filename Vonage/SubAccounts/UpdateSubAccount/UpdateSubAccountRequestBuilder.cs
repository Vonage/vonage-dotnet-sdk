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
///     Represents a builder for SubAccountKey.
/// </summary>
public interface IBuilderForSubAccountKey
{
    /// <summary>
    ///     Sets the SubAccount key.
    /// </summary>
    /// <param name="value">The SubAccount key.</param>
    /// <returns></returns>
    IBuilderForOptional WithSubAccountKey(string value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<UpdateSubAccountRequest>
{
    /// <summary>
    ///     Disables shared balance with primary account.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptional DisableSharedAccountBalance();

    /// <summary>
    ///     Enables the account.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptional EnableAccount();

    /// <summary>
    ///     Enables shared balance with primary account.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptional EnableSharedAccountBalance();

    /// <summary>
    ///     Suspends the account.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptional SuspendAccount();

    /// <summary>
    ///     Sets the Name.
    /// </summary>
    /// <param name="value">The name.</param>
    /// <returns></returns>
    IBuilderForOptional WithName(string value);
}