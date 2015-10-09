using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transpo.WebApp.Models;

namespace Transpo.WebApp.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View(new SearchResultModel());
        }

        public ActionResult PerformSearch(SearchModel searchModel)
        {
            var model = new SearchResultModel(searchModel);
            model.Rides = MockRides();
            return View("Index", model);
        }

        private IEnumerable<RideModel> MockRides()
        {
            List<RideModel> rides = new List<RideModel>();

            rides.Add(new RideModel());
            rides.Add(new RideModel());
            rides.Add(new RideModel());
            rides.Add(new RideModel());
            rides.Add(new RideModel());

            return rides;
        }
    }
}