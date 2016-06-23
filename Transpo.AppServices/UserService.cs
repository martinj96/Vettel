using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.AppServices.DTOs;
using Transpo.Infrastructure.Data.Entities;
using Transpo.Infrastructure.Data.Interfaces;

namespace Transpo.AppServices
{
    public enum Gender
    {
        Male = 1,
        Female
    }
    public class UserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        //public bool Exists(long facebookId)
        //{
        //    var user = _userRepository.GetUserByFacebookId(facebookId);
        //    if (user == null)
        //        return false;
        //    return true;
        //}

        public User CreateUser(LoginDto u)
        {
            User user = new User();
            user.Email = u.Email;
            if (u.Gender == "male")
                user.Gender = (int)Gender.Male;
            else
                user.Gender = (int)Gender.Female;
            user.Name = u.Name;
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

        //public User GetUserByFacebookId(long id)
        //{
        //    return _userRepository.GetUserByFacebookId(id);
        //}

        //public void UpdateProfilePicture(string pictureUrl, long facebookId)
        //{
        //    var user = GetUserByFacebookId(facebookId);
        //    if (user != null)
        //    {
        //        _userRepository.Edit(user);
        //        user.PictureUrl = pictureUrl;
        //        _userRepository.Save();
        //    }
        //}
    }
}
