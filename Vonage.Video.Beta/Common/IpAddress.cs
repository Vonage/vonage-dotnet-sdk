using System.Net;

namespace Vonage.Video.Beta.Common;

public struct IpAddress
{
    private IpAddress(string address)
    {
        this.Address = address;
    }

    public string Address { get; }

    public static Result<IpAddress> Parse(string location) =>
        IsEmpty(location)
        || IsLocalhost(location)
        || CanBeParsed(location)
            ? Result<IpAddress>.FromSuccess(new IpAddress(location))
            : Result<IpAddress>.FromFailure(ResultFailure.FromErrorMessage($"Unable to parse location '{location}'."));

    private static bool IsLocalhost(string location) => location == Localhost.Address;

    private static bool IsEmpty(string location) => location == Empty.Address;

    private static bool CanBeParsed(string location) => IPAddress.TryParse(location, out _);

    public static IpAddress Localhost => new("localhost");

    public static IpAddress Empty => new("");
}