using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Transpo.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        public string Email { get; set; }

        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public int Gender { get; set; }
        public string FacebookId { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }


    public class VerifyCodeViewModel
    {
        public string Provider { get; set; }

        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public int? Gender { get; set; }

        public int? Age { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }
    }

    public class ResetPasswordViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        public string Email { get; set; }
    }

    public class PersonalInfoViewModel
    {
        public string Name { get; set; }
        public int? Gender { get; set; }
        public int? Age { get; set; }
        public string Phone { get; set; }
    }
}
