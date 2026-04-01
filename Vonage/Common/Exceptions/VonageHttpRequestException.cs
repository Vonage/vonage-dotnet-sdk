#region
using System;
using System.Net;
using System.Net.Http;
#endregion

namespace Vonage.Common.Exceptions;

/// <summary>
///     Represents an issue when processing an HttpRequest.
/// </summary>
/// <example>
///     <code><![CDATA[
/// throw new VonageHttpRequestException("Request failed")
/// {
///     HttpStatusCode = HttpStatusCode.BadRequest,
///     Json = "{\"error\":\"invalid_request\"}"
/// };
/// ]]></code>
/// </example>
public class VonageHttpRequestException : HttpRequestException
{
    /// <summary>
    ///     Creates a VonageHttpRequestException.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <example>
    ///     <code><![CDATA[
    /// throw new VonageHttpRequestException("Server returned an error");
    /// ]]></code>
    /// </example>
    public VonageHttpRequestException(string message) : base(message)
    {
    }

    /// <summary>
    ///     Creates a VonageHttpRequestException.
    /// </summary>
    /// <param name="inner">The inner exception.</param>
    /// <example>
    ///     <code><![CDATA[
    /// try { /* http operation */ }
    /// catch (Exception ex)
    /// {
    ///     throw new VonageHttpRequestException(ex);
    /// }
    /// ]]></code>
    /// </example>
    public VonageHttpRequestException(Exception inner) : base(inner.Message, inner)
    {
    }

    /// <summary>
    ///     The response status code.
    /// </summary>
    /// <example>
    ///     <code><![CDATA[
    /// catch (VonageHttpRequestException ex)
    /// {
    ///     if (ex.HttpStatusCode == HttpStatusCode.NotFound) { /* handle 404 */ }
    /// }
    /// ]]></code>
    /// </example>
    public HttpStatusCode HttpStatusCode { get; init; }

    /// <summary>
    ///     The response body content.
    /// </summary>
    /// <example>
    ///     <code><![CDATA[
    /// catch (VonageHttpRequestException ex)
    /// {
    ///     Console.WriteLine($"Error response: {ex.Json}");
    /// }
    /// ]]></code>
    /// </example>
    public string Json { get; init; }
}