#region
using System;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice;

internal record SubscribeRealTimeDtmfCommand([property: JsonProperty("event_url")] Uri[] EventUrls);