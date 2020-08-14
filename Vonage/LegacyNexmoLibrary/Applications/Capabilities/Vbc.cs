namespace Nexmo.Api.Applications.Capabilities
{
    [System.Obsolete("The Nexmo.Api.Applications.Capabilities.Vbc class is obsolete. " +
        "References to it should be switched to the new Vonage.Applications.Capabilities.Vbc class.")]
    public class Vbc : Capability 
    {
        public Vbc()
        {
            Type = CapabilityType.Vbc;
        }
    }
}