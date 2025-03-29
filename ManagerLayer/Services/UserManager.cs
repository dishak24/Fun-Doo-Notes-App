using CommonLayer.Model;
using ManagerLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer;

using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Interfaces;

namespace ManagerLayer.Services
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepo userRepo;

        public UserManager(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }

        
        public UserEntity Register(RegisterModel model)
        {
            return userRepo.Register(model);
        }

        
        public bool CheckEmailExist(string email)
        {
            return userRepo.CheckEmailExist(email);
        }

        public string Login(LoginModel loginModel)
        {
            return userRepo.Login(loginModel);
        }
    }
}
