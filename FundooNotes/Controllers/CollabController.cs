﻿namespace Microsoft.Xxx
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
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Caching.Distributed;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    namespace FundooNotes.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        [Authorize]
        public class CollabController : ControllerBase
        {
            private readonly ICollabBL collabBL;
            private readonly FundooContext fundooContext;
            private readonly IMemoryCache memoryCache;
            private readonly IDistributedCache distributedCache;
            public CollabController(ICollabBL collabBL, FundooContext fundooContext, IMemoryCache memoryCache, IDistributedCache distributedCache)
            {
                this.collabBL = collabBL;
                this.fundooContext = fundooContext;
                this.memoryCache = memoryCache;
                this.distributedCache = distributedCache;
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

            [HttpGet("redis")]
            public async Task<IActionResult> GetAllCollaboratorUsingRedisCache()
            {
                var cacheKey = "CollabsList";
                string serializedList;
                var CollabsList = new List<Collaborator>();
                var redisCollabsList = await distributedCache.GetAsync(cacheKey);
                if (redisCollabsList != null)
                {
                    serializedList = Encoding.UTF8.GetString(redisCollabsList);
                    CollabsList = JsonConvert.DeserializeObject<List<Collaborator>>(serializedList);
                }
                else
                {
                    CollabsList = await fundooContext.CollabTable.ToListAsync();  // Comes from Microsoft.EntityFrameworkCore Namespace
                    serializedList = JsonConvert.SerializeObject(CollabsList);
                    redisCollabsList = Encoding.UTF8.GetBytes(serializedList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await distributedCache.SetAsync(cacheKey, redisCollabsList, options);
                }
                return Ok(CollabsList);
            }
        }
    }
}
