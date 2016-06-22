using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpo.Infrastructure.Data.Entities
{
    public class Car : BaseEntity
    {
        public int Comfort { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public virtual User User { get; set; }
    }
}
