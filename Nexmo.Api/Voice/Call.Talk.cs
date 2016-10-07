using System;

namespace Nexmo.Api.Voice
{
    public static partial class Call
    {
        /// <summary>
        /// PUT /v1/calls/{uuid}/talk - send a synthesized speech message to an active Call
        /// </summary>
        public static CallResponse BeginTalk()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// DELETE /v1/calls/{uuid}/talk - stop sending a synthesized speech message to an active Call
        /// </summary>
        public static CallResponse EndTalk()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// PUT /v1/calls/{uuid}/dtmf - send Dual-tone multi-frequency(DTMF) tones to an active Call
        /// </summary>
        public static CallResponse SendDtmf()
        {
            throw new NotImplementedException();
        }
    }
}