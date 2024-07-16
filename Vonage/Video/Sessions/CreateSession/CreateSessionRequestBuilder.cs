using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;

namespace Vonage.Video.Sessions.CreateSession;

internal struct CreateSessionRequestBuilder :
    IBuilderForLocation,
    IBuilderForMediaMode,
    IBuilderForArchiveMode,
    IBuilderForOptional
{
    private const string IncompatibleMediaAndArchive =
        "A session with always archive mode must also have the routed media mode.";

    private ArchiveMode archiveMode = CreateSessionRequest.Default.ArchiveMode;
    private MediaMode mediaMode = CreateSessionRequest.Default.MediaMode;
    private Result<IpAddress> location = CreateSessionRequest.Default.Location;
    private bool isEncryptionEnabled = CreateSessionRequest.Default.EndToEndEncryption;

    public CreateSessionRequestBuilder()
    {
    }

    public Result<CreateSessionRequest> Create() =>
        ParseAddress(this.location, this.mediaMode, this.archiveMode, this.isEncryptionEnabled);

    private static Result<CreateSessionRequest> ParseAddress(Result<IpAddress> location, MediaMode mediaMode,
        ArchiveMode archiveMode, bool isEncryptionEnabled) =>
        location.Bind(address => Parse(address, mediaMode, archiveMode, isEncryptionEnabled));

    public IVonageRequestBuilder<CreateSessionRequest> EnableEncryption() => this with {isEncryptionEnabled = true};

    public IBuilderForOptional WithArchiveMode(ArchiveMode value) => this with {archiveMode = value};

    public IBuilderForMediaMode WithLocation(string value) => this with {location = IpAddress.Parse(value)};

    public IBuilderForMediaMode WithLocation(IpAddress value) => this with {location = value};

    public IBuilderForArchiveMode WithMediaMode(MediaMode value) => this with {mediaMode = value};

    private static bool AreMediaAndArchiveCompatible(MediaMode mediaMode, ArchiveMode archiveMode) =>
        archiveMode == ArchiveMode.Manual || mediaMode == MediaMode.Routed;

    private static Result<CreateSessionRequest> Parse(IpAddress ipAddress, MediaMode mediaMode, ArchiveMode archiveMode,
        bool isEncryptionEnabled) =>
        AreMediaAndArchiveCompatible(mediaMode, archiveMode)
            ? Result<CreateSessionRequest>.FromSuccess(new CreateSessionRequest
            {
                ArchiveMode = archiveMode,
                MediaMode = mediaMode,
                Location = ipAddress,
                EndToEndEncryption = isEncryptionEnabled,
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
    IBuilderForOptional WithArchiveMode(ArchiveMode value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<CreateSessionRequest>
{
    /// <summary>
    ///     Enables end-to-end encryption.
    /// </summary>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<CreateSessionRequest> EnableEncryption();
}