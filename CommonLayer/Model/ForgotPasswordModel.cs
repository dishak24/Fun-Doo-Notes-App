using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text;

namespace CommonLayer.Model
{
    public class ForgotPasswordModel
    {
        //for returning data,  not for input/request
        public int UserId { get; set; }

        [Required(ErrorMessage = "Email is required !!")]
        [RegularExpression(@"^[a-zA-Z0-9]+([._-][0-9a-zA-Z]+)*@[a-zA-Z0-9]+([.-][0-9a-zA-Z]+)*\.[a-zA-Z]{2,}$",
                    ErrorMessage = "Email must be in proper format!!")]
        public string Email { get; set; }

        public string Token { get; set; }
    }
}
