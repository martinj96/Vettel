using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transpo.Core.Entities;

namespace Transpo.WebApp.Models
{
    public class UserViewModel
    {
        public UserViewModel(User u)
        {
            Name = u.Name;
            if (u.Gender == (int)Transpo.AppServices.Gender.male)
                Gender = "Male";
            else
                Gender = "Female";
            Email = u.Email;
            PictureUrl = u.PictureUrl;
            Link = u.Link;

        }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PictureUrl { get; set; }
        public string Link { get; set; }
        // public string Phone { get; set; }
        // public decimal Rating { get; set; }
        // public Car Car { get; set; }
        // public ICollection<Characteristic> Characteristics { get; set; }
    }
}