﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transpo.WebApp.Models
{
    public class SearchModel
    {
        public double FromLongitude { get; set; }
        public double FromLatitude { get; set; }
        public string FromCountryShortCode { get; set; }
        public string FromCityName { get; set; }
        public double ToLongitude { get; set; }
        public double ToLatitude { get; set; }
        public string ToCountryShortCode { get; set; }
        public string ToCityName { get; set; }
    }
}