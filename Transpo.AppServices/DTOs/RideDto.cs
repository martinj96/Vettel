using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Core.Entities;

namespace Transpo.AppServices.DTOs
{
    public class RideDto
    {
        public ICollection<CriticalPointDto> CriticalPoints { get; set; }
        public DateTime Departure { get; set; }
        public User Driver { get; set; }
    }
}
