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

/// <summary>
///     Represents a request to create a new subaccount under the primary account.
/// </summary>
[Builder]
public readonly partial struct CreateSubAccountRequest : IVonageRequest
{
    private const int NameMaxLength = 80;

    private string ApiKey { get; init; }

    /// <summary>
    ///     Sets the name of the subaccount; limited to 80 characters.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithName("Department A")
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(0)]
    [Mandatory(0)]
    public string Name { get; internal init; }

    /// <summary>
    ///     Sets the API secret for the subaccount. If not provided, a secret will be auto-generated.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithSecret("MySecurePassword123")
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(2)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [Optional]
    public Maybe<string> Secret { get; internal init; }

    /// <summary>
    ///     Disables balance sharing with the primary account. By default, subaccounts share the primary account's balance.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .DisableSharedAccountBalance()
    /// ]]></code>
    /// </example>
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

    [ValidationRule]
    internal static Result<CreateSubAccountRequest> VerifyName(CreateSubAccountRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Name, nameof(request.Name));

    [ValidationRule]
    internal static Result<CreateSubAccountRequest> VerifyNameLength(CreateSubAccountRequest request) =>
        InputValidation
            .VerifyLengthLowerOrEqualThan(request, request.Name, NameMaxLength, nameof(request.Name));

    internal CreateSubAccountRequest WithApiKey(string primaryAccountKey) => this with {ApiKey = primaryAccountKey};
}