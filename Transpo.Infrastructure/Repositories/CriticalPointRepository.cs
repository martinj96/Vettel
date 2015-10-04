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
    }
}
