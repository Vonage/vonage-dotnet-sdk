using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.VerifyV2.VerifyCode;

internal class VerifyCodeRequestBuilder : IVonageRequestBuilder<VerifyCodeRequest>, IBuilderForCode,
    IBuilderForRequestId
{
    private Guid requestId;
    private string code;

    /// <inheritdoc />
    public Result<VerifyCodeRequest> Create() => Result<VerifyCodeRequest>.FromSuccess(new VerifyCodeRequest
        {
            Code = this.code,
            RequestId = this.requestId,
        })
        .Bind(VerifyRequestIdNotEmpty)
        .Bind(VerifyCodeNotEmpty);

    /// <inheritdoc />
    public IVonageRequestBuilder<VerifyCodeRequest> WithCode(string value)
    {
        this.code = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForCode WithRequestId(Guid value)
    {
        this.requestId = value;
        return this;
    }

    private static Result<VerifyCodeRequest> VerifyCodeNotEmpty(
        VerifyCodeRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.Code, nameof(request.Code));

    private static Result<VerifyCodeRequest> VerifyRequestIdNotEmpty(
        VerifyCodeRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.RequestId, nameof(request.RequestId));
}

/// <summary>
///     Represents a builder to set the RequestId.
/// </summary>
public interface IBuilderForRequestId
{
    /// <summary>
    ///     Sets the RequestId.
    /// </summary>
    /// <param name="value">The RequestId.</param>
    /// <returns>The builder.</returns>
    IBuilderForCode WithRequestId(Guid value);
}

/// <summary>
///     Represents a builder to set the Code.
/// </summary>
public interface IBuilderForCode
{
    /// <summary>
    ///     Sets the code.
    /// </summary>
    /// <param name="value">The code.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<VerifyCodeRequest> WithCode(string value);
}