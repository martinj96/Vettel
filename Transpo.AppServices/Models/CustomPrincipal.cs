using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using Transpo.Infrastructure.Data;

namespace Transpo.AppServices.Models
{
    public class CustomPrincipal : IPrincipal
    {
        public int UserId { get; set; }
        public long FacebookId { get; set; }
        public string Name { get; set; }
        public IIdentity Identity { get; private set; }
        public CustomPrincipal(int UserId, long FacebookId, string Name)
        {
            this.UserId = UserId;
            this.FacebookId = FacebookId;
            this.Name = Name;
            this.Identity = new GenericIdentity(Name);
        }
        public bool IsInRole(string role)
        {
            return true;
        }
    }
    public class CustomPrincipalSerializedModel
    {
        public int UserId { get; set; }

        public long FacebookId { get; set; }

        public string Name { get; set; }

    }
}
