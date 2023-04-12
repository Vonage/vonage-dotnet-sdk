﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vonage.Voice;

public class CallList
{
    [JsonProperty("calls")]
    public List<CallRecord> Calls { get; set; }
}