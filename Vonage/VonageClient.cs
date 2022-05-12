using Vonage.Accounts;
using Vonage.Voice;
using Vonage.Applications;
using Vonage.Conversions;
using Vonage.Messages;
using Vonage.Numbers;
using Vonage.NumberInsights;
using Vonage.Verify;
using Vonage.ShortCodes;
using Vonage.Redaction;
using Vonage.Messaging;
using Vonage.Request;
using Vonage.Pricing;

namespace Vonage
{
    public class VonageClient
    {
        private Credentials _credentials;
        
        public Credentials Credentials {
            get => _credentials;
            set
            {
                _credentials = value;
                PropagateCredentials();
            }
        }
        
        public IAccountClient AccountClient { get; private set; }
        
        public IApplicationClient ApplicationClient { get; private set; }
        
        public IVoiceClient VoiceClient { get; private set; }
        
        public IConversionClient ConversionClient { get; private set; }
        
        public INumbersClient NumbersClient { get; private set; }
        
        public INumberInsightClient NumberInsightClient { get; private set; }
        
        public IVerifyClient VerifyClient { get; private set; }
        
        public IShortCodesClient ShortCodesClient { get; private set; }
        
        public IRedactClient RedactClient { get; private set; }
        
        public ISmsClient SmsClient { get; private set; }

        public IPricingClient PricingClient { get; private set; }

        public IMessagesClient MessagesClient { get; private set; }

        public VonageClient(Credentials credentials)
        {
            Credentials = credentials;
        }

        private void PropagateCredentials()
        {
            AccountClient = new AccountClient(Credentials);
            ApplicationClient = new ApplicationClient(Credentials);
            VoiceClient = new VoiceClient(Credentials);
            ConversionClient = new ConversionClient(Credentials);
            NumbersClient = new NumbersClient(Credentials);
            NumberInsightClient = new NumberInsightClient(Credentials);
            VerifyClient = new VerifyClient(Credentials);
            ShortCodesClient = new ShortCodesClient(Credentials);
            RedactClient = new RedactClient(Credentials);
            SmsClient = new SmsClient(Credentials);
            PricingClient = new PricingClient(Credentials);
            MessagesClient = new MessagesClient(Credentials);
        }
        
    }
}