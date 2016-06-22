// XMLsample.cs
// compile with: /doc:XMLsample.xml
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.Infrastructure.Data.Facades
{
  
    /// <summary>
    /// This is the interface of the application for 
    /// authenticated user.
    /// 
    /// </summary>
    interface IUserInterface
    {
        /// <summmary>
        /// Calling this method will result with collection of rides
        /// that pass the route specified with the parametars.
        /// </summary>
        /// <param name="from">The starting point of the route.</param>
        /// <param name="to">The ending point of the route</param>
        /// <returns>Collection of riddes that pass the asked route.</returns>
        ICollection<Ride> GetRides(CriticalPoint from, CriticalPoint to);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Your id.</returns>
        int GetMyID();
    }
}
