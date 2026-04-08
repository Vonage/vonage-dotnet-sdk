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
    ///     Sets the phone number to assign an emergency address to, in E.164 format (e.g., "14155550100").
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithNumber("14155550100")
    /// ]]></code>
    /// </example>
    [Mandatory(0)]
    public string Number { get; internal init; }

    /// <summary>
    ///     Sets the unique identifier of the emergency address to assign to this number.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithAddressId(Guid.Parse("8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9"))
    /// ]]></code>
    /// </example>
    [Mandatory(1)]
    public Guid AddressId { get; internal init; }

    /// <summary>
    ///     Sets the contact name associated with this emergency number.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithContactName("John Doe")
    /// ]]></code>
    /// </example>
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