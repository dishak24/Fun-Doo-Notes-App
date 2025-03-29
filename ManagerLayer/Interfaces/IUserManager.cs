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

        //Login User
        public string Login(LoginModel loginModel);

        //Forgot password
        public ForgotPasswordModel ForgotPassword(string email);

        //Reset passwod
        public bool ResetPassword(string email, ResetPasswordModel reset);

        //Get all users
        public List<UserEntity> GetAllUsers();

        //Find a user by ID
        public UserEntity GetUserById(int userId);

        //Get users whose name starts with 'A'
        public List<UserEntity> GetUsersNameStartsWithLetter(string letter);

        //Count the total number of users
        public int CountUsers();

        //Get users ordered by name (ascending)
        public List<UserEntity> GetUsersByNamesASC();

        //Get users ordered by name ( descending)
        public List<UserEntity> GetUsersByNamesDESC();

        //Get the average age of users
        public double GetUsersAverageAge();


        //Get the youngest user age
        public int GetYoungestUserAge();

        //Get the oldest user age
        public int GetOldestUserAge();
    }
}
