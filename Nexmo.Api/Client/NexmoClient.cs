using Nexmo.Api.Accounts;
using Nexmo.Api.Voice;
using Nexmo.Api.Applications;
using Nexmo.Api.Conversions;
using Nexmo.Api.Numbers;
using Nexmo.Api.NumberInsights;
using Nexmo.Api.Verify;
using Nexmo.Api.ShortCodes;
using Nexmo.Api.Redaction;
using Nexmo.Api.Messaging;
using Nexmo.Api.Request;
using Nexmo.Api.Pricing;
using Nexmo.Api.MessageSearch;

namespace Nexmo.Api
{
    public class NexmoClient
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

        public IMessageSearchClient MessageSearchClient { get; set; }

        public NexmoClient(Credentials credentials)
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
            MessageSearchClient = new MessageSearchClient(Credentials);
        }
        
    }
}