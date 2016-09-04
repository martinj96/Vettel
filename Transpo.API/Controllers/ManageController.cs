//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security;
//using Transpo.WebApp.Models;
//using Transpo.Infrastructure.Data.Identity;
//using Transpo.AppServices.DTOs;
//using System.Net;
//using System.Diagnostics;
//using System.Web.Http;
//using Transpo.API.Filters;
//using System.Collections.Generic;

//namespace Transpo.API.Controllers
//{
//    [BasicAuthentication]
//    public class ManageController : BaseApiController
//    {
//        public ManageController()
//        {
//        }

//        //
//        // GET: /Manage/Index
//        [Route("api/manage/index")]
//        [HttpGet]
//        public async Task<IHttpActionResult> Index()
//        {
//            var userId = User.Identity.GetUserId();
//            var model = new IndexViewModel
//            {
//                HasPassword = HasPassword(),
//                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
//                Logins = await UserManager.GetLoginsAsync(userId),
//                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
//            };
//            return Ok(model);
//        }

//        //
//        // POST: /Manage/RemoveLogin
//        [Route("api/manage/removelogin")]
//        [HttpPost]
//        public async Task<IHttpActionResult> RemoveLogin([FromBody] string loginProvider, [FromBody] string providerKey)
//        {
//            ManageMessageId? message;
//            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
//            if (result.Succeeded)
//            {
//                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
//                if (user != null)
//                {
//                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
//                }
//                message = ManageMessageId.RemoveLoginSuccess;
//            }
//            else
//            {
//                message = ManageMessageId.Error;
//            }
//            return Ok();
//        }

//        //
//        // POST: /Manage/ChangePassword
//        [Route("api/manage/changepassword")]
//        [HttpPost]
//        public async Task<IHttpActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return Content(HttpStatusCode.BadRequest, model);
//            }
//            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
//            if (result.Succeeded)
//            {
//                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
//                if (user != null)
//                {
//                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
//                }
//                return Ok();
//            }
//            AddErrors(result);
//            return Ok(result.Errors);
//        }

//        //
//        // POST: /Manage/SetPassword
//        [Route("api/manage/setpassword")]
//        [HttpPost]
//        public async Task<IHttpActionResult> SetPassword([FromBody] SetPasswordViewModel model)
//        {
//            List<string> errors = new List<string>();
//            if (ModelState.IsValid)
//            {
//                try {
//                    var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
//                    if (result.Succeeded)
//                    {
//                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
//                        if (user != null)
//                        {
//                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
//                        }
//                        return Ok();
//                    }
//                    errors = result.Errors.ToList();
//                    AddErrors(result);
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine(e.InnerException);
//                }

//            }

//            // If we got this far, something failed, redisplay form
//            return Ok(errors);
//        }

//        //
//        // GET: /Manage/ManageLogins
//        [Route("api/manage/managelogins")]
//        [HttpGet]
//        public async Task<IHttpActionResult> ManageLogins()
//        {
//            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
//            if (user == null)
//            {
//                return Content(HttpStatusCode.BadRequest, "User");
//            }
//            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
//            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
//            return Ok(new ManageLoginsViewModel
//            {
//                CurrentLogins = userLogins,
//                OtherLogins = otherLogins
//            });
//        }

//        //
//        // POST: /Manage/LinkLogin
//        [Route("api/manage/linklogin")]
//        [HttpPost]
//        public IHttpActionResult LinkLogin([FromBody] string provider)
//        {
//            // Request a redirect to the external login provider to link a login for the current user
//            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
//        }

//        //
//        // GET: /Manage/LinkLoginCallback
//        public async Task<IHttpActionResult> LinkLoginCallback()
//        {
//            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
//            if (loginInfo == null)
//            {
//                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
//            }
//            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
//            if (result.Succeeded)
//            {
//                UpdateUserFromFacebook(loginInfo);
//            }
//            return result.Succeeded ? RedirectToAction("Index") : RedirectToAction("Index", new { Message = ManageMessageId.ExternalLoginExists });
//        }

//        //
//        // GET: /Manage/ModifyPersonalInfo
//        [HttpGet]
//        [Authorize]
//        public IHttpActionResult ModifyPersonalInfo()
//        {
//            var appUser = UserManager.FindById(User.Identity.GetUserId());
//            if (appUser == null)
//            {
//                throw new UnauthorizedAccessException();
//            }
//            var user = appUser.User;
//            PersonalInfoViewModel model = new PersonalInfoViewModel
//            {
//                Age = user.Age,
//                Name = user.Name,
//                Gender = user.Gender,
//                Phone = user.Phone
//            };

//            return View(model);
//        }

//        //
//        // POST: /Manage/ChangePersonalInfo
//        [HttpPost]
//        [Authorize]
//        public IHttpActionResult ChangePersonalInfo(PersonalInfoViewModel model)
//        {
//            var appUser = UserManager.FindById(User.Identity.GetUserId());
//            if (appUser == null)
//            {
//                throw new UnauthorizedAccessException();
//            }
//            var user = appUser.User;
//            var uDto = new LoginDto
//            {
//                Name = model.Name,
//                Age = model.Age,
//                Phone = model.Phone,
//                Gender = model.Gender == (int)AppServices.Gender.Male ? "male" : "female"
//            };
//            Service.GetUserService().UpdateUserInfo(user.id, uDto);

//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && _userManager != null)
//            {
//                _userManager.Dispose();
//                _userManager = null;
//            }

//            base.Dispose(disposing);
//        }

//#region Helpers
//        // Used for XSRF protection when adding external logins
//        private const string XsrfKey = "XsrfId";

//        private IAuthenticationManager AuthenticationManager
//        {
//            get
//            {
//                return HttpContext.GetOwinContext().Authentication;
//            }
//        }

//        private void AddErrors(IdentityResult result)
//        {
//            foreach (var error in result.Errors)
//            {
//                ModelState.AddModelError("", error);
//            }
//        }

//        private bool HasPassword()
//        {
//            var user = UserManager.FindById(User.Identity.GetUserId());
//            if (user != null)
//            {
//                return user.PasswordHash != null;
//            }
//            return false;
//        }

//        private bool HasPhoneNumber()
//        {
//            var user = UserManager.FindById(User.Identity.GetUserId());
//            if (user != null)
//            {
//                return user.PhoneNumber != null;
//            }
//            return false;
//        }

//        public enum ManageMessageId
//        {
//            AddPhoneSuccess,
//            ChangePasswordSuccess,
//            SetTwoFactorSuccess,
//            SetPasswordSuccess,
//            RemoveLoginSuccess,
//            RemovePhoneSuccess,
//            PhoneRequired,
//            ExternalLoginExists,
//            Error
//        }

//        private void UpdateUserFromFacebook(ExternalLoginInfo loginInfo)
//        {
//            var access_token = loginInfo.ExternalIdentity.Claims.FirstOrDefault(x => x.Type == "FacebookAccessToken").Value;

//            var fb = new FacebookClient(access_token);
//            dynamic claims = fb.Get("/me?fields=email,name,gender,id");

//            LoginDto userInfo = new LoginDto
//            {
//                Gender = claims.gender,
//                Name = claims.name,
//                FacebookId = claims.id,
//                PictureUrl = GetFacebookPictureUrl(claims.id)
//            };

//            Service.GetUserService().UpdateUserInfo(UserManager.FindById(User.Identity.GetUserId()).User.id, userInfo);
//        }

//        private string GetFacebookPictureUrl(string facebookId)
//        {
//            WebResponse response = null;
//            string pictureUrl = string.Empty;
//            try
//            {
//                WebRequest request = WebRequest.Create(string.Format("https://graph.facebook.com/{0}/picture?type=large", facebookId));
//                response = request.GetResponse();
//                pictureUrl = response.ResponseUri.ToString();
//            }
//            catch (Exception ex)
//            {
//#if DEBUG
//                Debug.WriteLine(ex.InnerException);
//#endif
//                return "";
//            }
//            finally
//            {
//                if (response != null) response.Close();
//            }
//            return pictureUrl;
//        }

//        #endregion
//    }
//}