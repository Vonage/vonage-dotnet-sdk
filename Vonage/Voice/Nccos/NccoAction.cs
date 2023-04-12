﻿using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vonage.Voice.Nccos;

public abstract class NccoAction
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ActionType
    {
        [EnumMember(Value = "record")] Record = 1,

        [EnumMember(Value = "conversation")] Conversation = 2,

        [EnumMember(Value = "connect")] Connect = 3,

        [EnumMember(Value = "talk")] Talk = 4,

        [EnumMember(Value = "stream")] Stream = 5,

        [EnumMember(Value = "input")] Input = 6,

        [EnumMember(Value = "notify")] Notify = 7,
    }

    [JsonProperty("action")] public abstract ActionType Action { get; }
}