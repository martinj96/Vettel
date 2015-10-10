using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Core.Entities;

namespace Transpo.AppServices.DTOs
{
    public class CriticalPointDto
    {
        public CriticalPointDto()
        {

        }
        public CriticalPointDto(OrderedCriticalPoint point)
        {
            Latitude = point.CriticalPoint.Latitude;
            Longitude = point.CriticalPoint.Longitude;
            Name = point.CriticalPoint.Name;
        }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Name { get; set; }
    }
}
