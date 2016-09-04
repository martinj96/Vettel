using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Transpo.Infrastructure.Data.Identity;
using System.Net;
using Transpo.Infrastructure.Data.Entities;
using System.Diagnostics;
using System.IO;
using System.Web.Http;
using Transpo.API.Models;
using System.Net.Http;
using System.Web.Hosting;
using System.Collections.Generic;
using System.Threading;

namespace Transpo.API.Controllers
{
    public class AccountController : BaseApiController
    {
        // POST: /Account/Login
        [Route("api/account/login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return Ok(model);
                case SignInStatus.Failure:
                default:
                    return Content(HttpStatusCode.BadRequest, model);
            }
        }

        // POST: /Account/Register
        [Route("api/account/register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register([FromBody] RegisterViewModel model)
        {
            var errors = new List<string>();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                User = new User
                {
                    Name = string.IsNullOrEmpty(model.Name) ? model.Email : model.Name,
                    Email = model.Email,
                    Age = model.Age,
                    Gender = model.Gender,
                    Phone = model.Phone
                }
            };

            var exists = await UserManager.FindByNameAsync(model.Email);
            if (exists != null)
            {
                return Content(HttpStatusCode.Conflict, "Емаилот веќе е регистриран.");
            }

            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                var callbackUrl = Request.GetRequestContext().VirtualPathRoot + @"/Account/ConfirmEmail?userId=" + user.Id + "&code=" + code;
                string body;
                using (var sr = new StreamReader(HostingEnvironment.MapPath("~\\EmailTemplates\\ConfirmMail.html")))
                {
                    body = sr.ReadToEnd();
                    body = body.Replace("[NAME]", user.User.Name);
                    body = body.Replace("[confirmAccountUrl]", callbackUrl);
                }
                await UserManager.SendEmailAsync(user.Id, "Потврда на корисничка сметка", body);

                return Ok();
            }
            return GetErrorResult(result);       
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<IHttpActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return Content(HttpStatusCode.BadRequest, "Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
                return Ok();
            else
                return GetErrorResult(result);
        }

        // POST: /Account/ForgotPassword
        [Route("api/account/forgotpassword")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return Ok();
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Request.GetRequestContext().VirtualPathRoot + @"/Account/ResetPassword?userId=" + user.Id + "&code=" + code;
                //var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                string body;
                using (var sr = new StreamReader(HostingEnvironment.MapPath("~\\EmailTemplates\\ForgetPassword.html")))
                {
                    body = sr.ReadToEnd();
                    body = body.Replace("[NAME]", user.User.Name);
                    body = body.Replace("[resetPasswordUrl]", callbackUrl);
                }

                await UserManager.SendEmailAsync(user.Id, "Заборавена лозинка", body);
                return Ok();
            }

            // If we got this far, something failed, redisplay form
            return Content(HttpStatusCode.BadRequest, model);
        }

        //
        // POST: /Account/ResetPassword
        [Route("api/account/resetpassword")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, model);
            }

            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return Ok();
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return GetErrorResult(result);
        }


        //
        // GET: /Account/IsTaken
        [Route("api/account/istaken")]
        [HttpGet]
        [AllowAnonymous]
        public bool IsTaken([FromUri] string email)
        {
            var result = UserManager.FindByName(email);
            return result != null;
        }

        [Route("api/account/registerexternal")]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            // Get the information about the user from the external login provider
            var info = await AuthenticationManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                return InternalServerError();
            }

            var appUser = new AppUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var access_token = info.ExternalIdentity.Claims.FirstOrDefault(x => x.Type == "FacebookAccessToken").Value;
            var uri = @"graph.facebook.com/me?fields=fields=email,name,gender,id&access_token=" + access_token;

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            dynamic claims = await response.Content.ReadAsAsync<dynamic>();

            var user = new User
            {
                Gender = claims.gender == "male" ? 1 : 2,
                Email = claims.email,
                Name = claims.name,
                FacebookId = claims.id,
                PictureUrl = GetFacebookPictureUrl(claims.id)
            };

            appUser.User = user;

            var result = await UserManager.CreateAsync(appUser);
            if (result.Succeeded)
            {
                result = await UserManager.AddLoginAsync(appUser.Id, info.Login);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            return GetErrorResult(result);
        }

        private string GetFacebookPictureUrl(string facebookId)
        {
            WebResponse response = null;
            string pictureUrl = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(string.Format("https://graph.facebook.com/{0}/picture?type=large", facebookId));
                response = request.GetResponse();
                pictureUrl = response.ResponseUri.ToString();
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex.InnerException);
#endif
                return "";
            }
            finally
            {
                if (response != null) response.Close();
            }
            return pictureUrl;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public class ChallengeResult : IHttpActionResult
        {
            public ChallengeResult(string loginProvider, ApiController controller)
            {
                LoginProvider = loginProvider;
                Request = controller.Request;
            }

            public string LoginProvider { get; set; }
            public HttpRequestMessage Request { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                Request.GetOwinContext().Authentication.Challenge(LoginProvider);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                response.RequestMessage = Request;
                return Task.FromResult(response);
            }
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
        #endregion
    }
}