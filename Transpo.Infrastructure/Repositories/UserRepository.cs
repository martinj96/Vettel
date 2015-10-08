using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Core.Entities;
using Transpo.Core.Interfaces;

namespace Transpo.Infrastructure.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TranspoDbContext context)
            : base(context)
        {
        }

        public User GetUserByFacebookId(long facebookId){
            return _context.Users.FirstOrDefault(u => u.FacebookId == facebookId);
        }
    
    }
}
