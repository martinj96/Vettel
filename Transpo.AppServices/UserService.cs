using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.AppServices.DTOs;
using Transpo.Core.Entities;
using Transpo.Core.Interfaces;

namespace Transpo.AppServices
{
    public class UserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public bool Exists(long facebookId)
        {
            var user = _userRepository.GetUserByFacebookId(facebookId);
            if (user == null)
                return false;
            return true;
        }
        public User CreateUser(UserDto u)
        {
            User user = new User();
            user.Email = u.Email;
            user.Gender = u.Gender;
            user.Name = u.Name;
            user.Surname = u.Surname;
            user.Phone = u.Phone;
            user.Link = u.Link;
            user.FacebookId = u.FacebookId;
            _userRepository.Add(user);
            _userRepository.Save();
            return user;
        }
        public User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }
        public User GetUserByFacebookId(long id)
        {
            return _userRepository.GetUserByFacebookId(id);
        }
    }
}
