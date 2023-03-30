using System;
using Vonage.Common.Failures;
using Vonage.Common.Monads;

namespace Vonage.Common;

/// <summary>
///     Represents an email address.
/// </summary>
public readonly struct MailAddress
{
    private const string InvalidEmail = "Email is invalid.";

    private MailAddress(string email) => this.Address = email;

    /// <summary>
    ///     The mail address.
    /// </summary>
    public string Address { get; }

    /// <summary>
    ///     Parses an email.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <returns>Success or failure.</returns>
    public static Result<MailAddress> Parse(string email)
    {
        try
        {
            _ = new System.Net.Mail.MailAddress(email);
            return Result<MailAddress>.FromSuccess(new MailAddress(email));
        }
        catch (Exception)
        {
            return Result<MailAddress>.FromFailure(ResultFailure.FromErrorMessage(InvalidEmail));
        }
    }
}