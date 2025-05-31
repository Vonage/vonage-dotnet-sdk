#region
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Common.Validation;
using Vonage.Serialization;
#endregion

namespace Vonage.SubAccounts.CreateSubAccount;

/// <inheritdoc />
[Builder]
public readonly partial struct CreateSubAccountRequest : IVonageRequest
{
    private const int NameMaxLength = 80;

    /// <summary>
    ///     Unique primary account ID.
    /// </summary>
    private string ApiKey { get; init; }

    /// <summary>
    ///     Name of the subaccount.
    /// </summary>
    [JsonPropertyOrder(0)]
    [Mandatory(0, nameof(VerifyName), nameof(VerifyNameLength))]
    public string Name { get; internal init; }

    /// <summary>
    ///     Secret of the subaccount.
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [Optional]
    public Maybe<string> Secret { get; internal init; }

    /// <summary>
    ///     Flag showing if balance is shared with primary account.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonPropertyName("use_primary_account_balance")]
    [OptionalBoolean(true, "DisableSharedAccountBalance")]
    public bool UsePrimaryAccountBalance { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, $"/accounts/{this.ApiKey}/subaccounts")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    internal static Result<CreateSubAccountRequest> VerifyName(CreateSubAccountRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Name, nameof(request.Name));

    internal static Result<CreateSubAccountRequest> VerifyNameLength(CreateSubAccountRequest request) =>
        InputValidation
            .VerifyLengthLowerOrEqualThan(request, request.Name, NameMaxLength, nameof(request.Name));

    internal CreateSubAccountRequest WithApiKey(string primaryAccountKey) => this with {ApiKey = primaryAccountKey};
}