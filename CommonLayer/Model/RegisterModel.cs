using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class RegisterModel
    {
        //Validations Applied
        [Required(ErrorMessage = "First Name is required !!")]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$",
                            ErrorMessage = "First Name must start with a capital letter and be atleast 3 characters long.")]
        public string FirstName { get; set; }



        [Required(ErrorMessage = "Last Name is required !!")]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$",
                            ErrorMessage = "Last Name must start with a capital letter and be atleast 3 characters long.")]
        public string LastName { get; set; }



        [Required(ErrorMessage = "Birth date is required !!")]
        [DataType(DataType.Date, ErrorMessage = "Invalide Date format.")]
        [Range(typeof(DateTime), "1970-01-01", "2020-01-01", ErrorMessage = "Birthdate must be between 2001 - 2025.")]
        public DateTime DOB { get; set; }


        [Required(ErrorMessage = "Gender is required !!")]
        [RegularExpression(@"^(Male|Female)$", ErrorMessage = "Gender must be Male or Female only !")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email is required !!")]
        [RegularExpression(@"^[a-zA-Z0-9]+([._-][0-9a-zA-Z]+)*@[a-zA-Z0-9]+([.-][0-9a-zA-Z]+)*\.[a-zA-Z]{2,}$",
                            ErrorMessage = "Email must be in proper format!!")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Password is Required !!")]
        [PasswordPropertyText]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character (@$!%*?&).")]
        public string Password { get; set; }
        
    }
}
