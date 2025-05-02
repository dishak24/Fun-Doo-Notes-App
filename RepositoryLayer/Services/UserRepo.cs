using CommonLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserRepo: IUserRepo
    {
        private readonly FundooDBContext context;
        private readonly IConfiguration configuration;
        public UserRepo(FundooDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        //Register User
        public async Task<UserEntity> Register(RegisterModel model)
        {
            UserEntity user = new UserEntity();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DOB = model.DOB;
            user.Gender = model.Gender;
            user.Email = model.Email;
            user.Password = EncodePasswordToBase64(model.Password);

            await context.Users.AddAsync(user);
            context.SaveChanges();
            return user;
        }

        //To encrypt Password For security
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encryption_Data = new byte[password.Length];
                encryption_Data = System.Text.Encoding.UTF8.GetBytes(password);
                string encoded_Data = Convert.ToBase64String(encryption_Data);
                return encoded_Data;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64 encode " + e.Message);
            }
        }

        //Checking email exist or not. Duplicate email not allowed
        public async Task<bool> CheckEmailExist(string email)
        {
            var result = await this.context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        //User Login
        public async Task<string> Login(LoginModel loginModel)
        {
            var checkUser = await this.context.Users.FirstOrDefaultAsync(u => u.Email == loginModel.Email && u.Password == EncodePasswordToBase64(loginModel.Password));
            if (checkUser != null)
            {
                //return user;
                var token = await GenerateToken(checkUser.Email, checkUser.UserId);
                return token;
            }
            return null;
        }


        // To generate token
        private async Task<string> GenerateToken(string emailId, int userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("EmailId", emailId),
                new Claim("UserId", userId.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(7),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        //Forgot password method.
        public async Task<ForgotPasswordModel> ForgotPassword(string email)
        {
            var User = context.Users.ToList().Find(u => u.Email == email);
            ForgotPasswordModel forgotPassword = new ForgotPasswordModel();
            forgotPassword.UserId = User.UserId;
            forgotPassword.Email = User.Email;
            forgotPassword.Token = await GenerateToken(User.Email, User.UserId);
            return forgotPassword;
        }

        //To Reset the password:
        //1. check email exist or not
        //2. then change password
        public async Task<bool> ResetPassword(string email, ResetPasswordModel reset)
        {
            var user = context.Users.ToList().Find(u => u.Email == email);

            if (await CheckEmailExist(user.Email))
            {
                user.Password = EncodePasswordToBase64(reset.ConfirmPassword);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Get all users
        public async Task<List<UserEntity>> GetAllUsers()
        {
            List<UserEntity> allUsers = await context.Users.ToListAsync();
            return allUsers;
        }

        //Find a user by ID
        public async Task<UserEntity> GetUserById(int userId)
        {
            UserEntity User = await context.Users.FindAsync(userId);
            return User;
        }

        //Get users whose name starts with 'A'
        public async Task<List<UserEntity>> GetUsersNameStartsWithLetter(string letter)
        {
            List<UserEntity> users = await context.Users.Where(u => u.FirstName.StartsWith(letter)).ToListAsync();
            return users;
        }

        //Count the total number of users
        public async Task<int> CountUsers()
        {
            var count = await context.Users.CountAsync();
            return count;
        }

        //Get users ordered by name (ascending & descending)
        public async Task<List<UserEntity>> GetUsersByNamesASC ()
        {
            List<UserEntity> users = await context.Users.OrderBy(u => u.FirstName).ToListAsync();
            return users;
        }

        //Get users ordered by name (ascending & descending)
        public async Task<List<UserEntity>> GetUsersByNamesDESC()
        {
            List<UserEntity> users = await context.Users.OrderByDescending(u => u.FirstName).ToListAsync();
            return users;
        }

        //Get the average age of users
        public async Task<double> GetUsersAverageAge()
        {
            var average = await  context.Users.AverageAsync(x => (DateTime.Now.Year - x.DOB.Year));
            return average; 
        }

        //Get the youngest user age
        public async Task<int> GetYoungestUserAge()
        {
            var Age = await context.Users.Select(x => DateTime.Now.Year - x.DOB.Year).ToListAsync();
            return Age.Min();
        }

        //Get the oldest user age
        public async Task<int> GetOldestUserAge()
        {
            var Age = await context.Users.Select(x => DateTime.Now.Year - x.DOB.Year).ToListAsync();
            return Age.Max();
        }
    }
}
