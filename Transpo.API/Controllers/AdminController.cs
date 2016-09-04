using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseApiController
    {
        // GET: Admin
        public IHttpActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IHttpActionResult Email(String content, String subject)
        {
            List<User> users = Service.GetUserService().GetAllActiveUsers();
            foreach (User user in users)
            {
                UserManager.SendEmailAsync(user.id.ToString(), subject, content);
            }
            return RedirectToAction("Index", "Email");
        }
    }
}