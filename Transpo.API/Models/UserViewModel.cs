using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transpo.Infrastructure.Data.Entities;
using Transpo.Infrastructure.Data.Identity;

namespace Transpo.API.Models
{
    public class UserViewModel
    {
        public UserViewModel(User u)
        {
            Name = u.Name;
            Link = u.Link;
            Email = u.Email;
            UserId = u.id;
            FacebookId = u.FacebookId;
            PictureUrl = u.PictureUrl;
            Phone = u.Phone;
            if (u.Gender.HasValue)
            {
                if (u.Gender == (int)AppServices.Gender.Male)
                    Gender = "Машко";
                else if (u.Gender == (int)AppServices.Gender.Female)
                    Gender = "Женско";
            }
            Age = u.Age;
        }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Link { get; set; }
        public string FacebookId { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public string Phone { get; set; }
        private string _pictureUrl;
        public string PictureUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_pictureUrl))
                {
                    return "~/Images/no_image.png";
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
