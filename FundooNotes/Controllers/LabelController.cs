namespace Microsoft.Xxx
{
    using BusinessLayer.Interfaces;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RepositoryLayer.Context;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace FundooNotes.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        [Authorize]  //user to grant and restrict permissions on Web pages.
        public class LabelsController : ControllerBase
        {
            private readonly ILabelBL labelBL;
            private readonly FundooContext fundooContext;

            public LabelsController(ILabelBL labelBL, FundooContext fundooContext)
            {
                this.labelBL = labelBL;
                this.fundooContext = fundooContext;
            }

            [HttpPost("Create")]
            public IActionResult CreateLabel(LabelModel labelModel)
            {
                try
                {
                    long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                    var labelNote = this.fundooContext.NotesTable.Where(x => x.NotesId == labelModel.NoteId).SingleOrDefault();
                    if (labelNote.Id == userid)
                    {
                        var result = this.labelBL.CreateLabel(labelModel);
                        if (result)
                        {
                            return this.Ok(new { status = 200, isSuccess = true, Message = "Label is been created Successfully!", data = labelModel.LabelName });
                        }
                        else
                        {
                            return this.BadRequest(new { status = 400, isSuccess = false, Message = "Label was not created" });
                        }
                    }
                    return this.Unauthorized(new { status = 401, isSuccess = false, Message = "Unauthorized User!" });
                }
                catch (Exception e)
                {
                    return this.BadRequest(new { status = 400, isSuccess = false, Message = e.InnerException.Message });
                }
            }
            

            /// <summary>
            /// api for Get Labels by noteId
            /// </summary>
            /// <param name="NotesId"></param>
            /// <returns></returns>
            [HttpGet("Get{NotesId}")]
            public IActionResult GetlabelByNotesId(long NotesId)
            {
                try
                {
                    long userId = Convert.ToInt32(User.Claims.FirstOrDefault(X => X.Type == "Id").Value);
                    var labels = this.labelBL.GetlabelByNotesId(NotesId);
                    if (labels != null)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, message = " Specific label was found Successfully", data = labels });
                    }
                    else
                        return this.NotFound(new { isSuccess = false, message = "Specific label was not Found!" });
                }
                catch (Exception e)
                {
                    return this.BadRequest(new { Status = 401, isSuccess = false, Message = e.InnerException.Message });
                }
            }


            [HttpPut("Update")]
            public IActionResult UpdateLabel(LabelModel labelModel, long labelID)
            {
                try
                {
                    long userid = Convert.ToInt32(User.Claims.FirstOrDefault(X => X.Type == "Id").Value);
                    var result = this.labelBL.UpdateLabel(labelModel, labelID);
                    if (result != null)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, message = "Label Updated Successfully", data = result });
                    }
                    else
                    {
                        return this.NotFound(new { isSuccess = false, message = "No Label Found" });
                    }
                }
                catch (Exception e)
                {
                    return this.BadRequest(new { Status = 401, isSuccess = false, Message = e.InnerException.Message });
                }
            }
            /// <summary>
            /// Api for Deleting a label by labelID
            /// </summary>
            /// <param name="labelID"></param>
            /// <returns></returns>
            [HttpDelete("Delete")]
            public IActionResult DeleteLabel(long labelID)
            {
                try
                {
                    long userid = Convert.ToInt32(User.Claims.FirstOrDefault(X => X.Type == "Id").Value);
                    var delete = this.labelBL.DeleteLabel(labelID);
                    if (delete != null)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, message = "Label has been Deleted Successfully" });
                    }
                    else
                    {
                        return this.NotFound(new { isSuccess = false, message = "Label was not found" });
                    }
                }
                catch (Exception e)
                {
                    return this.BadRequest(new { Status = 401, isSuccess = false, Message = e.InnerException.Message });
                }
            }

            /// <summary>
            /// Created GetAll api
            /// </summary>
            /// <returns></returns>
            [HttpGet("GetAll")]
            public IActionResult GetAllLabels()
            {
                try
                {
                    var labels = labelBL.GetAllLabels();
                    if (labels != null)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, Message = " All labels were found Successfully!", data = labels });
                    }
                    else
                    {
                        return this.NotFound(new { isSuccess = false, Message = "No label found" });
                    }
                }
                catch (Exception e)
                {
                    return this.BadRequest(new { Status = 401, isSuccess = false, Message = e.InnerException.Message });
                }
            }

        }
    }
}
