using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Transpo.Models
{
    public class SearchResultModel : SearchModel
    {
        public IEnumerable<RideModel> Rides { get; set; }
    }
}