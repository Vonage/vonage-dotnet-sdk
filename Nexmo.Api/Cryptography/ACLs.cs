using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Cryptography
{
    [JsonConverter(typeof(AclConverter))]
    public class ACLs
    {
        [JsonProperty("paths")]
        public IList<Acl> Paths { get; set; } = new List<Acl>();
        public ACLs()
        {
            Paths.Add(new Acl() { Service = "users" });
            Paths.Add(new Acl() { Service = "conversations" });
            Paths.Add(new Acl() { Service = "sessions" });
            Paths.Add(new Acl() { Service = "devices" });
            Paths.Add(new Acl() { Service = "image" });
            Paths.Add(new Acl() { Service = "media" });
            Paths.Add(new Acl() { Service = "applications" });
            Paths.Add(new Acl() { Service = "push" });
            Paths.Add(new Acl() { Service = "knocking" });
        }
        public void AddServices(List<Acl> acls)
        {
            foreach (var acl in acls)
            {                
                Paths.Add(acl);
            }
            
        }
        public void AddService(Acl acl)
        {
            AddServices(new List<Acl>() { acl });
        }
        public void RemoveServices(IList<string> services)
        {            
            foreach(var service in services)
            {
                Paths = Paths.Where(x => x.Service != service).ToList();
            }
        }
        public void RemoveService(string service)
        {
            RemoveServices(new List<string>() { service });
        }
    }
}
