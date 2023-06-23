using System;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Serialization;

namespace Vonage.Meetings.Common;

/// <summary>
///     Represents a Room.
/// </summary>
public struct Room
{
    /// <summary>
    ///     Provides options to customize the room
    /// </summary>
    public Features AvailableFeatures { get; set; }

    /// <summary>
    ///     Provides callback URLs to listen to events
    /// </summary>
    public Callback CallbackUrls { get; set; }

    /// <summary>
    ///     The time for when the room was created, expressed in ISO 8601 format
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     The name of the meeting room
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    ///     Close the room after a session ends. Only relevant for long_term rooms.
    /// </summary>
    public bool ExpiresAfterUse { get; set; }

    /// <summary>
    ///     The time for when the room will be expired, expressed in ISO 8601 format
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    ///     The room UUID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Sets the default options for participants
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
    [JsonConverter(typeof(EnumDescriptionJsonConverter<RoomApprovalLevel>))]
    public RoomApprovalLevel JoinApprovalLevel { get; set; }

    /// <summary>
    ///     Represents HAL links for navigation purposes
    /// </summary>
    [JsonPropertyName("_links")]
    public RoomLinks Links { get; set; }

    /// <summary>
    ///     The meeting PIN number
    /// </summary>
    public string MeetingCode { get; set; }

    /// <summary>
    ///     Free text that can be attached to a room. This will be passed in the form of a header in events related to this
    ///     room.
    /// </summary>
    public string Metadata { get; set; }

    /// <summary>
    ///     An object containing various meeting recording options
    /// </summary>
    public RecordingOptions Recording { get; set; }

    /// <summary>
    ///     The theme id for the room
    /// </summary>
    public string ThemeId { get; set; }

    /// <summary>
    ///     The type of meeting which can be instant or long term. An instant is active for 10 minutes until the first
    ///     participant joins the roo, and remains active for 10 minutes after the last participant leaves. A long term room
    ///     expires after a specific date
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionJsonConverter<RoomType>))]
    public RoomType Type { get; set; }

    /// <summary>
    ///     Provides options to customize the user interface
    /// </summary>
    public UiSettings UserInterfaceSettings { get; set; }

    /// <summary>
    ///     An object containing various meeting recording options
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
    ///     Sets the default options for participants
    /// </summary>
    public struct JoinOptions
    {
        /// <summary>
        ///     Set the default microphone option for users in the pre-join screen of this room.
        /// </summary>
        [JsonConverter(typeof(EnumDescriptionJsonConverter<RoomMicrophoneState>))]
        public RoomMicrophoneState MicrophoneState { get; set; }
    }

    /// <summary>
    ///     Provides callback URLs to listen to events
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
    ///     Provides options to customize the room
    /// </summary>
    public struct Features
    {
        /// <summary>
        ///     Determine if captions are available in the UI.
        /// </summary>
        public bool IsCaptionsAvailable { get; set; }

        /// <summary>
        ///     Determine if chat feature is available in the UI.
        /// </summary>
        public bool IsChatAvailable { get; set; }

        /// <summary>
        ///     Determine if the locale switcher is available in the UI.
        /// </summary>
        public bool IsLocaleSwitcherAvailable { get; set; }

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
    ///     Provides links to join the meeting room
    /// </summary>
    public struct RoomLinks
    {
        /// <summary>
        ///     The link to join the meeting as participant, using the meeting code
        /// </summary>
        public HalLink GuestUrl { get; set; }

        /// <summary>
        ///     The link to join the meeting as host, using the meeting code
        /// </summary>
        public HalLink HostUrl { get; set; }
    }
}