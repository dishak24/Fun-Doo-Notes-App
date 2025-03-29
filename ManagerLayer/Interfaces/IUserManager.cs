using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interfaces
{
    public interface IUserManager
    {
        //Register User
        public UserEntity Register(RegisterModel model);

        //Checking email exist or not. Duplicate email not allowed
        public bool CheckEmailExist(string email);
    }
}
