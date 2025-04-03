using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text;

namespace CommonLayer.Model
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Password is Required !!")]
        [PasswordPropertyText]
        [RegularExpression(@"^(.*[A-Z])(.*[a-z])(.*\d)(.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                                ErrorMessage = "Both should be Same. Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character. and atleast 8 digit password !")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required !!")]
        [PasswordPropertyText]
        [RegularExpression(@"^(.*[A-Z])(.*[a-z])(.*\d)(.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                                ErrorMessage = "Both should be Same. Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character. and atleast 8 digit password !")]
        public string ConfirmPassword { get; set; }
    }
}
