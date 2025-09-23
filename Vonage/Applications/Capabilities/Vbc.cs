namespace Vonage.Applications.Capabilities;

public class Vbc : Capability
{
    public Vbc()
    {
        this.Type = CapabilityType.Vbc;
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public static Vbc Build() => new Vbc();
}