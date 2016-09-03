using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Transpo.API.Controllers
{
    public class RidesController : BaseApiController
    {
        [Route("rides/myrides")]
        [HttpGet]
        public IHttpActionResult MyRides()
        {
            var rides = Service.GetRideService();
            var r = rides.GetAllRides().Last();
            return Ok(r.DateCreated);
        }
    }
}