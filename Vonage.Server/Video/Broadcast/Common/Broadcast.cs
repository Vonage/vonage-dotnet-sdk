using System;
using System.Collections.Generic;

namespace Vonage.Server.Video.Broadcast.Common;

/// <summary>
///     Represents a broadcast.
/// </summary>
public struct Broadcast
{
    public Guid ApplicationId { get; set; }
    public BroadcastUrl BroadcastUrls { get; set; }
    public long CreatedAt { get; set; }
    public bool HasAudio { get; set; }
    public bool HasVideo { get; set; }
    public string Id { get; set; }
    public int MaxBitrate { get; set; }
    public long MaxDuration { get; set; }
    public string MultiBroadcastTag { get; set; }
    public string Resolution { get; set; }
    public string SessionId { get; set; }
    public BroadcastSettings Settings { get; set; }
    public string Status { get; set; }
    public string StreamMode { get; set; }
    public List<BroadcastStream> Streams { get; set; }
    public long UpdatedAt { get; set; }

    public struct BroadcastUrl
    {
        public Uri Hls { get; set; }

        public List<BroadcastUrlRtmp> Rtmp { get; set; }

        public struct BroadcastUrlRtmp
        {
            public string Id { get; set; }
            public string ServerUrl { get; set; }
            public string Status { get; set; }
            public string StreamName { get; set; }
        }
    }

    public struct BroadcastSettings
    {
        public BroadcastSettingsHls Hls { get; set; }

        public struct BroadcastSettingsHls
        {
            public bool Dvr { get; set; }
            public bool LowLatency { get; set; }
        }
    }

    public struct BroadcastStream
    {
        public bool HasAudio { get; set; }
        public bool HasVideo { get; set; }
        public string StreamId { get; set; }
    }
}