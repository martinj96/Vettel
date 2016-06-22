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
            if (u.Gender == (int)AppServices.Gender.male)
                Gender = "Male";
            else
                Gender = "Female";
        }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Link { get; set; }
        public string Email { get; set; }
        public string PictureUrl { get; set; }
        public int UserId { get; set; }
    }
}
