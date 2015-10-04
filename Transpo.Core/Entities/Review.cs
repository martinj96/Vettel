using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpo.Core.Entities
{
    public class Review : BaseEntity
    {
        public string Text { get; set; }
        public virtual User Reviewer { get; set; }
        public virtual User Reviewee { get; set; }

    }
}
