using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.API.Models
{
    public class ApiIdentity : IIdentity
    {
        public User User
        {
            get;
            private set;
        }

        public ApiIdentity(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            User = user;
            Name = user.Name;
        }

        public string AuthenticationType
        {
            get
            {
                return "Basic";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get; private set;
        }
    }
}