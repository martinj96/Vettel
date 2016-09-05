using System;

namespace Transpo.Models
{
    // Models used as parameters to AccountController actions.

    public class AddExternalLoginBindingModel
    {
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class RegisterExternalBindingModel
    {
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
