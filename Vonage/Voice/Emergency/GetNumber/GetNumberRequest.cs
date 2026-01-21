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
    ///     Parses the input into a GetNumberRequest.
    /// </summary>
    /// <param name="number">The Number</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<GetNumberRequest> Parse(string number) =>
        PhoneNumber.Parse(number).Map(validNumber => new GetNumberRequest(validNumber));
}