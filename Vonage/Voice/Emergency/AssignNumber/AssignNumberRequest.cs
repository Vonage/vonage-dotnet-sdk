#region
using System;
using System.Net.Http;
using System.Text;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Serialization;
#endregion

namespace Vonage.Voice.Emergency.AssignNumber;

/// <inheritdoc />
[Builder]
public readonly partial struct AssignNumberRequest : IVonageRequest
{
    /// <summary>
    /// </summary>
    [Mandatory(0)]
    public string Number { get; internal init; }

    /// <summary>
    /// </summary>
    [Mandatory(1)]
    public Guid AddressId { get; internal init; }

    /// <summary>
    /// </summary>
    [Mandatory(2)]
    public string ContactName { get; internal init; }

    private PhoneNumber ParsedNumber { get; init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(new HttpMethod("PATCH"), $"/v1/emergency/numbers/{this.ParsedNumber}")
            .WithContent(this.GetRequestContent())
            .Build();

    private StringContent GetRequestContent() =>
        new StringContent(
            JsonSerializerBuilder.BuildWithCamelCase()
                .SerializeObject(new {address = new {id = this.AddressId}, contact_name = this.ContactName}),
            Encoding.UTF8, "application/json");

    [ValidationRule]
    internal static Result<AssignNumberRequest> VerifyNumber(AssignNumberRequest request) =>
        PhoneNumber.Parse(request.Number).Map(v => request with {ParsedNumber = v});

    [ValidationRule]
    internal static Result<AssignNumberRequest> VerifyAddressId(AssignNumberRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.AddressId, nameof(request.AddressId));

    [ValidationRule]
    internal static Result<AssignNumberRequest> VerifyContactName(AssignNumberRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ContactName, nameof(request.ContactName));
}