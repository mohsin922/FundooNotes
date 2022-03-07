namespace Microsoft.Xxx
{
    using System;
    using BusinessLayer.Interfaces;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RepositoryLayer.Context;
    using RepositoryLayer.entities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    namespace FundooNotes.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        [Authorize]
        public class CollabController : ControllerBase
        {
            private readonly ICollabBL collabBL;
            private readonly FundooContext fundooContext;
            public CollabController(ICollabBL collabBL, FundooContext fundooContext)
            {
                this.collabBL = collabBL;
                this.fundooContext = fundooContext;
            }

            [HttpPost]
            [Route("Add")]
            public IActionResult AddCollaboration(CollabModel collabModel)
            {
                try
                {
                    long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                    var collab = this.fundooContext.NotesTable.Where(X => X.NotesId == collabModel.NotesId).SingleOrDefault();
                    if (collab.Id == userId)
                    {
                        var result = this.collabBL.AddCollaboration(collabModel);
                        if (result)
                        {
                            return this.Ok(new { status = 200, isSuccess = true, message = "Collaborated successfully!", data = result });
                        }
                        return this.BadRequest(new { status = 400, isSucess = false, message = "Failed to collaborate!" });
                    }
                    else
                    {
                        return this.Unauthorized(new { status = 401, isSucess = false, Message = "Not Authorized to Add Collaboration!" });
                    }
                }
                catch (Exception e)
                {
                    return this.BadRequest(new { Status = 401, isSuccess = false, Message = e.Message, InnerException = e.InnerException });
                }

            }

            [HttpGet("Get")]
            public IActionResult GetAllCollabs(long NotesId)
            {
                try
                {
                    long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                    var result = collabBL.GetAllCollabs(NotesId);
                    if (result != null)
                    {
                        return this.Ok(new { isSuccess = true, message = " All Collaborators found Successfully", data = result });

                    }
                    else
                    {
                        return this.NotFound(new { isSuccess = false, message = "No Collaborator  Found" });
                    }
                }

                catch (Exception ex)
                {
                    return this.BadRequest(new { Status = 401, isSuccess = false, message = ex.InnerException.Message });
                }
            }

            [HttpDelete("Remove")]
            public IActionResult RemoveCollab(long collabID)
            {
                try
                {
                    long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                    var result = this.collabBL.RemoveCollab(collabID);
                    if (result != null)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, Message = "Removed collab for User sucessfully!", data = collabID });
                    }
                    return this.BadRequest(new { status = 400, isSuccess = false, Message = "Sorry! Couldn't remove the User from Collaboration. " });
                }
                catch (Exception e)
                {
                    return this.BadRequest(new { status = 400, isSuccess = false, Message = e.InnerException.Message });
                }
            }

            [HttpGet("GetAll")]
            public IActionResult GetAllCollabs()
            {
                try
                {
                    long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                    var collabs = collabBL.GetAllCollabs();
                    if (collabs != null)
                    {
                        return this.Ok(new { isSuccess = true, message = " All Collaborators found Successfully", data = collabs });

                    }
                    else
                    {
                        return this.NotFound(new { isSuccess = false, message = "No Collaborator  Found" });
                    }
                }
                catch (Exception ex)
                {
                    return this.BadRequest(new { Status = 401, isSuccess = false, message = ex.InnerException.Message });
                }
            }
        }
    }
}
