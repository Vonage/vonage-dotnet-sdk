using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.NumberInsightV2.FraudCheck;

internal class FraudCheckRequestBuilder : IBuilderForPhone, IBuilderForOptional
{
    /// <inheritdoc />
    public Result<FraudCheckRequest> Create() => throw new NotImplementedException();

    /// <inheritdoc />
    public IBuilderForOptional WithFraudScore() => throw new NotImplementedException();

    /// <inheritdoc />
    public IBuilderForOptional WithPhone(string value) => throw new NotImplementedException();

    /// <inheritdoc />
    public IBuilderForOptional WithSimSwap() => throw new NotImplementedException();
}

/// <summary>
///     Represents a builder for Phone.
/// </summary>
public interface IBuilderForPhone
{
    /// <summary>
    ///     Sets the Phone.
    /// </summary>
    /// <param name="value">The phone.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithPhone(string value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<FraudCheckRequest>
{
    /// <summary>
    ///     Enables Fraud Score in the response.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithFraudScore();

    /// <summary>
    ///     Enables Sim Swap in the response.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithSimSwap();
}