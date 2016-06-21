using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Core.Entities;

namespace Transpo.Core.Facades
{
    class StandardUserInterface : UserInterface
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
