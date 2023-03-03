using System.ComponentModel;

namespace Vonage.Messages.Viber;

public enum ViberMessageCategory
{
    [Description("transaction")] Transaction = 0,

    [Description("promotion")] Promotion = 1,
}