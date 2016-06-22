using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Infrastructure.Data.Entities;
using Transpo.Infrastructure.Data.Interfaces;

namespace Transpo.Infrastructure.Data.Repositories
{
    public class OrderedCriticalPointRepository : BaseRepository<OrderedCriticalPoint>, IOrderedCriticalPointRepository
    {
        public OrderedCriticalPointRepository(TranspoDbContext context)
            : base(context)
        {
        }
        public List<OrderedCriticalPoint> GetCriticalPointsByRideId(int rideId){
            return _context.OrderedCriticalPoints.Where(cp => cp.RideId == rideId).ToList();
        }
    }
}
