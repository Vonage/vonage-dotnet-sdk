using System;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.UpdateThemeLogo;

internal struct GetUploadLogosUrlResponse
{
    public UploadDetails Fields { get; set; }
    public Uri Url { get; set; }

    public bool MatchesLogoType(ThemeLogoType logoType) => this.Fields.LogoType == logoType;
}