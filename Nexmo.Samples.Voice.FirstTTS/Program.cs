using System;
using System.Net;
using Nexmo.Api;
using Nexmo.Api.Voice;

namespace Nexmo.Samples.Voice.FirstTTS
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // https://github.com/dotnet/corefx/issues/4476
            // https://github.com/dotnet/corefx/issues/7623
#if net452
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
#endif

            // Set up some variables
            var applicationId = Configuration.Instance.Settings["Nexmo.Application.Id"];
            const string phoneNumberToCall = "15555551212";

            // Make a TTS Call to a phone number

            Console.WriteLine($"Using application ID {applicationId} to call {phoneNumberToCall}");
            Console.WriteLine();
            Console.WriteLine($"Generate a JWT for {applicationId}");
            Console.WriteLine();

            var response = Call.Do(new Call.CallCommand
            {
                to = new[]
                {
                    new Call.Endpoint {
                        type = "phone",
                        number = phoneNumberToCall
                    }
                },
                from = new Call.Endpoint
                {
                    type = "phone",
                    number = "15554443333"
                },
                answer_url = new[]
                {
                    "https://nexmo-community.github.io/ncco-examples/first_call_talk.json"
                }
            });

            //Console.WriteLine(response.ErrorText);
        }
    }
}
