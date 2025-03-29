using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepo
    {
        //For User registration
        public UserEntity Register(RegisterModel model);


        //for duplicate email checking
        public bool CheckEmailExist(string email);

        //for login user
        public string Login(LoginModel loginModel);

        //Forgot password
        public ForgotPasswordModel ForgotPassword(string email);

        //Reset passwod
        public bool ResetPassword(string email, ResetPasswordModel reset);
    }
}
