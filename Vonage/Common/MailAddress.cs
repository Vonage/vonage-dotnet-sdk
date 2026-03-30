#region
using System;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Common;

/// <summary>
///     Represents a validated email address using RFC 5321 standards.
/// </summary>
/// <remarks>
///     <para>Uses <see cref="System.Net.Mail.MailAddress"/> internally for validation.</para>
///     <para>Use <see cref="Parse"/> to create an instance with validation.</para>
/// </remarks>
public readonly struct MailAddress
{
    private const string InvalidEmail = "Email is invalid.";

    private MailAddress(string email) => this.Address = email;

    /// <summary>
    ///     Gets the validated email address string.
    /// </summary>
    public string Address { get; }

    /// <summary>
    ///     Parses and validates an email address.
    /// </summary>
    /// <param name="email">The email address to validate.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the validated <see cref="MailAddress"/> on success,
    ///     or an <see cref="IResultFailure"/> if the email format is invalid.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var result = MailAddress.Parse("user@example.com");
    /// result.Match(
    ///     email => Console.WriteLine($"Valid email: {email.Address}"),
    ///     failure => Console.WriteLine("Invalid email format")
    /// );
    /// ]]></code>
    /// </example>
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