using System.Text.Json.Serialization;

namespace Vonage.Meetings.Common;

/// <summary>
/// </summary>
public struct Room
{
    /// <summary>
    /// </summary>
    public Features AvailableFeatures { get; set; }

    /// <summary>
    /// </summary>
    public Callback CallbackUrls { get; set; }

    /// <summary>
    ///     The time for when the room was created, expressed in ISO 8601 format
    /// </summary>
    public string CreatedAt { get; set; }

    /// <summary>
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    ///     Close the room after a session ends. Only relevant for long_term rooms.
    /// </summary>
    public bool ExpiresAfterUse { get; set; }

    /// <summary>
    ///     The time for when the room will be expired, expressed in ISO 8601 format
    /// </summary>
    public string ExpiresAt { get; set; }

    /// <summary>
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// </summary>
    public JoinOptions InitialJoinOptions { get; set; }

    /// <summary>
    ///     Once a room becomes unavailable, no new sessions can be created under it
    /// </summary>
    public bool IsAvailable { get; set; }

    /// <summary>
    ///     The level of approval needed to join the meeting in the room. When set to "after_owner_only" the participants will
    ///     join the meeting only after the host joined. When set to "explicit_approval" the participants will join the waiting
    ///     room and the host will deny/approve them.
    /// </summary>
    public RoomApprovalLevel JoinApprovalLevel { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("_links")]
    public RoomLinks Links { get; set; }

    /// <summary>
    /// </summary>
    public string MeetingCode { get; set; }

    /// <summary>
    ///     Free text that can be attached to a room. This will be passed in the form of a header in events related to this
    ///     room.
    /// </summary>
    public string Metadata { get; set; }

    /// <summary>
    /// </summary>
    public RecordingOptions Recording { get; set; }

    /// <summary>
    ///     The theme id for the room
    /// </summary>
    public string ThemeId { get; set; }

    /// <summary>
    /// </summary>
    public RoomType Type { get; set; }

    /// <summary>
    /// </summary>
    public struct RecordingOptions
    {
        /// <summary>
        ///     Automatically record all sessions in this room. Recording cannot be stopped when this is set to true.
        /// </summary>
        public bool AutoRecord { get; set; }

        /// <summary>
        ///     Record only the owner screen or any share screen of the video.
        /// </summary>
        public bool RecordOnlyOwner { get; set; }
    }

    /// <summary>
    /// </summary>
    public struct JoinOptions
    {
        /// <summary>
        ///     Set the default microphone option for users in the pre-join screen of this room.
        /// </summary>
        public RoomMicrophoneState MicrophoneState { get; set; }
    }

    /// <summary>
    /// </summary>
    public struct Callback
    {
        /// <summary>
        ///     Callback url for recordings events, overrides application level recordings callback url.
        /// </summary>
        public string RecordingsCallbackUrl { get; set; }

        /// <summary>
        ///     Callback url for rooms events, overrides application level rooms callback url.
        /// </summary>
        public string RoomsCallbackUrl { get; set; }

        /// <summary>
        ///     Callback url for sessions events, overrides application level sessions callback url.
        /// </summary>
        public string SessionsCallbackUrl { get; set; }
    }

    /// <summary>
    /// </summary>
    public struct Features
    {
        /// <summary>
        ///     Determine if chat feature is available in the UI.
        /// </summary>
        public bool IsChatAvailable { get; set; }

        /// <summary>
        ///     Determine if recording feature is available in the UI.
        /// </summary>
        public bool IsRecordingAvailable { get; set; }

        /// <summary>
        ///     Determine if whiteboard feature is available in the UI.
        /// </summary>
        public bool IsWhiteboardAvailable { get; set; }
    }

    /// <summary>
    /// </summary>
    public struct RoomLinks
    {
        /// <summary>
        /// </summary>
        public Link GuestUrl { get; set; }

        /// <summary>
        /// </summary>
        public Link HostUrl { get; set; }
    }
}