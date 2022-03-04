using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteBL notebl;
        private readonly FundooContext fundooContext;

        /// <summary>
        /// Constructor
        /// </summary>
        public NotesController(INoteBL notebl, FundooContext fundooContext)
        {
            this.notebl = notebl;
            this.fundooContext = fundooContext;
        }


        [HttpPost("Create")]
        public IActionResult CreateNote(NoteModel noteModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var note =this.notebl.CreateNote(noteModel, userId);
                if(note)
                {
                    return this.Ok(new { status = 200, isSuccess = true, message = " Note was created Successfully! ",data = note});
                }
                else
                {
                    return this.BadRequest(new { status = 401, isSuccess = false, message = "Unsuccessful!" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("ShowAll")]
        public IActionResult RetrieveAllNotes(long userId)
        {
            try
            {
                var notes = notebl.RetrieveAllNotes(userId);
                if (notes != null)
                {
                    return this.Ok(new { isSuccess = true, message = " All Notes Displayed Successfully!", data = notes });

                }
                else
                {
                    return this.NotFound(new { isSuccess = false, message = "Notes not Found!" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = 401, isSuccess = false, Message = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpGet("Show")]
        public IActionResult RetrieveNote(int NotesId)
        {
            try
            {
                List<Note> notes = this.notebl.RetrieveNote(NotesId);
                if (notes != null)
                {
                    return this.Ok(new { isSuccess = true, message = "Note found Successfully!", data = notes });
                }
                else
                    return this.NotFound(new { isSuccess = false, message = "Note not Found!" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = 401, isSuccess = false, Message = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpPut("Update")]
        public IActionResult UpdateNote(NoteModel updateNoteModel,long NotesId)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(X => X.Type == "Id").Value);
                var result = this.notebl.UpdateNote(updateNoteModel, NotesId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Notes Updated Successful", data = result });
                }
                else
                {
                    return this.NotFound(new { isSuccess = false, message = "No Notes Found" });
                }
            }
            catch (Exception)
            {
                return this.BadRequest(new { success = false, message = "Notes Not Updated" });
            }
        }


        [HttpDelete("Delete")]
        public IActionResult DeleteNotes(long NotesId)
        {
            try
            {
                var delete = this.notebl.DeleteNotes(NotesId);
                if (delete != null)
                {
                    return this.Ok(new { success = true, message = "Note was Deleted Successfully!" });
                }
                else
                {
                    return this.NotFound(new { isSuccess = false, message = "Note was not Deleted!" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = 401, isSuccess = false, Message = e.Message, InnerException = e.InnerException });
            }
        }
        /// <summary>
        /// IsArchive method
        /// </summary>
        /// <param name="id">Mandatory</param>
        /// <returns>IActionResult</returns>
        [HttpPut]
        [Route("IsArchive")]
        public IActionResult IsArchive(long NotesId)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notebl.IsArchive(NotesId);
                if (result == true)
                {
                    return this.Ok(new { status = 200, isSuccess = true, Message = "Note has been Archived",data=result });
                }
                if (result == false)
                {
                    return this.Ok(new { status = 200, isSuccess = true, Message = "Note has been Un-Archived" });
                }
                return this.BadRequest(new { status = 400, isSuccess = false, Message = "Error" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message, InnerException = e.InnerException });
            }
        }

        /// <summary>
        /// IsPin method
        /// </summary>
        /// <param name="id">Mandatory</param>
        /// <returns>IActionResult</returns>
        [HttpPut]
        [Route("Pin")]
        public IActionResult Pin(long NotesId)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notebl.Pin(NotesId);
                if (result == true)
                {
                    return this.Ok(new { status = 200, isSuccess = true, Message = "Note has been Pinned!", data = result });
                }
                if (result == false)
                {
                    return this.Ok(new { status = 200, isSuccess = true, Message = "Note has been Un-Pinned" });
                }
                return this.BadRequest(new { status = 400, isSuccess = false, Message = "Error" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message, InnerException = e.InnerException });
            }
        }
        /// <summary>
        /// IsTrash method
        /// </summary>
        /// <param name="id">Mandatory</param>
        /// <returns>IActionResult</returns>
        [HttpPut]
        [Route("IsTrash")]
        public IActionResult IsTrash(long NotesId)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notebl.IsTrash(NotesId);
                if (result == true)
                {
                    return this.Ok(new { status = 200, isSuccess = true, Message = "Note has been Sent To Trash!", data = result });
                }
                if (result == false)
                {
                    return this.Ok(new { status = 200, isSuccess = true, Message = "Note has been Recovered" });
                }
                return this.BadRequest(new { status = 400, isSuccess = false, Message = "Error" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message, InnerException = e.InnerException });
            }
        }

        /// <summary>
        /// Color method
        /// </summary>
        [HttpPut]
        [Route("Color")]
        public IActionResult UpdateColor(string color, long NotesId)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notebl.UpdateColor(color, NotesId);
                if (result != null)
                {
                    return this.Ok(new { isSuccess = true, message = "Color has been Updated!", data = color });
                }
                else
                    return this.BadRequest(new { isSuccess = false, message = " Color has not been Added!" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = 401, isSuccess = false, Message = e.Message });
            }
        }
        /// <summary>
        /// API for adding a background image for a note
        /// </summary>
        /// <param name="imageURL"></param>
        /// <param name="noteid"></param>
        /// <returns></returns>
        [HttpPut("BgImage")]
        public IActionResult UpdateBgImage(IFormFile imageURL, long NotesId)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notebl.UpdateBgImage(imageURL, NotesId);
                if (result)
                {
                    return this.Ok(new { status = 200, isSuccess = true, Message = "BackGround Image has been updated" });
                }
                else
                    return this.BadRequest(new { status = 400, isSuccess = false, Message = "BackGround Image was not updated" });
            }
            catch (Exception e)
            { 
                return this.BadRequest(new { status = 400, isSuccess = false, Message = e.InnerException.Message });
            }
        }
    }
}