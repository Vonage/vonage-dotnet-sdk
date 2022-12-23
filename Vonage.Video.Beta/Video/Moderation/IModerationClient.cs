﻿using Vonage.Video.Beta.Video.Moderation.DisconnectConnection;
using Vonage.Video.Beta.Video.Moderation.MuteStream;
using Vonage.Video.Beta.Video.Moderation.MuteStreams;

namespace Vonage.Video.Beta.Video.Moderation;

/// <summary>
///     Exposes features for moderating connections.
/// </summary>
public interface IModerationClient : IDisconnectConnectionUseCase, IMuteStreamUseCase, IMuteStreamsUseCase
{
}