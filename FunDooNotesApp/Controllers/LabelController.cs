using CommonLayer.Model;
using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System;
using System.Threading.Tasks;

namespace FunDooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelManager labelManager;
        public LabelController(ILabelManager labelManager)
        {
            this.labelManager = labelManager;
        }

        // Create a Label
        [HttpPost]
        [Route("CreateLabel")]
        public async Task<IActionResult> CreateLabel(CreateLabelModel model)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId").Value);
                var label = await labelManager.CreateLabelAsync(userId, model.LabelName);
                return CreatedAtAction(nameof(CreateLabel), new { userId }, label);

            }
            catch(Exception e)
            {
                throw e;
            }
           
        }


        // Get All Labels for a User
        [HttpGet()]
        [Route("GetAllLabels")]
        public async Task<IActionResult> GetLabels()
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId").Value);
                var labels = await labelManager.GetLabelsAsync(userId);
                if (labels != null)
                {
                    return Ok(labels);
                }
                else
                {
                    return BadRequest(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "failed to get labels!!!!!"

                    });
                }
               

            }
            catch (Exception e)
            {
                throw e;
            }

            
        }


        // Assign Label to a Note
        [HttpPost]
        [Route("AssignLabel")]
        public async Task<IActionResult> AssignLable(AssignLabelModel model)
        {
            try
            {
                var result = await labelManager.AssignLableAsync(model.NoteId, model.LabelId);
                if ( result != null)
                {
                    return Ok(new ResponseModel<NoteLabelEntity>
                    {
                        Success = true,
                        Message = "Label Assign to Note successfully.",
                        Data = result

                    });
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteLabelEntity>
                    {
                        Success = false,
                        Message = "Failed to Assign Label !!!!!",
                        Data = result

                    });
                }
            }
            catch( Exception e)
            {
                throw e;
            }
        }


        // Assign Label to a Note
        [HttpDelete]
        [Route("RemoveLableFromNote/{LabelId}")]
        public async Task<IActionResult> RemoveLabel(int noteId, int LabelId)
        {
            try
            {
                var result = await labelManager.RemoveLabelFromNoteAsync(noteId, LabelId);
                if (result)
                {
                    return Ok(new ResponseModel<bool>
                    {
                        Success = true,
                        Message = "Label Removed successfully.",
                        Data = result

                    });
                }
                else
                {
                    return NotFound(new ResponseModel<bool>
                    {
                        Success = false,
                        Message = "NoteId or LabelId not found !!!!!!!",
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

