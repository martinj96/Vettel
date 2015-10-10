using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Core.Entities;
using Transpo.Core.Interfaces;

namespace Transpo.Infrastructure.Data.Repositories
{
    public class CriticalPointRepository : BaseRepository<CriticalPoint>, ICriticalPointRepository
    {
        public CriticalPointRepository(TranspoDbContext context)
            : base(context)
        {
        }
        public CriticalPoint getByLatLon(decimal lat, decimal lon)
        {
            return _context.CriticalPoints.FirstOrDefault(c => c.Latitude == lat && c.Longitude == lon);
        }
        public void AddOrderedCriticalPoint(OrderedCriticalPoint entity)
        {
            _context.CriticalPointsRides.Add(entity);
            _context.SaveChanges();
        }
    }
}
