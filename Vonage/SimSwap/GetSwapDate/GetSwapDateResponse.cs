using System;
using System.Text.Json.Serialization;

namespace Vonage.SimSwap.GetSwapDate;

internal record GetSwapDateResponse(
    [property: JsonPropertyName("latestSimChange")]
    DateTimeOffset LatestSimChange);