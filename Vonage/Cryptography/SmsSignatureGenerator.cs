using System.Security.Cryptography;
using System.Text;

namespace Vonage.Cryptography;

/// <summary>
///     Generates the signature applied to SMS API requests when using signed-request authentication.
///     The resulting signature is sent as the <c>sig</c> parameter so the Vonage API can verify the request
///     originated from the holder of the signature secret.
/// </summary>
/// <remarks>
///     Configure the desired hashing <see cref="Method"/> on <see cref="Vonage.Request.Credentials"/> via
///     <see cref="Vonage.Request.Credentials.FromApiKeySignatureSecretAndMethod"/>. HMAC variants are
///     preferred over the legacy <see cref="Method.md5hash"/> mode.
/// </remarks>
public class SmsSignatureGenerator
{
    /// <summary>
    ///     Generates a request signature using the supplied hashing method.
    /// </summary>
    /// <param name="query">The request query string to sign (parameters concatenated as <c>&amp;key=value</c> pairs).</param>
    /// <param name="securitySecret">The signature secret from your Vonage account dashboard.</param>
    /// <param name="method">The hashing method used to compute the signature.</param>
    /// <returns>
    ///     The hexadecimal signature. <see cref="Method.md5hash"/> returns lowercase MD5 of
    ///     <c>query + secret</c>; the HMAC variants return the uppercase HMAC of the query keyed by the secret.
    /// </returns>
    public static string GenerateSignature(string query, string securitySecret, Method method)
    {
        // security secret provided, sort and sign request
        if (method == Method.md5hash)
        {
            query += securitySecret;
            var hashgen = MD5.Create();
            var hash = hashgen.ComputeHash(Encoding.UTF8.GetBytes(query));
            return ByteArrayToHex(hash).ToLower();
        }

        var securityBytes = Encoding.UTF8.GetBytes(securitySecret);
        var input = Encoding.UTF8.GetBytes(query);
        HMAC hmacGen = new HMACMD5(securityBytes);
        switch (method)
        {
            case Method.md5:
                hmacGen = new HMACMD5(securityBytes);
                break;
            case Method.sha1:
                hmacGen = new HMACSHA1(securityBytes);
                break;
            case Method.sha256:
                hmacGen = new HMACSHA256(securityBytes);
                break;
            case Method.sha512:
                hmacGen = new HMACSHA512(securityBytes);
                break;
        }

        var hmac = hmacGen.ComputeHash(input);
        var sig = ByteArrayToHex(hmac).ToUpper();
        return sig;
    }

    /// <summary>
    ///     Identifies the hashing algorithm used to compute the SMS request signature.
    /// </summary>
    public enum Method
    {
        /// <summary>
        ///     Legacy MD5 hash of <c>query + secret</c>. Produces a lowercase hexadecimal digest.
        ///     Kept for backwards compatibility; prefer one of the HMAC variants for new integrations.
        /// </summary>
        md5hash,

        /// <summary>
        ///     HMAC-MD5 of the query keyed by the signature secret. Produces an uppercase hexadecimal digest.
        /// </summary>
        md5,

        /// <summary>
        ///     HMAC-SHA1 of the query keyed by the signature secret. Produces an uppercase hexadecimal digest.
        /// </summary>
        sha1,

        /// <summary>
        ///     HMAC-SHA256 of the query keyed by the signature secret. Produces an uppercase hexadecimal digest.
        /// </summary>
        sha256,

        /// <summary>
        ///     HMAC-SHA512 of the query keyed by the signature secret. Produces an uppercase hexadecimal digest.
        ///     Recommended for new integrations.
        /// </summary>
        sha512
    }

    ///// There is no built-in byte[] => hex string, so here's an implementation
    /// http://stackoverflow.com/questions/311165/how-do-you-convert-byte-array-to-hexadecimal-string-and-vice-versa/24343727#24343727
    /// We're not going to going with the unchecked version. Seems overkill for now.
    internal static readonly uint[] _lookup32 = CreateLookup32();

    internal static uint[] CreateLookup32()
    {
        var result = new uint[256];
        for (var i = 0; i < 256; i++)
        {
            var s = i.ToString("X2");
            result[i] = s[0] + ((uint) s[1] << 16);
        }

        return result;
    }

    internal static string ByteArrayToHex(byte[] bytes)
    {
        var lookup32 = _lookup32;
        var result = new char[bytes.Length * 2];
        for (var i = 0; i < bytes.Length; i++)
        {
            var val = lookup32[bytes[i]];
            result[2 * i] = (char) val;
            result[2 * i + 1] = (char) (val >> 16);
        }

        return new string(result);
    }
}