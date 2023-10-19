using System.Security.Cryptography;
using System.Text;

namespace Vonage.Cryptography;

public class SmsSignatureGenerator
{
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

    public enum Method
    {
        md5hash,
        md5,
        sha1,
        sha256,
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