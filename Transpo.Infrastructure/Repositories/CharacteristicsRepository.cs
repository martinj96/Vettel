using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Infrastructure.Data.Entities;
using Transpo.Infrastructure.Data.Interfaces;

namespace Transpo.Infrastructure.Data.Repositories
{
    public class CharacteristicRepository : BaseRepository<Characteristic>, ICharacteristicRepository
    {
        public CharacteristicRepository(TranspoDbContext context)
            : base(context)
        {

        }
    }
}
