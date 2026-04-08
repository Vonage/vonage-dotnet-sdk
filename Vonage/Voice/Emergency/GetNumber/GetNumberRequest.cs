#region
using System.Net.Http;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Voice.Emergency.GetNumber;

/// <inheritdoc />
public readonly struct GetNumberRequest : IVonageRequest
{
    private GetNumberRequest(PhoneNumber validNumber) => this.Number = validNumber;

    private PhoneNumber Number { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, $"/v1/emergency/numbers/{this.Number}")
        .Build();

    /// <summary>
    ///     Creates a new request to retrieve emergency number details.
    /// </summary>
    /// <param name="number">The phone number in E.164 format (e.g., "14155550100").</param>
    /// <returns>A <see cref="Result{T}"/> containing the request if successful, or validation errors if the number is invalid.</returns>
    public static Result<GetNumberRequest> Parse(string number) =>
        PhoneNumber.Parse(number).Map(validNumber => new GetNumberRequest(validNumber));
}