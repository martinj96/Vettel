using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transpo.Models
{
    public class UserViewModel
    {
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
