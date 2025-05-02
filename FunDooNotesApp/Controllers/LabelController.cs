using CommonLayer.Model;
using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System;
using System.Threading.Tasks;

namespace FunDooNotesApp.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Authorize]
    public class LabelController : ControllerBase
    {
        private readonly ILabelManager labelManager;
        private readonly FundooDBContext context;
        public LabelController(ILabelManager labelManager, FundooDBContext context)
        {
            this.labelManager = labelManager;
            this.context = context;
        }

        // Create a Label
        [HttpPost]
        [Route("createLabel")]
        [Authorize]
        public async Task<IActionResult> CreateLabel([FromBody]CreateLabelModel model)
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


        // Get All Labels of Users ---not from notes
        [HttpGet()]
        [Route("getAllLabels")]
        [Authorize]
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
        [Route("assignLabel")]
        [Authorize]
        public async Task<IActionResult> AssignLable([FromBody]AssignLabelModel model)
        {
            try
            {
                var result = await labelManager.AssignLableAsync(model.NoteId, model.LabelId);
                if (result != null)
                {
                    var note = await context.Notes
                                             .Include(n => n.NoteLabels)
                                             .ThenInclude(nl => nl.Label)
                                             .FirstOrDefaultAsync(n => n.NotesId == model.NoteId);

                    return Ok(new ResponseModel<NotesEntity>
                    {
                        Success = true,
                        Message = "Label assigned to note.",
                        Data = note
                    });
                }
                else
                {
                    return BadRequest(new ResponseModel<NotesEntity>
                    {
                        Success = false,
                        Message = "Failed to assign label to note.",
                        Data = null
                    });
                }
            }
            catch( Exception e)
            {
                throw e;
            }
        }


        //update label
        [HttpPut]
        [Route("updateLabel/{labelId}")]
        [Authorize]
        public async Task<IActionResult> UpdateLabel(int labelId, [FromBody] CreateLabelModel model)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId").Value);
                var updatedLabel = await labelManager.UpdateLabelAsync(userId, labelId, model.LabelName);
                if (updatedLabel != null)
                {
                    return Ok(updatedLabel);
                }
                return NotFound("Label not found.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //delete label
        [HttpDelete]
        [Route("deleteLabel/{labelId}")]
        [Authorize]
        public async Task<IActionResult> DeleteLabel(int labelId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId").Value);
                var success = await labelManager.DeleteLabelAsync(userId, labelId);
                if (success)
                {
                    return Ok(new { message = "Label deleted successfully" });
                }
                return NotFound("Label not found.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        // Assign Label to a Note
        [HttpDelete]
        [Route("removeLableFromNote/{LabelId}")]
        [Authorize]
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

        
        //Practice code only. Not the part of FunDoo notes.
        //To convert Timezone
               
        [HttpGet]
        [Route("ConvertTimeZone")]
        public IActionResult ConvertTimeZone( string AnotherZone )
        {
                
             try
             {
                    
                var IndiaZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                var OtherZone = TimeZoneInfo.FindSystemTimeZoneById(AnotherZone);
                                        
                var Time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IndiaZone);

                var UTCTime = TimeZoneInfo.ConvertTimeToUtc(Time, IndiaZone);
                var Result = TimeZoneInfo.ConvertTimeFromUtc(UTCTime, OtherZone);
                   
                if(Result !=null )
                {
                     return Ok(new ResponseModel<DateTime>
                     {
                         Success = true,
                         Message = "Time zone Converted successfully.",
                         Data = Result
                     });
                        
                }
                else
                {
                    return BadRequest(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "Conversion Failed !!!!.",


                    });
                }
                        
             }
             catch(Exception e)
             {
                throw e;   
             }
        }
    }


    


}

