using CommonLayer.Model;
using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;

namespace FunDooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
            private readonly INotesManager notesManager;
            public NotesController(INotesManager notesManager)
            {
                this.notesManager = notesManager;
            }

            //API for Creating/adding new Note
            [HttpPost]
            [Route("AddNote")]
            public IActionResult CreateNote(NotesModel notesModel)
            {
                try
                {
                    int userId = int.Parse(User.FindFirst("UserId").Value);

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
                            Success = true,
                            Message = "Adding Notes Failed !!!!!"

                        });
                    }

                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        //Get all Notes API
        [HttpGet]
        [Route("GetAllNotes")]
        public IActionResult GetAllNotes()
        {
            try
            {
                List<NotesEntity> notes = notesManager.GetAllNotes();
                if (notes == null)
                {
                    return BadRequest(new ResponseModel<string>
                    {
                        Success = true,
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
                throw e;
            }
        }

        //Fetch Notes using title
        [HttpGet]
        [Route("GetNotesByTitle/{title}")]
        public IActionResult GetNotesByTitle(string title)
        {
            try
            {
                List<NotesEntity> notes = notesManager.GetNotesByTitle(title);
                if (notes == null)
                {
                    return BadRequest(new ResponseModel<string>
                    {
                        Success = true,
                        Message = "Empty Notes !!!!!"

                    });
                }
                else
                {
                    return Ok(new ResponseModel<List<NotesEntity>>                  {
                        Success = true,
                        Message = "Getting Notes successfull. ",
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
                        Success = true,
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

    }
}
