using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepo
    {
        //For User registration
        public Task<UserEntity> Register(RegisterModel model);


        //for duplicate email checking
        public Task<bool> CheckEmailExist(string email);

        //for login user
        public Task<string> Login(LoginModel loginModel);

        //Forgot password
        public Task<ForgotPasswordModel> ForgotPassword(string email);

        //Reset passwod
        public Task<bool> ResetPassword(string email, ResetPasswordModel reset);

        //Get all users
        public Task<List<UserEntity>> GetAllUsers();

        //Find a user by ID
        public Task<UserEntity> GetUserById(int userId);

        //Get users whose name starts with 'A'
        public Task<List<UserEntity>> GetUsersNameStartsWithLetter(string letter);

        //Count the total number of users
        public Task<int> CountUsers();

        //Get users ordered by name (ascending )
        public  Task<List<UserEntity>> GetUsersByNamesASC();

        //Get users ordered by name (descending)
        public Task<List<UserEntity>> GetUsersByNamesDESC();

        //Get the average age of users
        public Task<double> GetUsersAverageAge();

        //Get the youngest user age
        public Task<int> GetYoungestUserAge();

        //Get the oldest user age
        public Task<int> GetOldestUserAge();
        

    }
}
