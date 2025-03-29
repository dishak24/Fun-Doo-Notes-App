using CommonLayer.Model;
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
        public UserEntity Register(RegisterModel model)
        {
            UserEntity user = new UserEntity();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DOB = model.DOB;
            user.Gender = model.Gender;
            user.Email = model.Email;
            user.Password = EncodePasswordToBase64(model.Password);

            this.context.Users.Add(user);
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
        public bool CheckEmailExist(string email)
        {
            var result = this.context.Users.FirstOrDefault(x => x.Email == email);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        //User Login
        public string Login(LoginModel loginModel)
        {
            var checkUser = this.context.Users.FirstOrDefault(u => u.Email == loginModel.Email && u.Password == EncodePasswordToBase64(loginModel.Password));
            if (checkUser != null)
            {
                //return user;
                var token = GenerateToken(checkUser.Email, checkUser.UserId);
                return token;
            }
            return null;
        }


        // To generate token
        private string GenerateToken(string emailId, int userId)
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
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
