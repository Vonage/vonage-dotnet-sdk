using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.SubAccounts.GetTransfers;

internal class GetTransfersRequestBuilder : IBuilderForOptional, IBuilderForStartDate
{
    private DateTimeOffset startDate;
    private Maybe<DateTimeOffset> endDate;
    private Maybe<string> subAccountKey;

    /// <inheritdoc />
    public Result<GetTransfersRequest> Create() =>
        Result<GetTransfersRequest>
            .FromSuccess(new GetTransfersRequest
            {
                StartDate = this.startDate,
                EndDate = this.endDate,
                SubAccountKey = this.subAccountKey,
            })
            .Bind(VerifySubAccountKey);

    /// <inheritdoc />
    public IBuilderForOptional WithEndDate(DateTimeOffset value)
    {
        this.endDate = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithStartDate(DateTimeOffset value)
    {
        this.startDate = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithSubAccountKey(string value)
    {
        this.subAccountKey = value;
        return this;
    }

    private static Result<GetTransfersRequest> VerifySubAccountKey(GetTransfersRequest request) =>
        request.SubAccountKey.Match(key => InputValidation.VerifyNotEmpty(request, key, nameof(request.SubAccountKey)),
            () => request);
}

/// <summary>
///     Represents a builder for StartDate.
/// </summary>
public interface IBuilderForStartDate
{
    /// <summary>
    ///     Sets the StartDate.
    /// </summary>
    /// <param name="value">The StartDate.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithStartDate(DateTimeOffset value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<GetTransfersRequest>
{
    /// <summary>
    ///     Sets the EndDate.
    /// </summary>
    /// <param name="value">The EndDate.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithEndDate(DateTimeOffset value);

    /// <summary>
    ///     Sets the SubAccount key.
    /// </summary>
    /// <param name="value">The SubAccount key.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithSubAccountKey(string value);
}