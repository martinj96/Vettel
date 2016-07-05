﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpo.AppServices.DTOs
{
    public class LoginDto
    {
        public string Name { get; set; }
        public string AccessToken { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Link { get; set; }
        public string FacebookId { get; set; }
        public string AppUserId { get; set; }
        public string PictureUrl { get; set; }
        public int? Age { get; set; }
        public string Phone { get; set; }
    }
}
