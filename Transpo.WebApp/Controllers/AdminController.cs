using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Email(String content, String subject)
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