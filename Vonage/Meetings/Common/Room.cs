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
    ///     Constructor.
    /// </summary>
    /// <param name="availableFeatures"></param>
    /// <param name="callbackUrls"></param>
    /// <param name="createdAt">The time for when the room was created, expressed in ISO 8601 format</param>
    /// <param name="displayName"></param>
    /// <param name="expiresAfterUse"> Close the room after a session ends. Only relevant for long_term rooms.</param>
    /// <param name="expiresAt">The time for when the room will be expired, expressed in ISO 8601 format</param>
    /// <param name="id"></param>
    /// <param name="initialJoinOptions"></param>
    /// <param name="isAvailable">   Once a room becomes unavailable, no new sessions can be created under it</param>
    /// <param name="joinApprovalLevel">
    ///     The level of approval needed to join the meeting in the room. When set to "after_owner_only" the participants will
    ///     join the meeting only after the host joined. When set to "explicit_approval" the participants will join the waiting
    ///     room and the host will deny/approve them.
    /// </param>
    /// <param name="links"></param>
    /// <param name="meetingCode"></param>
    /// <param name="metadata">
    ///     Free text that can be attached to a room. This will be passed in the form of a header in
    ///     events related to this room.
    /// </param>
    /// <param name="recording"></param>
    /// <param name="themeId"> The theme id for the room</param>
    /// <param name="type"></param>
    public Room(Features availableFeatures, Callback callbackUrls, string createdAt, string displayName,
        bool expiresAfterUse, string expiresAt, string id, JoinOptions initialJoinOptions, bool isAvailable,
        RoomApprovalLevel joinApprovalLevel, RoomLinks links, string meetingCode, string metadata,
        RecordingOptions recording,
        string themeId, RoomType type)
    {
        this.AvailableFeatures = availableFeatures;
        this.CallbackUrls = callbackUrls;
        this.CreatedAt = createdAt;
        this.DisplayName = displayName;
        this.ExpiresAfterUse = expiresAfterUse;
        this.ExpiresAt = expiresAt;
        this.Id = id;
        this.InitialJoinOptions = initialJoinOptions;
        this.IsAvailable = isAvailable;
        this.JoinApprovalLevel = joinApprovalLevel;
        this.Links = links;
        this.MeetingCode = meetingCode;
        this.Metadata = metadata;
        this.Recording = recording;
        this.ThemeId = themeId;
        this.Type = type;
    }

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

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="autoRecord">
        ///     Automatically record all sessions in this room. Recording cannot be stopped when this is set
        ///     to true.
        /// </param>
        /// <param name="recordOnlyOwner">  Record only the owner screen or any share screen of the video.</param>
        public RecordingOptions(bool autoRecord, bool recordOnlyOwner)
        {
            this.AutoRecord = autoRecord;
            this.RecordOnlyOwner = recordOnlyOwner;
        }
    }

    /// <summary>
    /// </summary>
    public struct JoinOptions
    {
        /// <summary>
        ///     Set the default microphone option for users in the pre-join screen of this room.
        /// </summary>
        public RoomMicrophoneState MicrophoneState { get; set; }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="microphoneState"> Set the default microphone option for users in the pre-join screen of this room.</param>
        public JoinOptions(RoomMicrophoneState microphoneState) => this.MicrophoneState = microphoneState;
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

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="recordingsCallbackUrl">
        ///     Callback url for recordings events, overrides application level recordings
        ///     callback url.
        /// </param>
        /// <param name="roomsCallbackUrl">Callback url for rooms events, overrides application level rooms callback url.</param>
        /// <param name="sessionsCallbackUrl"> Callback url for sessions events, overrides application level sessions callback url.</param>
        public Callback(string recordingsCallbackUrl, string roomsCallbackUrl, string sessionsCallbackUrl)
        {
            this.RecordingsCallbackUrl = recordingsCallbackUrl;
            this.RoomsCallbackUrl = roomsCallbackUrl;
            this.SessionsCallbackUrl = sessionsCallbackUrl;
        }
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

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="isChatAvailable">  Determine if chat feature is available in the UI.</param>
        /// <param name="isRecordingAvailable">  Determine if recording feature is available in the UI.</param>
        /// <param name="isWhiteboardAvailable">    Determine if whiteboard feature is available in the UI.</param>
        public Features(bool isChatAvailable, bool isRecordingAvailable, bool isWhiteboardAvailable)
        {
            this.IsChatAvailable = isChatAvailable;
            this.IsRecordingAvailable = isRecordingAvailable;
            this.IsWhiteboardAvailable = isWhiteboardAvailable;
        }

        /// <summary>
        ///     The default value.
        /// </summary>
        public static Features Default = new(true, true, true);
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

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="guestUrl"></param>
        /// <param name="hostUrl"></param>
        public RoomLinks(Link guestUrl, Link hostUrl)
        {
            this.GuestUrl = guestUrl;
            this.HostUrl = hostUrl;
        }
    }
}