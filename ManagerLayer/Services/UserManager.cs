using CommonLayer.Model;
using ManagerLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer;

using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Interfaces;
using System.Threading.Tasks;

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
        public async Task<UserEntity> Register(RegisterModel model)
        {
            return await userRepo.Register(model);
        }

        //Checking email exist or not. Duplicate email not allowed
        public async Task<bool> CheckEmailExist(string email)
        {
            return await userRepo.CheckEmailExist(email);
        }

        //Login User
        public async Task<string> Login(LoginModel loginModel)
        {
            return await userRepo.Login(loginModel);
        }

        //Forgot Password
        public async Task<ForgotPasswordModel> ForgotPassword(string email)
        {
            return await userRepo.ForgotPassword(email);
        }

        //Reset Password
        public async Task<bool> ResetPassword(string email, ResetPasswordModel reset)
        {
            return await userRepo.ResetPassword(email, reset);
        }

        //Get all users
        public async Task<List<UserEntity>> GetAllUsers()
        {
            return await userRepo.GetAllUsers();
        }

        //Find a user by ID
        public async Task<UserEntity> GetUserById(int userId)
        {
            return await userRepo.GetUserById(userId);
        }

        //Get users whose name starts with 'A'
        public async Task<List<UserEntity>> GetUsersNameStartsWithLetter(string letter)
        {
            return await userRepo.GetUsersNameStartsWithLetter(letter);
        }

        //Count the total number of users
        public async Task<int> CountUsers()
        {
            return await userRepo.CountUsers();
        }

        //Get users ordered by name (ascending)
        public async Task<List<UserEntity>> GetUsersByNamesASC()
        {
            return await userRepo.GetUsersByNamesASC();
        }

        //Get users ordered by name (descending)
        public async Task<List<UserEntity>> GetUsersByNamesDESC()
        {
            return await userRepo.GetUsersByNamesDESC();
        }

        //Get the average age of users
        public async Task<double> GetUsersAverageAge()
        {
            return await userRepo.GetUsersAverageAge();
        }

        //Get the youngest user age
        public async Task<int> GetYoungestUserAge()
        {
            return await userRepo.GetYoungestUserAge();
        }

        //Get the oldest user age
        public async Task<int> GetOldestUserAge()
        {
            return await userRepo.GetOldestUserAge();
        }
    }
}
