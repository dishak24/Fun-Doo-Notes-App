using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text;

namespace CommonLayer.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required !!")]
        [RegularExpression(@"^[a-zA-Z0-9]+([._-][0-9a-zA-Z]+)*@[a-zA-Z0-9]+([.-][0-9a-zA-Z]+)*\.[a-zA-Z]{2,}$",
                            ErrorMessage = "Email must be in proper format!!")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Password is Required !!")]
        /*[PasswordPropertyText]
        [RegularExpression(@"^(.*[A-Z])(.*[a-z])(.*\d)(.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                                ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character. and atleast 8 digit password !")]
*/        public string Password { get; set; }
    }
}
