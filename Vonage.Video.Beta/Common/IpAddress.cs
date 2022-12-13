﻿using System.Net;

namespace Vonage.Video.Beta.Common;

/// <summary>
///     Represents a valid IpAddress.
/// </summary>
public readonly struct IpAddress
{
    private const string ParseFailedMessage = "Unable to parse location '{location}'.";

    private IpAddress(string address)
    {
        this.Address = address;
    }

    /// <summary>
    ///     The address.
    /// </summary>
    public string Address { get; }

    /// <summary>
    ///     Parses the provided ip address.
    /// </summary>
    /// <param name="location">The ip address.</param>
    /// <returns>Success if the parsing operation succeeded, Failure if it failed.</returns>
    public static Result<IpAddress> Parse(string location) =>
        IsEmpty(location)
        || IsLocalhost(location)
        || CanBeParsed(location)
            ? Result<IpAddress>.FromSuccess(new IpAddress(location))
            : Result<IpAddress>.FromFailure(
                ResultFailure.FromErrorMessage(ParseFailedMessage.Replace("{location}", location)));

    private static bool IsLocalhost(string location) => location == Localhost.Address;

    private static bool IsEmpty(string location) => location == Empty.Address;

    private static bool CanBeParsed(string location) => IPAddress.TryParse(location, out _);

    /// <summary>
    ///     Returns the Localhost address.
    /// </summary>
    public static IpAddress Localhost => new("localhost");

    /// <summary>
    ///     Returns an empty address.
    /// </summary>
    public static IpAddress Empty => new(string.Empty);
}