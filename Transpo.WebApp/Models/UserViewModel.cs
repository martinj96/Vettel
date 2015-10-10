using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transpo.Core.Entities;

namespace Transpo.WebApp.Models
{
    public class UserViewModel
    {
        public UserViewModel(User u)
        {
            Name = u.Name;
            Link = u.Link;
            Email = u.Email;
            PictureUrl = u.PictureUrl;
            if (u.Gender == (int)Transpo.AppServices.Gender.male)
                Gender = "Male";
            else
                Gender = "Female";
        }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Link { get; set; }
        public string Email { get; set; }
        public string PictureUrl { get; set; }
    }
}
