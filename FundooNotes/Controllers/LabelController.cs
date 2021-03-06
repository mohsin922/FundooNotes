namespace FundooNotes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLayer.Interfaces;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;
    using RepositoryLayer.Context;
    using RepositoryLayer.Entities;


    [Route("api/[controller]")]
    [ApiController]
    [Authorize]  ///user to grant and restrict permissions on Web pages.
    public class LabelsController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        private readonly FundooContext fundooContext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        public LabelsController(ILabelBL labelBL, FundooContext fundooContext, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.labelBL = labelBL;
            this.fundooContext = fundooContext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
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
        /// <param name="NotesId">NotesId</param>
        /// <returns>returns</returns>
        [HttpGet("{NotesId}/Get")]
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
                {
                    return this.NotFound(new { isSuccess = false, message = "Specific label was not Found!" });
                }
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
        /// API for Deleting a label by labelID
        /// </summary>
        /// <param name="labelID">labelId</param>
        /// <returns>returns</returns>
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
                var labels = this.labelBL.GetAllLabels();
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

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "LabelsList";
            string serializedLabelsList;
            var LabelsList = new List<Label>();
            var redisLabelsList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelsList != null)
            {
                serializedLabelsList = Encoding.UTF8.GetString(redisLabelsList);
                LabelsList = JsonConvert.DeserializeObject<List<Label>>(serializedLabelsList);
            }
            else
            {
                LabelsList = await fundooContext.LabelTable.ToListAsync();  // Comes from Microsoft.EntityFrameworkCore Namespace
                serializedLabelsList = JsonConvert.SerializeObject(LabelsList);
                redisLabelsList = Encoding.UTF8.GetBytes(serializedLabelsList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelsList, options);
            }

            return Ok(LabelsList);
        }
    }
}

