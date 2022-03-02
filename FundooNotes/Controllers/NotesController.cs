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

    }
}