using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpo.Core.Entities
{
    public class Car : BaseEntity
    {
        public int Comfort { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
    }
}
