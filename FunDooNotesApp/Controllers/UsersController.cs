using CommonLayer.Model;
using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System.Threading.Tasks;
using System;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using RepositoryLayer.Migrations;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using RepositoryLayer.Context;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FunDooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager userManager;

        //For Rabbit MQ
        private readonly IBus bus;

        //For Logger
        private readonly ILogger<UsersController> logger;

        //Dependencies For redis cache
        private readonly IDistributedCache cache;
        private readonly FundooDBContext Context;

        public UsersController(IUserManager userManager, IBus bus, IDistributedCache cache, FundooDBContext Context, ILogger<UsersController> logger)
        {
            this.userManager = userManager;
            this.bus = bus;
            this.cache = cache;
            this.Context = Context;
            this.logger = logger;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterModel model)
        {
            //adding code to checking email already used or not
            var check = userManager.CheckEmailExist(model.Email);
            
            if (check)
            {
                return BadRequest(new ResponseModel<UserEntity>
                {
                    Success = false,
                    Message = "Email already exist! Please, Enter another EmailId."
                });
            }
            else
            {
                var result = userManager.Register(model);

                //This stores (result.UserId) in the session under the key "UserId"
               HttpContext.Session.SetInt32("UserId", result.UserId );

                if (result != null)
                {
                    return Ok(new ResponseModel<UserEntity>
                    {
                        Success = true,
                        Message = "Registration Successfull !",
                        Data = result
                    });
                }
                return BadRequest(new ResponseModel<UserEntity>
                {
                    Success = false,
                    Message = "Registration failed !!!!",
                    Data = result
                });
            }
        }

        //login api
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            var result = userManager.Login(loginModel);
            if (result != null)
            {
                return Ok(new ResponseModel<string>
                {
                    Success = true,
                    Message = "Login Successfull !",
                    Data = result

                });
            }
            return BadRequest(new ResponseModel<string>
            {
                Success = false,
                Message = "Login failed !!!!",
                Data = result

            });

        }

        //Forgot password API
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                if (userManager.CheckEmailExist(email))
                {
                    Send send = new Send();
                    ForgotPasswordModel forgotPasswordModel = userManager.ForgotPassword(email);
                    send.SendingMail(forgotPasswordModel.Email, forgotPasswordModel.Token);
                    Uri uri = new Uri("rabbitmq://localhost/FunDooNotes_EmailQueue");
                    var endPoint = await bus.GetSendEndpoint(uri);

                    await endPoint.Send(forgotPasswordModel);

                    return Ok(new ResponseModel<string>
                    {
                        Success = true,
                        Message = "Mail sent Successfull !"

                    });
                }
                else
                {
                    return BadRequest(new ResponseModel<string>
                    {
                        Success = true,
                        Message = "Sending email failed !!!!!"

                    });
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Reset Password API
        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel model)
        {
            try
            {
                string email = User.FindFirst("EmailId").Value;
                if (userManager.ResetPassword(email, model))
                {
                    return Ok(new ResponseModel<string>
                    {
                        Success = true,
                        Message = "Done, Password is Reset !"

                    });
                }
                else
                {
                    return BadRequest(new ResponseModel<string>
                    {
                        Success = true,
                        Message = "Reseting Password Failed !!!!!"

                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //To get all Users
        [HttpGet]
        [Route("AllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<UserEntity> userList = userManager.GetAllUsers();
                if ( userList == null)
                {
                    return BadRequest(new ResponseModel<string>
                    {
                        Success = true,
                        Message = " No User available in Database !!!!!"

                    });
                }
                else
                {
                    return Ok(new ResponseModel<List<UserEntity>>
                    {
                        Success = true,
                        Message = "All Users :",
                        Data = userList

                    });
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        //Find a user by ID
        [HttpGet]
        [Route("GetUserById/{userId}")]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                var user = userManager.GetUserById(userId);

                if (user == null)
                {
                    return BadRequest(new ResponseModel<string>
                    {
                        Success = true,
                        Message = " This UserId does not exist !!!!!"

                    });
                }
                else
                {
                    return Ok(new ResponseModel<UserEntity>
                    {
                        Success = true,
                        Message = "Getting Users Data Successfully. :",
                        Data = user

                    });
                }

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        //Get users whose name starts with 'A'
        [HttpGet]
        [Route("GetNameStarsWith/{letter}")]
        public IActionResult GetUsersNameStartsWithLetter(string letter)
        {
            try
            {
                List<UserEntity> users = userManager.GetUsersNameStartsWithLetter(letter);

                if (users == null)
                {
                    return BadRequest(new ResponseModel<string>
                    {
                        Success = true,
                        Message = " Such Users not exist !!!!!"

                    });
                }
                else
                {
                    return Ok(new ResponseModel<List<UserEntity>>
                    {
                        Success = true,
                        Message = "Getting Users Data Successfully. ",
                        Data = users

                    });
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Count the total number of users
        [HttpGet]
        [Route("GetUserCount")]
        public IActionResult CountUsers()
        {
            try
            {
                var count = userManager.CountUsers();

                if (count == null)
                {
                    return BadRequest(new ResponseModel<int>
                    {
                        Success = true,
                        Message = " Empty User Database !!!!!! ",
                        Data = count

                    });
                }
                else
                {
                    return Ok(new ResponseModel<int>
                    {
                        Success = true,
                        Message = "Getting Users Count Successfully. ",
                        Data = count

                    });
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //Get users ordered by name (ascending)
        [HttpGet]
        [Route("GetUsersOrderByNames_ASC")]
        public IActionResult GetUsersByNamesASC()
        {
            List<UserEntity> users = userManager.GetUsersByNamesASC();
            try
            {
                if (users == null)
                {
                    return BadRequest(new ResponseModel<List<UserEntity>>
                    {
                        Success = true,
                        Message = " Empty Users !!!!!",
                        Data = users

                    });
                }
                else
                {
                    return Ok(new ResponseModel<List<UserEntity>>
                    {
                        Success = true,
                        Message = "Getting Users Data Successfully in Ascending Order. ",
                        Data = users

                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        //Get users ordered by name (descending)
        [HttpGet]
        [Route("GetUsersOrderByNames_DESC")]
        public IActionResult GetUsersByNamesDESC()
        {
            List<UserEntity> users = userManager.GetUsersByNamesDESC();
            try
            {
                if (users == null)
                {
                    return BadRequest(new ResponseModel<List<UserEntity>>
                    {
                        Success = true,
                        Message = " Empty Users !!!!!",
                        Data = users

                    });
                }
                else
                {
                    return Ok(new ResponseModel<List<UserEntity>>
                    {
                        Success = true,
                        Message = "Getting Users Data Successfully in Ascending Order. ",
                        Data = users

                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        //Get the average age of users
        [HttpGet]
        [Route("GetUsersAverageAge")]
       
        public IActionResult GetUsersAverageAge()
        {
            try
            {
                var avgAge = userManager.GetUsersAverageAge();

                if (avgAge == null)
                {
                    return BadRequest(new ResponseModel<double>
                    {
                        Success = true,
                        Message = " No Data to Calculate Average !!!!!! ",
                        Data = avgAge

                    });
                }
                else
                {
                    return Ok(new ResponseModel<double>
                    {
                        Success = true,
                        Message = "Average Age Getting Successfull. ",
                        Data = avgAge

                    });
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //Get the youngest user age
        [HttpGet]
        [Route("GetYoungestUserAge")]
        public IActionResult GetYoungestUserAge()
        {
            try
            {
                var age = userManager.GetYoungestUserAge();

                if (age == null)
                {
                    return BadRequest(new ResponseModel<int>
                    {
                        Success = true,
                        Message = " Empty User Database !!!!!! ",
                        Data = age

                    });
                }
                else
                {
                    return Ok(new ResponseModel<int>
                    {
                        Success = true,
                        Message = "Getting Youngest Age of User Successfully. ",
                        Data = age

                    });
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Get the oldest user age
        [HttpGet]
        [Route("GetOldestUserAge")]
        public IActionResult GetOldestUserAge()
        {
            try
            {
                var age = userManager.GetOldestUserAge();

                if (age == null)
                {
                    return BadRequest(new ResponseModel<int>
                    {
                        Success = true,
                        Message = " Empty User Database !!!!!! ",
                        Data = age

                    });
                }
                else
                {
                    return Ok(new ResponseModel<int>
                    {
                        Success = true,
                        Message = "Getting Oldest Age of User Successfully. ",
                        Data = age

                    });
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //Radis cache
        [HttpGet]
        [Route("GetAllUsers-RedisCache")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var CacheKey = "UserList";
            string SerializedNoteList;
            var UserList = new List<UserEntity>();
            byte[] RedisNotesList = await cache.GetAsync(CacheKey);
            if (RedisNotesList != null)
            {
                SerializedNoteList = Encoding.UTF8.GetString(RedisNotesList);
                UserList = JsonConvert.DeserializeObject<List<UserEntity>>(SerializedNoteList);
            }
            else
            {
                UserList = Context.Users.ToList();
                SerializedNoteList = JsonConvert.SerializeObject(UserList);
                RedisNotesList = Encoding.UTF8.GetBytes(SerializedNoteList);
                var option = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(30))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                await cache.SetAsync(CacheKey, RedisNotesList, option);
            }
            return Ok(UserList);

        }


    }

}

