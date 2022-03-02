//using BusinessLayer.Interfaces;
//using CommonLayer.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace FundooNotes.Controllers
//{
//    public class NoteController : Controller
//    {
//        [Route("api/[controller]")]
//        [ApiController]
//        [Authorize]
//        public class NotesController : ControllerBase
//        {
//            private readonly INoteBL Nbl;

//            /// <summary>
//            /// Construction function
//            /// </summary>
//            /// <param name="Nbl"></param>
//            /// <param name="funContext"></param>
//            public NotesController(INoteBL Nbl)
//            {
//                this.Nbl = Nbl;
//            }
//            /// <summary>
//            /// Create note api
//            /// </summary>
//            /// <param name="noteModel"></param>
//            /// <returns></returns>
//            [HttpPost("CreateNote")]
//            public IActionResult CreateNote(NoteModel noteModel)
//            {
//                try
//                {
//                    long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
//                    if (this.Nbl.CreateNote(noteModel, userid))
//                    {
//                        return this.Ok(new { status = 200, isSuccess = true, message = "Note created" });
//                    }
//                    else
//                    {
//                        return this.BadRequest(new { status = 401, isSuccess = false, message = "Failed to create note" });
//                    }
//                }
//                catch (Exception e)
//                {
//                    return this.BadRequest(new { status = 400, isSuccess = false, Message = e.InnerException.Message });
//                }
//            }
//        }
//    }
//}
