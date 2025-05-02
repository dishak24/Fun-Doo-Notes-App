using CommonLayer.Model;
using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client.Impl;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDooNotesApp.Controllers
{
    //[Route("[controller]")]
    //[ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INotesManager notesManager;

        //For Logger
        private readonly ILogger<UsersController> logger;

        //Dependencies For redis cache
        private readonly FundooDBContext Context;
        private readonly IDistributedCache cache;
        public NotesController(INotesManager notesManager, IDistributedCache cache, FundooDBContext Context, ILogger<UsersController> logger)
         {
            this.notesManager = notesManager;
            this.cache = cache;
            this.Context = Context;
            this.logger = logger;
         }

        //for create a note 
        [HttpPost]
        [Route("addNote")]
        [Authorize]
        public IActionResult CreateNote([FromBody] NotesModel notesModel)
        {
            try
            {
                var userIdClaim = User.FindFirst("UserId");
                if (userIdClaim == null)
                {
                    return Unauthorized(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "UserId not found in token."
                    });
                }

                int userId = int.Parse(userIdClaim.Value);

                var note = notesManager.CreateNote(userId, notesModel);
                if (note != null)
                {
                    return Ok(new ResponseModel<NotesEntity>
                    {
                        Success = true,
                        Message = "Done, Note Added successfully !",
                        Data = note
                    });
                }
                else
                {
                    return BadRequest(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "Adding Notes Failed !!!!!"
                    });
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
                return StatusCode(500, new ResponseModel<string>
                {
                    Success = false,
                    Message = "Internal Server Error: " + e.Message
                });
            }
        }

        //Get all Notes API
        [HttpGet]
        [Route("getAllNotes")]
        [Authorize]
        public IActionResult GetAllNotes()
        {
            try
            {
                int userId = int.Parse(User.FindFirst("UserId").Value);
                List<NotesEntity> notes = notesManager.GetAllNotes(userId);
               
                if (notes == null)
                {
                    return BadRequest(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "Empty Notes !!!!!"

                    });
                }
                else
                {
                    return Ok(new ResponseModel <List<NotesEntity>>
                    {
                        Success = true,
                        Message = "All Notes : ",
                        Data = notes

                    });
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
                throw e;
            }
        }

        //API : To Update Notes 
        [HttpPut]
        [Route("updateNote/{noteId}")]
        [Authorize]
        public IActionResult UpdateNote([FromRoute]int noteId, [FromBody]NotesModel model)
        {
            try
            {
                int userId = int.Parse(User.FindFirst("UserId").Value);
                NotesEntity note = notesManager.UpdateNote(noteId, model, userId);
                if (note != null )
                {
                    return Ok(new ResponseModel<NotesEntity>
                    {
                        Success = true,
                        Message = "Note Updated Seccessful.  ",
                        Data = note

                    });
                }
                else
                {
                    return BadRequest(new ResponseModel<NotesEntity>
                    {
                        Success = false,
                        Message = " Note Upadation failed !!!!!  ",
                        Data = note

                    });
                }
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        //To Delete Note
        [HttpDelete]
        [Route("deleteNote/{noteId}")]
        [Authorize]
        public IActionResult DeleteNote([FromRoute]int noteId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId").Value);
                bool note = notesManager.DeleteNote(noteId, userId);
                if (note)
                {
                    return Ok(new ResponseModel<bool>
                    {
                        Success = true,
                        Message = "Note Deleted Seccessfully.  ",
                        Data = note

                    });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool>
                    {
                        Success = false,
                        Message = "Deletion Failed !!!! ",
                        Data = note

                    });
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        //Fetch Notes using title
        [HttpGet]
        [Route("GetNotesByTitleOrDescription/{searchText}")]
        [Authorize]
        public IActionResult GetNotesByTitleOrDescription(string searchText)
        {
            try
            {
                List<NotesEntity> notes = notesManager.GetNotesByTitleOrDescription(searchText);
                if (notes == null || notes.Count == 0)
                {
                    return BadRequest(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "No matching notes found!"
                    });
                }
                else
                {
                    return Ok(new ResponseModel<List<NotesEntity>>
                    {
                        Success = true,
                        Message = "Notes fetched successfully.",
                        Data = notes
                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Return Count of notes a user has
        [HttpGet]
        [Route("CountAllNotes")]
        public IActionResult CountAllNotes()
        {
            try
            {
                var count = notesManager.CountAllNotes();
                if (count == null)
                {
                    return BadRequest(new ResponseModel<int>
                    {
                        Success = false,
                        Message = "Empty Notes !!!!!",
                        Data = count

                    });
                }
                else
                {
                    return Ok(new ResponseModel<int>
                    {
                        Success = true,
                        Message = "Getting Notes Count successfull. ",
                        Data = count

                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //To Pin or UnPin note
        [HttpPut]
        [Route("Pin/{noteId}")]
        public IActionResult PinNote(int noteId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId").Value);
                var result = notesManager.PinNote(noteId, userId);
                if (result)
                {
                    return Ok(new ResponseModel<bool>
                    {
                        Success = true,
                        Message = "Pinned or UnPinned Note Successfull.",
                        Data = result

                    });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool>
                    {
                        Success = false,
                        Message = "failed this Operation !!!!! ",
                        Data = result

                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //To Archive note
        [HttpPut]
        [Route("archive/{noteId}")]
        [Authorize]
        public IActionResult ArchiveNote([FromRoute] int noteId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId").Value);
                var result = notesManager.ArchiveNote(noteId, userId);
                if (result)
                {
                    return Ok(new ResponseModel<bool>
                    {
                        Success = true,
                        Message = " Note Archived Successfull.",
                        Data = result

                    });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool>
                    {
                        Success = false,
                        Message = "failed this Operation !!!!! ",
                        Data = result

                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //To Trash note
        [HttpPut]
        [Route("trash/{noteId}")]
        [Authorize]
        public IActionResult TrashNote([FromRoute]int noteId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId").Value);
                var result = notesManager.TrashNote(noteId, userId);
                if (result)
                {
                    return Ok(new ResponseModel<bool>
                    {
                        Success = true,
                        Message = "Note Trashed Successfull.",
                        Data = result

                    });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool>
                    {
                        Success = false,
                        Message = "failed this Operation !!!!! ",
                        Data = result

                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //To add colour to note
        [HttpPut]
        [Route("addColour/{noteId}")]
        [Authorize]
        public IActionResult AddColourToNote([FromRoute]int noteId, [FromBody]ColorModel model)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId").Value);
                var result = notesManager.AddColourToNote(noteId, model.Colour, userId);
                if (result)
                {
                    return Ok(new ResponseModel<bool>
                    {
                        Success = true,
                        Message = "Successfully added colour to Note .",
                        Data = result

                    });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool>
                    {
                        Success = false,
                        Message = "failed this Operation !!!!! ",
                        Data = result

                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //To add Remainder to note
        [HttpPut]
        [Route("addReminder/{noteId}")]
        [Authorize]
        public IActionResult AddRemainderToNote([FromRoute]int noteId, [FromBody]ReminderModel model)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId").Value);
                var result = notesManager.AddRemainderToNote(noteId, model.Reminder, userId);
                if (result)
                {
                    return Ok(new ResponseModel<bool>
                    {
                        Success = true,
                        Message = "Successfully added Remainder to Note.",
                        Data = result

                    });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool>
                    {
                        Success = false,
                        Message = "failed this Operation !!!!! ",
                        Data = result

                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //To adding Image to Note
        [HttpPut]
        [Route("AddImage")]
        public IActionResult AddImage(int NoteId, IFormFile Image)
        {
            try
            {
                var UserId = int.Parse(User.FindFirst("UserId").Value);
                var result = notesManager.AddImage(NoteId, UserId, Image);
                if (result)
                {
                    return Ok(new ResponseModel<bool>
                    {
                        Success = true,
                        Message = "Successfully added Image to Note.",
                        Data = result

                    });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool>
                    {
                        Success = false,
                        Message = "failed to add image !!!!!!",
                        Data = result

                    });
                }

            }
            catch(Exception e)
            {
                throw e;
            }
        }


        //Using Radis cache
        [HttpGet]
        [Route("GetAllNotes-RedisCache")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            try
            {

                var CacheKey = "NoteList";
                string SerializedNoteList;
                var NotesList = new List<NotesEntity>();
                byte[] RedisNotesList = await cache.GetAsync(CacheKey);
                if (RedisNotesList != null)
                {
                    SerializedNoteList = Encoding.UTF8.GetString(RedisNotesList);
                    NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(SerializedNoteList);
                }
                else
                {
                    NotesList = Context.Notes.ToList();
                    SerializedNoteList = JsonConvert.SerializeObject(NotesList);
                    RedisNotesList = Encoding.UTF8.GetBytes(SerializedNoteList);
                    var option = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(30))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    await cache.SetAsync(CacheKey, RedisNotesList, option);
                }
                return Ok(NotesList);
            }
            catch(Exception e)
            {
                logger.LogError(e.ToString());
                throw e;
            }

        }

    }
}
