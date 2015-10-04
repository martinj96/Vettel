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
        private ICarRepository _carRepository;
        public UserService(IUserRepository userRepository, ICarRepository carRepository)
        {
            _userRepository = userRepository;
            _carRepository = carRepository;
        }
        public User AddUser(UserDto u){
            User user = new User();
            user.Email = u.Email;
            user.Gender = u.Gender;
            user.Name = u.Name;
            user.Phone = u.Phone;
            _userRepository.Add(user);
            _userRepository.Save();
            return user;
        }
    }
}
