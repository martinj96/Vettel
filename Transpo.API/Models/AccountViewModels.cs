using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Transpo.API.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Емаил")]
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

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Емаил")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Лозинка")]
        public string Password { get; set; }

        [Display(Name = "Запамти ме?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Емаил")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Лозинка")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потврди лозинка")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Пол")]
        public int? Gender { get; set; }

        [Range(0, 99, ErrorMessage = "Please enter a valid age")]
        [Display(Name = "Возраст")]
        public int? Age { get; set; }

        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Емаил")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Лозинката мора да има барем {2} карактери.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Лозинка")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потврди лозинка")]
        [Compare("Password", ErrorMessage = "Лозинките не се совпаѓаат.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Емаил")]
        public string Email { get; set; }
    }

    public class PersonalInfoViewModel
    {
        [Display(Name = "Име")]
        public string Name { get; set; }
        [Display(Name = "Пол")]
        public int? Gender { get; set; }
        [Display(Name = "Возраст")]
        public int? Age { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
    }
}
