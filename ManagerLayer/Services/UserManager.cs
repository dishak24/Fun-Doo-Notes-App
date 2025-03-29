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
        //Dependency
        private readonly IUserRepo userRepo;

        public UserManager(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }

        //Register User
        public UserEntity Register(RegisterModel model)
        {
            return userRepo.Register(model);
        }

        //Checking email exist or not. Duplicate email not allowed
        public bool CheckEmailExist(string email)
        {
            return userRepo.CheckEmailExist(email);
        }

        //Login User
        public string Login(LoginModel loginModel)
        {
            return userRepo.Login(loginModel);
        }

        //Forgot Password
        public ForgotPasswordModel ForgotPassword(string email)
        {
            return userRepo.ForgotPassword(email);
        }

        //Reset Password
        public bool ResetPassword(string email, ResetPasswordModel reset)
        {
            return userRepo.ResetPassword(email, reset);
        }

        //Get all users
        public List<UserEntity> GetAllUsers()
        {
            return userRepo.GetAllUsers();
        }

        //Find a user by ID
        public UserEntity GetUserById(int userId)
        {
            return userRepo.GetUserById(userId);
        }

        //Get users whose name starts with 'A'
        public List<UserEntity> GetUsersNameStartsWithLetter(string letter)
        {
            return userRepo.GetUsersNameStartsWithLetter(letter);
        }

        //Count the total number of users
        public int CountUsers()
        {
            return userRepo.CountUsers();
        }

        //Get users ordered by name (ascending)
        public List<UserEntity> GetUsersByNamesASC()
        {
            return userRepo.GetUsersByNamesASC();
        }

        //Get users ordered by name (descending)
        public List<UserEntity> GetUsersByNamesDESC()
        {
            return userRepo.GetUsersByNamesDESC();
        }

        //Get the average age of users
        public double GetUsersAverageAge()
        {
            return userRepo.GetUsersAverageAge();
        }

        //Get the youngest user age
        public int GetYoungestUserAge()
        {
            return userRepo.GetYoungestUserAge();
        }

        //Get the oldest user age
        public int GetOldestUserAge()
        {
            return userRepo.GetOldestUserAge();
        }
    }
}
