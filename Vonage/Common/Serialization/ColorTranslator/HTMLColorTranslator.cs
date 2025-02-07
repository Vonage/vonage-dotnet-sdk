using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Vonage.Common.Serialization.ColorTranslator;

// From: https://github.com/stephenhand/.NETCore-Compatibility-Helpers/blob/master/src/ColorTranslations/ARGB.cs
// Credit: Stephen Hand

internal class HTMLColorTranslator
{
    private static readonly Dictionary<string, ARGB> NAMED_COLOR_MAP = new()
    {
        //X11 Color Names (W3C color names)
        //Red color names
        {"indianred",new ARGB(205, 92, 92)},
        {"lightcoral",new ARGB(240, 128, 128)},
        {"salmon",new ARGB(250, 128, 114)},
        {"darksalmon",new ARGB(233, 150, 122)},
        {"crimson",new ARGB(220, 20, 60)},
        {"red",new ARGB(255, 0, 0)},
        {"firebrick",new ARGB(178, 34, 34)},
        {"darkred",new ARGB(139, 0, 0)},
        //Pink color names
        {"pink",new ARGB(255, 192, 203)},
        {"lightpink",new ARGB(255, 182, 193)},
        {"hotpink",new ARGB(255, 105, 180)},
        {"deeppink",new ARGB(255, 20, 147)},
        {"mediumvioletred",new ARGB(199, 21, 133)},
        {"palevioletred",new ARGB(219, 112, 147)},
        //Orange color names
        {"lightsalmon",new ARGB(255, 160, 122)},
        {"coral",new ARGB(255, 127, 80)},
        {"tomato",new ARGB(255, 99, 71)},
        {"orangered",new ARGB(255, 69, 0)},
        {"darkorange",new ARGB(255, 140, 0)},
        {"orange",new ARGB(255, 165, 0)},
        //Yellow color names
        {"gold",new ARGB(255, 215, 0)},
        {"yellow",new ARGB(255, 255, 0)},
        {"lightyellow",new ARGB(255, 255, 224)},
        {"lemonchiffon",new ARGB(255, 250, 205)},
        {"lightgoldenrodyellow",new ARGB(250, 250, 210)},
        {"papayawhip",new ARGB(255, 239, 213)},
        {"moccasin",new ARGB(255, 228, 181)},
        {"peachpuff",new ARGB(255, 218, 185)},
        {"palegoldenrod",new ARGB(238, 232, 170)},
        {"khaki",new ARGB(240, 230, 140)},
        {"darkkhaki",new ARGB(189, 183, 107)},
        //Purple color names
        {"lavender",new ARGB(230, 230, 250)},
        {"thistle",new ARGB(216, 191, 216)},
        {"plum",new ARGB(221, 160, 221)},
        {"violet",new ARGB(238, 130, 238)},
        {"orchid",new ARGB(218, 112, 214)},
        {"fuchsia",new ARGB(255, 0, 255)},
        {"magenta",new ARGB(255, 0, 255)},
        {"mediumorchid",new ARGB(186, 85, 211)},
        {"mediumpurple",new ARGB(147, 112, 219)},
        {"blueviolet",new ARGB(138, 43, 226)},
        {"darkviolet",new ARGB(148, 0, 211)},
        {"darkorchid",new ARGB(153, 50, 204)},
        {"darkmagenta",new ARGB(139, 0, 139)},
        {"purple",new ARGB(128, 0, 128)},
        {"indigo",new ARGB(75, 0, 130)},
        {"slateblue",new ARGB(106, 90, 205)},
        {"darkslateblue",new ARGB(72, 61, 139)},
        //Green color names
        {"greenyellow",new ARGB(173, 255, 47)},
        {"chartreuse",new ARGB(127, 255, 0)},
        {"lawngreen",new ARGB(124, 252, 0)},
        {"lime",new ARGB(0, 255, 0)},
        {"limegreen",new ARGB(50, 205, 50)},
        {"palegreen",new ARGB(152, 251, 152)},
        {"lightgreen",new ARGB(144, 238, 144)},
        {"mediumspringgreen",new ARGB(0, 250, 154)},
        {"springgreen",new ARGB(0, 255, 127)},
        {"mediumseagreen",new ARGB(60, 179, 113)},
        {"seagreen",new ARGB(46, 139, 87)},
        {"forestgreen",new ARGB(34, 139, 34)},
        {"green",new ARGB(0, 128, 0)},
        {"darkgreen",new ARGB(0, 100, 0)},
        {"yellowgreen",new ARGB(154, 205, 50)},
        {"olivedrab",new ARGB(107, 142, 35)},
        {"olive",new ARGB(128, 128, 0)},
        {"darkolivegreen",new ARGB(85, 107, 47)},
        {"mediumaquamarine",new ARGB(102, 205, 170)},
        {"darkseagreen",new ARGB(143, 188, 143)},
        {"lightseagreen",new ARGB(32, 178, 170)},
        {"darkcyan",new ARGB(0, 139, 139)},
        {"teal",new ARGB(0, 128, 128)},
        //Blue color names
        {"aqua",new ARGB(0, 255, 255)},
        {"cyan",new ARGB(0, 255, 255)},
        {"lightcyan",new ARGB(224, 255, 255)},
        {"paleturquoise",new ARGB(175, 238, 238)},
        {"aquamarine",new ARGB(127, 255, 212)},
        {"turquoise",new ARGB(64, 224, 208)},
        {"mediumturquoise",new ARGB(72, 209, 204)},
        {"darkturquoise",new ARGB(0, 206, 209)},
        {"cadetblue",new ARGB(95, 158, 160)},
        {"steelblue",new ARGB(70, 130, 180)},
        {"lightsteelblue",new ARGB(176, 196, 222)},
        {"powderblue",new ARGB(176, 224, 230)},
        {"lightblue",new ARGB(173, 216, 230)},
        {"skyblue",new ARGB(135, 206, 235)},
        {"lightskyblue",new ARGB(135, 206, 250)},
        {"deepskyblue",new ARGB(0, 191, 255)},
        {"dodgerblue",new ARGB(30, 144, 255)},
        {"cornflowerblue",new ARGB(100, 149, 237)},
        {"mediumslateblue",new ARGB(123, 104, 238)},
        {"royalblue",new ARGB(65, 105, 225)},
        {"blue",new ARGB(0, 0, 255)},
        {"mediumblue",new ARGB(0, 0, 205)},
        {"darkblue",new ARGB(0, 0, 139)},
        {"navy",new ARGB(0, 0, 128)},
        {"midnightblue",new ARGB(25, 25, 112)},
        //Brown color names
        {"cornsilk",new ARGB(255, 248, 220)},
        {"blanchedalmond",new ARGB(255, 235, 205)},
        {"bisque",new ARGB(255, 228, 196)},
        {"navajowhite",new ARGB(255, 222, 173)},
        {"wheat",new ARGB(245, 222, 179)},
        {"burlywood",new ARGB(222, 184, 135)},
        {"tan",new ARGB(210, 180, 140)},
        {"rosybrown",new ARGB(188, 143, 143)},
        {"sandybrown",new ARGB(244, 164, 96)},
        {"goldenrod",new ARGB(218, 165, 32)},
        {"darkgoldenrod",new ARGB(184, 134, 11)},
        {"peru",new ARGB(205, 133, 63)},
        {"chocolate",new ARGB(210, 105, 30)},
        {"saddlebrown",new ARGB(139, 69, 19)},
        {"sienna",new ARGB(160, 82, 45)},
        {"brown",new ARGB(165, 42, 42)},
        {"maroon",new ARGB(128, 0, 0)},
        //White color names
        {"white",new ARGB(255, 255, 255)},
        {"snow",new ARGB(255, 250, 250)},
        {"honeydew",new ARGB(240, 255, 240)},
        {"mintcream",new ARGB(245, 255, 250)},
        {"azure",new ARGB(240, 255, 255)},
        {"aliceblue",new ARGB(240, 248, 255)},
        {"ghostwhite",new ARGB(248, 248, 255)},
        {"whitesmoke",new ARGB(245, 245, 245)},
        {"seashell",new ARGB(255, 245, 238)},
        {"beige",new ARGB(245, 245, 220)},
        {"oldlace",new ARGB(253, 245, 230)},
        {"floralwhite",new ARGB(255, 250, 240)},
        {"ivory",new ARGB(255, 255, 240)},
        {"antiquewhite",new ARGB(250, 235, 215)},
        {"linen",new ARGB(250, 240, 230)},
        {"lavenderblush",new ARGB(255, 240, 245)},
        {"mistyrose",new ARGB(255, 228, 225)},
        //Grey color names
        {"gainsboro",new ARGB(220, 220, 220)},
        {"lightgrey",new ARGB(211, 211, 211)},
        {"silver",new ARGB(192, 192, 192)},
        {"darkgray",new ARGB(169, 169, 169)},
        {"gray",new ARGB(128, 128, 128)},
        {"dimgray",new ARGB(105, 105, 105)},
        {"lightslategray",new ARGB(119, 136, 153)},
        {"slategray",new ARGB(112, 128, 144)},
        {"darkslategray",new ARGB(47, 79, 79)},
        {"black",new ARGB(0, 0, 0)},
    };

    private static readonly Regex HexParser = new("^#([0-9a-f]{2})([0-9a-f]{2})([0-9a-f]{2})$");
    private static readonly Regex ShortHexParser = new("^#([0-9a-f]{1})([0-9a-f]{1})([0-9a-f]{1})$");

    public static ARGB Translate(string HTMLIdentifier)
    {
        var id = HTMLIdentifier.Trim().ToLowerInvariant();
        if (NAMED_COLOR_MAP.ContainsKey(id))
        {
            return NAMED_COLOR_MAP[id];
        }
        else
        {
            var m = HexParser.Match(id);
            if (m.Value == string.Empty)
            {
                m = ShortHexParser.Match(id);
                if (m.Value == string.Empty)
                {
                    throw new ArgumentException("Invalid HTML color");
                }
            }
            return new ARGB(
                Convert.ToInt32(m.Groups[1].Value.PadRight(2, m.Groups[1].Value[0]), 16),
                Convert.ToInt32(m.Groups[2].Value.PadRight(2, m.Groups[2].Value[0]), 16),
                Convert.ToInt32(m.Groups[3].Value.PadRight(2, m.Groups[3].Value[0]), 16));
        }
    }
}
