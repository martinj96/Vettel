using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.Infrastructure.Data.Facades
{
    class StandardUserInterface : IUserInterface
    {
        private User user;
        
        public StandardUserInterface(User user) 
        {
            this.user = user;
        }

        public int GetMyID()
        {
            return user.id;
        }

        public ICollection<Ride> GetRides(CriticalPoint from, CriticalPoint to)
        {
            throw new NotImplementedException();
        }
    }
}
