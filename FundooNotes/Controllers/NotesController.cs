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

        /// <summary>
        /// Constructor
        /// </summary>
        public NotesController(INoteBL notebl, FundooContext funContext)
        {
            this.notebl = notebl;
        }


        [HttpPost("CreateNote")]
        public IActionResult CreateNote(NoteModel noteModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.notebl.CreateNote(noteModel, userId))
                {
                    return this.Ok(new { status = 200, isSuccess = true, message = " Note was created Successfully! " });
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

        [HttpGet("ShowAllNotes")]
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

        [HttpGet("ShowNote")]
        public IActionResult RetrieveNote(int NotesId)
        {
            try
            {
                long note = Convert.ToInt32(User.Claims.FirstOrDefault(X => X.Type == "Id").Value);
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

        [HttpPut("UpdateNote")]
        public IActionResult UpdateNote(NoteModel updateNoteModel)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(X => X.Type == "Id").Value);
                var result = this.notebl.UpdateNote(updateNoteModel, userid);
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

    }
}