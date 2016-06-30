using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transpo.Infrastructure.Data.Entities;
using Transpo.Infrastructure.Data.Identity;

namespace Transpo.WebApp.Models
{
    public class UserViewModel
    {
        public UserViewModel(User u)
        {
            Name = u.Name;
            Link = u.Link;
            Email = u.Email;
            UserId = u.id;
            PictureUrl = u.PictureUrl;
            if (u.Gender.HasValue)
            {
                if (u.Gender == (int)AppServices.Gender.Male)
                    Gender = "Male";
                else if (u.Gender == (int)AppServices.Gender.Female)
                    Gender = "Female";
            }
        }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Link { get; set; }
        public string Email { get; set; }

        private string _pictureUrl;
        public string PictureUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_pictureUrl))
                {
                    return "Images/no_image.png";
                }
                return _pictureUrl;
            }
            set
            {
                _pictureUrl = value;
            }
        }
        public int UserId { get; set; }
    }
}
