using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Transpo.API.Filters;
using Transpo.API.Models;

namespace Transpo.API.Controllers
{
    public class SearchController : BaseApiController
    {
        [Route("search/index")]
        [HttpGet]
        public IHttpActionResult Index()
        {
            return Ok(new SearchResultModel());
        }

        [Route("search/perform")]
        [HttpGet]
        public IHttpActionResult PerformSearch([FromUri] SearchModel searchModel)
        {
            var model = new SearchResultModel(searchModel);
            return Ok(model);
        }

        [Route("search/performpartial")]
        [HttpGet]
        public IHttpActionResult PerformSearchPartial([FromUri] SearchModel searchModel)
        {
            var model = new SearchResultModel(searchModel);
            return Ok(model);
        }

        [Route("search/all")]
        [HttpGet]
        public IHttpActionResult GetAllRides()
        {
            var model = new SearchResultModel();
            return Ok(model);

        }

        //private IHttpActionResult MockRides()
        //{
        //    List<RideModel> rides = new List<RideModel>();

        //    var lar = Service.GetRideService().GetById(20);
        //    var mockride = new RideModel(lar);
        //    rides.Add(mockride);
        //    return Ok(rides);
        //}
    }
}