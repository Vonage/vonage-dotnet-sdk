using Nexmo.Api.Voice;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nexmo.Api.Test.Integration
{
    [TestClass]
    public class CallTest
	{
        [TestMethod]
        public void should_call()
        {
            var results = Call.Do(new Call.CallCommand
            {
                to = new[]
                {
                    new Call.Endpoint
                    {
                        type = "phone",
                        number = Configuration.Instance.Settings["test_number"]
                    }
                },
                from = new Call.Endpoint
                {
                    type = "phone",
                    number = Configuration.Instance.Settings["nexmo_number"]
                },
                answer_url = new[]
                {
                    "https://nexmo-community.github.io/ncco-examples/first_call_talk.json"
                }
            });
            Assert.AreEqual("started", results.status);
        }

        [TestMethod]
        public void should_get_calls()
        {
            var results = Call.List();
            Assert.AreEqual(3, results._embedded.calls.Count);
        }

        [TestMethod]
        public void should_get_specified_call()
        {
            var id = "ffffffff-ffff-ffff-ffff-ffffffffffff";
            var results = Call.Get(id);
            Assert.AreEqual(id, results.uuid);
        }

        [TestMethod]
        public void should_edit_specified_call()
        {
            var id = "ffffffff-ffff-ffff-ffff-ffffffffffff";
            var results = Call.Edit(id, new Call.CallEditCommand
            {
                Action = "hangup"
            });
            Assert.AreEqual(id, results.uuid);
        }

        [TestMethod]
        public void should_call_then_stream_then_end_stream()
        {
            var call = Call.Do(new Call.CallCommand
            {
                to = new[]
                {
                    new Call.Endpoint
                    {
                        type = "phone",
                        number = Configuration.Instance.Settings["test_number"]
                    }
                },
                from = new Call.Endpoint
                {
                    type = "phone",
                    number = Configuration.Instance.Settings["nexmo_number"]
                },
                answer_url = new[]
                {
                    "https://nexmo-community.github.io/ncco-examples/first_call_talk.json"
                }
            });
            Assert.AreEqual("started", call.status);

            // need to look up the call id based on the conversation id
            var calls = Call.List(new Call.SearchFilter
            {
                conversation_uuid = call.conversation_uuid,
                page_size = 10
            });
            var callId = calls._embedded.calls[0].uuid;

            // test streaming
            var streamCmdResponse = Call.BeginStream(callId, new Call.StreamCommand
            {
                stream_url = new[]
                {
                    "https://nexmo-community.github.io/ncco-examples/assets/voice_api_audio_streaming.mp3"
                },
                loop = 0
            });
            Assert.AreEqual("Stream started", streamCmdResponse.message);

            System.Threading.Thread.Sleep(1000);

            streamCmdResponse = Call.EndStream(callId);
            Assert.AreEqual("Stream stopped", streamCmdResponse.message);
        }

        [TestMethod]
        public void should_call_then_send_dtmf()
        {
            var callId = "ffffffff-ffff-ffff-ffff-ffffffffffff";
            // TODO: need to look up the call id based on the conversation id
            var dtmfResponse = Call.SendDtmf(callId, new Call.DtmfCommand
            {
                digits = "24681357"
            });
            Assert.AreEqual("DTMF sent", dtmfResponse.message);
        }

        [TestMethod]
        public void should_call_then_send_and_end_talk()
        {
            var callId = "ffffffff-ffff-ffff-ffff-ffffffffffff";
            // TODO: need to look up the call id based on the conversation id
            var talkResponse = Call.BeginTalk(callId, new Call.TalkCommand
            {
                text = "Four score and seven years ago our fathers brought forth, on this continent, a new nation, conceived in Liberty, and dedicated to the proposition that all men are created equal. Now we are engaged in a great civil war, testing whether that nation, or any nation so conceived and so dedicated, can long endure. We are met on a great battle - field of that war. We have come to dedicate a portion of that field, as a final resting place for those who here gave their lives that that nation might live. It is altogether fitting and proper that we should do this. But, in a larger sense, we can not dedicate — we can not consecrate — we can not hallow — this ground. The brave men, living and dead, who struggled here, have consecrated it, far above our poor power to add or detract. The world will little note, nor long remember what we say here, but it can never forget what they did here. It is for us the living, rather, to be dedicated here to the unfinished work which they who fought here have thus far so nobly advanced. It is rather for us to be here dedicated to the great task remaining before us — that from these honored dead we take increased devotion to that cause for which they gave the last full measure of devotion — that we here highly resolve that these dead shall not have died in vain — that this nation, under God, shall have a new birth of freedom — and that government of the people, by the people, for the people, shall not perish from the earth.",
                voice_name = "Salli",
                loop = 1
            });
            Assert.AreEqual("Talk started", talkResponse.message);

            System.Threading.Thread.Sleep(1000);

            talkResponse = Call.EndTalk(callId);
            Assert.AreEqual("Talk stopped", talkResponse.message);
        }
    }
}