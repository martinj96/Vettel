using System;
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
        public long FacebookId { get; set; }
    }
}
