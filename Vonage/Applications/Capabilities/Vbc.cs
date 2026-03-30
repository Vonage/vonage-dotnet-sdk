namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents the VBC (Vonage Business Communications) capability. Enables zero-rated calls for VBC number
///     programmability service applications. This capability has no configuration options.
/// </summary>
public class Vbc : Capability
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Vbc" /> class.
    /// </summary>
    public Vbc()
    {
        this.Type = CapabilityType.Vbc;
    }

    /// <summary>
    ///     Creates a new VBC capability builder.
    /// </summary>
    /// <returns>A new VBC capability instance.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var vbcCapability = Vbc.Build();
    /// ]]></code>
    /// </example>
    public static Vbc Build() => new Vbc();
}