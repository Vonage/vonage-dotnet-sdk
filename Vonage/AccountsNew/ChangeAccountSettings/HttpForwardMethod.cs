using System.ComponentModel;

namespace Vonage.AccountsNew.ChangeAccountSettings;

/// <summary>
///     Represents the HTTP method used when making requests to the callback URLs.
/// </summary>
public enum HttpForwardMethod
{
    /// <summary>HTTP GET with query parameters.</summary>
    [Description("GET_QUERY_PARAMS")] GetQueryParams,

    /// <summary>HTTP POST with query parameters.</summary>
    [Description("POST_QUERY_PARAMS")] PostQueryParams,

    /// <summary>HTTP POST with JSON body.</summary>
    [Description("POST_JSON")] PostJson
}
