using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpo.Infrastructure.Data.Entities
{
    public class Review : BaseEntity
    {
        public string Text { get; set; }
        public int ReviewerId { get; set; }
        public int RevieweeId { get; set; }
        public virtual User Reviewer { get; set; }
        public virtual User Reviewee { get; set; }

    }
}
