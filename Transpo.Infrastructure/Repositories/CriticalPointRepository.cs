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
            lat = decimal.Round(lat, 5);
            lon = decimal.Round(lon, 5);
            return _context.CriticalPoints.FirstOrDefault(c => c.Latitude.CompareTo(lat) == 0 && c.Longitude.CompareTo(lon) == 0);
        }
    }
}
