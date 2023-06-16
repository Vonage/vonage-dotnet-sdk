using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.SubAccounts.Transfer;

internal class TransferRequestBuilder : IBuilderForFrom, IBuilderForAmount, IBuilderForTo, IBuilderForOptional
{
    private decimal amount;
    private Maybe<string> reference;
    private string from;
    private string to;

    /// <inheritdoc />
    public Result<TransferRequest> Create() =>
        Result<TransferRequest>
            .FromSuccess(new TransferRequest
            {
                From = this.from,
                To = this.to,
                Amount = this.amount,
                Reference = this.reference,
            })
            .Bind(VerifyFrom)
            .Bind(VerifyTo)
            .Bind(VerifyAmount);

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

    private static Result<TransferRequest> VerifyAmount(TransferRequest request) =>
        InputValidation.VerifyNotNegative(request, request.Amount, nameof(request.Amount));

    private static Result<TransferRequest> VerifyFrom(TransferRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.From, nameof(request.From));

    private static Result<TransferRequest> VerifyTo(TransferRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.To, nameof(request.To));
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
public interface IBuilderForOptional : IVonageRequestBuilder<TransferRequest>
{
    /// <summary>
    ///     Sets the reference.
    /// </summary>
    /// <param name="value">The reference.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithReference(string value);
}