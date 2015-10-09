using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transpo.WebApp.Models
{
    public class SearchResultModel : SearchModel
    {
        public IEnumerable<RideModel> Rides { get; set; }

        public SearchResultModel(SearchModel searchModel)
        {
            this.FromCityName = searchModel.FromCityName;
            this.FromCountryShortCode = searchModel.FromCountryShortCode;
            this.FromLatitude = searchModel.FromLatitude;
            this.FromLongitude = searchModel.FromLongitude;

            this.ToCityName = searchModel.ToCityName;
            this.ToCountryShortCode = searchModel.ToCountryShortCode;
            this.ToLatitude = searchModel.ToLatitude;
            this.ToLongitude = searchModel.ToLongitude;
        }

        public SearchResultModel() 
        {
            Rides = new List<RideModel>();
        }
    }
}