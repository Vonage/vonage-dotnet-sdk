using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.DeviceStatus.GetConnectivityStatus;

internal struct GetConnectivityStatusRequestBuilder : IBuilderForPhoneNumber, IVonageRequestBuilder<GetConnectivityStatusRequest>
{
    private string number = default;

    public GetConnectivityStatusRequestBuilder()
    {
    }
    
    public IVonageRequestBuilder<GetConnectivityStatusRequest> WithPhoneNumber(string value) => this with {number = value};

    public Result<GetConnectivityStatusRequest> Create() => PhoneNumber.Parse(this.number)
        .Map(validNumber => new GetConnectivityStatusRequest() {PhoneNumber = validNumber})
        .Map(InputEvaluation<GetConnectivityStatusRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules());
}


/// <summary>
///     Represents a builder for PhoneNumber.
/// </summary>
public interface IBuilderForPhoneNumber
{
    /// <summary>
    ///     Sets the phone number on the builder.
    /// </summary>
    /// <param name="value">The phone number.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<GetConnectivityStatusRequest> WithPhoneNumber(string value);
}