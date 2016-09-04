//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Http;
//using Transpo.WebApp.Models;

//namespace Transpo.API.Controllers
//{   
//    public class SearchController : BaseApiController
//    {
//        [Route("api/search/index")]
//        [HttpGet]
//        public IHttpActionResult Index()
//        {
//            return Ok(new SearchResultModel());
//        }

//        [Route("api/search/perform")]
//        [HttpGet]
//        public IHttpActionResult PerformSearch([FromBody] SearchModel searchModel)
//        {
//            var model = new SearchResultModel(searchModel);
//            return Ok(model);
//        }

//        [Route("api/search/performpartial")]
//        [HttpGet]
//        public IHttpActionResult PerformSearchPartial([FromBody] SearchModel searchModel)
//        {
//            var model = new SearchResultModel(searchModel);
//            return Ok(model);
//        }

//        [Route("api/search/all")]
//        [HttpGet]
//        public IHttpActionResult GetAllRides()
//        {
//            var model = new SearchResultModel();
//            return Ok(model);

//        }

//        //private IHttpActionResult MockRides()
//        //{
//        //    List<RideModel> rides = new List<RideModel>();

//        //    var lar = Service.GetRideService().GetById(20);
//        //    var mockride = new RideModel(lar);
//        //    rides.Add(mockride);
//        //    return Ok(rides);
//        //}
//    }
//}