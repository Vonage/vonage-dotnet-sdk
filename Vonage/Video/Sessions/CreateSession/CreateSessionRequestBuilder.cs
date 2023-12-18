using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;

namespace Vonage.Video.Sessions.CreateSession;

internal class CreateSessionRequestBuilder :
    IVonageRequestBuilder<CreateSessionRequest>,
    IBuilderForLocation,
    IBuilderForMediaMode,
    IBuilderForArchiveMode
{
    private const string IncompatibleMediaAndArchive =
        "A session with always archive mode must also have the routed media mode.";

    private ArchiveMode archiveMode;
    private MediaMode mediaMode;
    private Result<IpAddress> location;

    /// <inheritdoc />
    public Result<CreateSessionRequest> Create() =>
        this.location
            .Bind(address => Parse(address, this.mediaMode, this.archiveMode));

    /// <inheritdoc />
    public IVonageRequestBuilder<CreateSessionRequest> WithArchiveMode(ArchiveMode value)
    {
        this.archiveMode = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForMediaMode WithLocation(string value)
    {
        this.location = IpAddress.Parse(value);
        return this;
    }

    /// <inheritdoc />
    public IBuilderForMediaMode WithLocation(IpAddress value)
    {
        this.location = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForArchiveMode WithMediaMode(MediaMode value)
    {
        this.mediaMode = value;
        return this;
    }

    private static bool AreMediaAndArchiveCompatible(MediaMode mediaMode, ArchiveMode archiveMode) =>
        archiveMode == ArchiveMode.Manual || mediaMode == MediaMode.Routed;

    private static Result<CreateSessionRequest>
        Parse(IpAddress ipAddress, MediaMode mediaMode, ArchiveMode archiveMode) =>
        AreMediaAndArchiveCompatible(mediaMode, archiveMode)
            ? Result<CreateSessionRequest>.FromSuccess(new CreateSessionRequest
            {
                ArchiveMode = archiveMode,
                MediaMode = mediaMode,
                Location = ipAddress,
            })
            : ParsingFailure.FromFailures(ResultFailure.FromErrorMessage(IncompatibleMediaAndArchive))
                .ToResult<CreateSessionRequest>();
}

/// <summary>
///     Represents a builder that allows to set the Location.
/// </summary>
public interface IBuilderForLocation
{
    /// <summary>
    ///     Sets the Location on the builder.
    /// </summary>
    /// <param name="value">The location.</param>
    /// <returns>The builder.</returns>
    IBuilderForMediaMode WithLocation(string value);

    /// <summary>
    ///     Sets the Location on the builder.
    /// </summary>
    /// <param name="value">The location.</param>
    /// <returns>The builder.</returns>
    IBuilderForMediaMode WithLocation(IpAddress value);
}

/// <summary>
///     Represents a builder that allows to set the MediaMode.
/// </summary>
public interface IBuilderForMediaMode
{
    /// <summary>
    ///     Sets the MediaMode on the builder.
    /// </summary>
    /// <param name="value">The media mode.</param>
    /// <returns>The builder.</returns>
    IBuilderForArchiveMode WithMediaMode(MediaMode value);
}

/// <summary>
///     Represents a builder that allows to set the ArchiveMode.
/// </summary>
public interface IBuilderForArchiveMode
{
    /// <summary>
    ///     Sets the ArchiveMode on the builder.
    /// </summary>
    /// <param name="value">The archive mode.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<CreateSessionRequest> WithArchiveMode(ArchiveMode value);
}