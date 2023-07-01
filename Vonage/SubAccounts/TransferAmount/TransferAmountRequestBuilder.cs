using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.SubAccounts.TransferAmount;

internal class TransferAmountRequestBuilder : IBuilderForFrom, IBuilderForAmount, IBuilderForTo, IBuilderForOptional
{
    private decimal amount;
    private Maybe<string> reference;
    private string from;
    private string to;

    /// <inheritdoc />
    public Result<TransferAmountRequest> Create() =>
        Result<TransferAmountRequest>
            .FromSuccess(new TransferAmountRequest
            {
                From = this.from,
                To = this.to,
                Amount = this.amount,
                Reference = this.reference,
            })
            .Map(InputEvaluation<TransferAmountRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyFrom, VerifyTo, VerifyAmount));

    /// <inheritdoc />
    public IBuilderForOptional WithAmount(decimal value)
    {
        this.amount = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForTo WithFrom(string value)
    {
        this.from = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithReference(string value)
    {
        this.reference = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForAmount WithTo(string value)
    {
        this.to = value;
        return this;
    }

    private static Result<TransferAmountRequest> VerifyAmount(TransferAmountRequest amountRequest) =>
        InputValidation.VerifyNotNegative(amountRequest, amountRequest.Amount, nameof(amountRequest.Amount));

    private static Result<TransferAmountRequest> VerifyFrom(TransferAmountRequest amountRequest) =>
        InputValidation.VerifyNotEmpty(amountRequest, amountRequest.From, nameof(amountRequest.From));

    private static Result<TransferAmountRequest> VerifyTo(TransferAmountRequest amountRequest) =>
        InputValidation.VerifyNotEmpty(amountRequest, amountRequest.To, nameof(amountRequest.To));
}

/// <summary>
///     Represents a builder for From.
/// </summary>
public interface IBuilderForFrom
{
    /// <summary>
    ///     Sets the From.
    /// </summary>
    /// <param name="value">The from.</param>
    /// <returns>The builder.</returns>
    IBuilderForTo WithFrom(string value);
}

/// <summary>
///     Represents a builder for To.
/// </summary>
public interface IBuilderForTo
{
    /// <summary>
    ///     Sets the To.
    /// </summary>
    /// <param name="value">The to.</param>
    /// <returns>The builder.</returns>
    IBuilderForAmount WithTo(string value);
}

/// <summary>
///     Represents a builder for Amount.
/// </summary>
public interface IBuilderForAmount
{
    /// <summary>
    ///     Sets the Amount.
    /// </summary>
    /// <param name="value">The amount.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithAmount(decimal value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<TransferAmountRequest>
{
    /// <summary>
    ///     Sets the reference.
    /// </summary>
    /// <param name="value">The reference.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithReference(string value);
}