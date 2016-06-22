using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpo.AppServices.DTOs
{
    public class OrderedCriticalPointDto
    {
        public int Order { get; set; }
        public int RideId { get; set; }
        public CriticalPointDto CriticalPoint { get; set; }
    }
}
