using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Cryptography
{
    
    public class Acl
    {
        public enum PathDepth
        {
            NoDepth,
            FirstChild,
            FullDepth
        }

        public enum Verbs
        {
            GET,
            POST,
            PUT,
            PATCH,
            DELETE
        }

        public List<Verbs> BlockedHttpVerbs { get; set; } = new List<Verbs>();
        public string ApiVersion { get; set; } = "*";
        public PathDepth AllowedDepth { get; set; } = PathDepth.FullDepth;
        public string Service { get; set; }
        public string FirstDepthAllowance { get; set; } = "*";
        public string SecondDepthAllowance { get; set; } = "*";
    }

    
    public class Acl <T> : Acl where T: class
    {        
        public T filters { get; set; }
    }
}
