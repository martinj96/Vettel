using System.Collections.Generic;

namespace Transpo.Models
{
    public class SetPasswordViewModel
    {
        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        public string Code { get; set; }
        public string PhoneNumber { get; set; }
    }

}