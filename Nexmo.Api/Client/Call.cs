using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api.ClientMethods
{
    public class Call
    {
        public Credentials Credentials { get; set; }

        public Call(Credentials creds)
        {
            Credentials = creds;
        }

        /// <summary>
        /// POST /v1/calls - create an outbound SIP or PSTN Call
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public Voice.Call.CallResponse Do(Voice.Call.CallCommand cmd, Credentials creds = null)
        {
            return Voice.Call.Do(cmd, creds ?? Credentials);
        }

        /// <summary>
        /// GET /v1/calls - retrieve information about all your Calls
        /// <param name="filter">Filter to search calls on</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// </summary>
        public PaginatedResponse<Voice.Call.CallList> List(Voice.Call.SearchFilter filter, Credentials creds = null)
        {
            return Voice.Call.List(filter, creds ?? Credentials);
        }

        public PaginatedResponse<Voice.Call.CallList> List(Credentials creds = null)
        {
            return Voice.Call.List(new Voice.Call.SearchFilter
                {
                    page_size = 10
                }, creds ?? Credentials);
        }

        /// <summary>
        /// GET /v1/calls/{uuid} - retrieve information about a single Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public Voice.Call.CallResponse Get(string id, Credentials creds = null)
        {
            return Voice.Call.Get(id, creds ?? Credentials);
        }

        /// <summary>
        /// PUT /v1/calls/{uuid} - modify an existing Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public Voice.Call.CallResponse Edit(string id, Voice.Call.CallEditCommand cmd, Credentials creds = null)
        {
            return Voice.Call.Edit(id, cmd, creds ?? Credentials);
        }

        #region Stream

        /// <summary>
        /// PUT /v1/calls/{uuid}/stream - stream an audio file to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public Voice.Call.CallCommandResponse BeginStream(string id, Voice.Call.StreamCommand cmd, Credentials creds = null)
        {
            return Voice.Call.BeginStream(id, cmd, creds ?? Credentials);
        }

        /// <summary>
        /// DELETE /v1/calls/{uuid}/stream - stop streaming an audio file to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public Voice.Call.CallCommandResponse EndStream(string id, Credentials creds = null)
        {
            return Voice.Call.EndStream(id, creds ?? Credentials);
        }

        #endregion

        #region Talk
        /// <summary>
        /// PUT /v1/calls/{uuid}/talk - send a synthesized speech message to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public  Voice.Call.CallCommandResponse BeginTalk(string id, Voice.Call.TalkCommand cmd, Credentials creds = null)
        {
            return Voice.Call.BeginTalk(id, cmd, creds ?? Credentials);
        }

        /// <summary>
        /// DELETE /v1/calls/{uuid}/talk - stop sending a synthesized speech message to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public Voice.Call.CallCommandResponse EndTalk(string id, Credentials creds = null)
        {
            return Voice.Call.EndTalk(id, creds ?? Credentials);
        }

        /// <summary>
        /// PUT /v1/calls/{uuid}/dtmf - send Dual-tone multi-frequency(DTMF) tones to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public Voice.Call.CallCommandResponse SendDtmf(string id, Voice.Call.DtmfCommand cmd, Credentials creds = null)
        {
            return Voice.Call.SendDtmf(id, cmd, creds ?? Credentials);
        }
        #endregion
    }
}
