using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.SubAccounts.TransferNumber;

internal class TransferNumberRequestBuilder : IBuilderForNumber, IBuilderForCountry, IBuilderForFrom, IBuilderForTo,
    IVonageRequestBuilder<TransferNumberRequest>
{
    private const int CountryLength = 2;
    private string country;
    private string from;
    private string number;
    private string to;

    /// <inheritdoc />
    public Result<TransferNumberRequest> Create() => Result<TransferNumberRequest>
        .FromSuccess(new TransferNumberRequest
        {
            Country = this.country,
            From = this.from,
            Number = this.number,
            To = this.to,
        })
        .Map(InputEvaluation<TransferNumberRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(
            VerifyFrom,
            VerifyTo,
            VerifyNumber,
            VerifyCountry));

    /// <inheritdoc />
    public IVonageRequestBuilder<TransferNumberRequest> WithCountry(string value)
    {
        this.country = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForTo WithFrom(string value)
    {
        this.from = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForCountry WithNumber(string value)
    {
        this.number = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForNumber WithTo(string value)
    {
        this.to = value;
        return this;
    }

    private static Result<TransferNumberRequest> VerifyCountry(TransferNumberRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Country, nameof(request.Country))
            .Bind(VerifyCountryLength);

    private static Result<TransferNumberRequest> VerifyCountryLength(TransferNumberRequest request) =>
        InputValidation.VerifyLength(request, request.Country, CountryLength, nameof(request.Country));

    private static Result<TransferNumberRequest> VerifyFrom(TransferNumberRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.From, nameof(request.From));

    private static Result<TransferNumberRequest> VerifyNumber(TransferNumberRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Number, nameof(request.Number));

    private static Result<TransferNumberRequest> VerifyTo(TransferNumberRequest request) =>
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
    /// <param name="value">The From.</param>
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
    /// <param name="value">The To.</param>
    /// <returns>The builder.</returns>
    IBuilderForNumber WithTo(string value);
}

/// <summary>
///     Represents a builder for Number.
/// </summary>
public interface IBuilderForNumber
{
    /// <summary>
    ///     Sets the Number.
    /// </summary>
    /// <param name="value">The Number.</param>
    /// <returns>The builder.</returns>
    IBuilderForCountry WithNumber(string value);
}

/// <summary>
///     Represents a builder for Country.
/// </summary>
public interface IBuilderForCountry
{
    /// <summary>
    ///     Sets the Country.
    /// </summary>
    /// <param name="value">The Country.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<TransferNumberRequest> WithCountry(string value);
}