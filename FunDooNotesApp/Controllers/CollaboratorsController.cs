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
    public class CollaboratorsController : ControllerBase
    {
        private readonly ICollaboratorsManager collaboratorsManager;
        public CollaboratorsController(ICollaboratorsManager collaboratorsManager)
        {
            this.collaboratorsManager = collaboratorsManager;
        }

        //Add Collaborator to Note
        [HttpPost]
        [Route("AddCollaborator")]
        public IActionResult AddCollaborator(int NoteId, string Email)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserId").Value);
                var result = collaboratorsManager.AddCollaborator(NoteId, UserId, Email);

                if (result != null)
                {
                    return Ok(new ResponseModel<CollaboratorsEntity>
                    {
                        Success = true,
                        Message = "Done, Collaborator Added successfully !",
                        Data = result

                    });
                }
                else
                {
                    return BadRequest(new ResponseModel<CollaboratorsEntity>
                    {
                        Success = true,
                        Message = "Adding Collaborator Failed !!!!!!",
                        Data = result

                    });
                }

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        //To Display all Collaborators
        [HttpGet]
        [Route("GetAllCollaborators/{NoteId}")]
        public IActionResult GetAllCollaborators( int NoteId)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserId").Value);
                var result = collaboratorsManager.GetAllCollaborators(NoteId, UserId);

                if (result != null)
                {
                    return Ok(new ResponseModel<List<CollaboratorsEntity>>
                    {
                        Success = true,
                        Message = "Getting All Collaborator successfully !",
                        Data = result

                    });
                }
                else
                {
                    return BadRequest(new ResponseModel<List<CollaboratorsEntity>>
                    {
                        Success = true,
                        Message = " Failed to Get All Collaborator!!!!!!",
                        Data = result

                    });
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Remove from Collaboration
        [HttpDelete]
        [Route("RemoveCollaborator/{CollaboratorId}")]
        public IActionResult RemoveCollaborator(int NoteId, [FromRoute] int CollaboratorId)
        {

            try
            {
                int UserId = int.Parse(User.FindFirst("UserId").Value);
                var result = collaboratorsManager.RemoveCollaborator(NoteId, UserId, CollaboratorId);

                if (result)
                {
                    return Ok(new ResponseModel<bool>
                    {
                        Success = true,
                        Message = "Removed Collaborator successfully !",
                        Data = result

                    });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool>
                    {
                        Success = true,
                        Message = "Failed to Remove Collaborator !!!!!!",
                        Data = result

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
