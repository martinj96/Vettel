using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Infrastructure.Data.Entities;
using Transpo.Infrastructure.Data.Interfaces;

namespace Transpo.Infrastructure.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TranspoDbContext context)
            : base(context)
        {

        }

        //public User GetUserByFacebookId(long facebookId){
        //    return _context.UsersInfo.FirstOrDefault(u => u.FacebookId == facebookId);
        //}
    
    }
}
