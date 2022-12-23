using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Moderation.MuteStreams;

/// <inheritdoc />
public class MuteStreamsUseCase : IMuteStreamsUseCase
{
    /// <inheritdoc />
    public Task<Result<MuteStreamsResponse>> MuteStreamSAsync(MuteStreamsRequest request) =>
        throw new NotImplementedException();
}