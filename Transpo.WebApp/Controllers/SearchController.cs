using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transpo.WebApp.Models;

namespace Transpo.WebApp.Controllers
{   
    public class SearchController : BaseController
    {
        // GET: Search
        public ActionResult Index()
        {
            return View(new SearchResultModel());
        }

        public ActionResult PerformSearch(SearchModel searchModel)
        {
            var model = new SearchResultModel(searchModel);
            return View("Index", model);
        }

        public ActionResult PerformSearchPartial(SearchModel searchModel)
        {
            var model = new SearchResultModel(searchModel);
            return PartialView("_SearchResultsGrid", model.Rides);
        }

        private IEnumerable<RideModel> MockRides()
        {
            List<RideModel> rides = new List<RideModel>();

            var lar = _rideService.GetById(20);
            var mockride = new RideModel(lar);
            rides.Add(mockride);
            return rides;
        }
    }
}