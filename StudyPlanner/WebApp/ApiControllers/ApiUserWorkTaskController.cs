using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Identity;
using App.DTO.v1_0;
using App.DTO.v1_0.ManyToMany;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Helpers;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // "Identity.application, Bearer"
    public class ApiUserWorkTaskController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<User> _userManager;
        private readonly PublicDTOBLLMapper<UserWorkTask, App.BLL.DTO.ManyToMany.UserWorkTask> _mapper;

        public ApiUserWorkTaskController(IAppBLL bll, UserManager<User> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBLLMapper<UserWorkTask, App.BLL.DTO.ManyToMany.UserWorkTask>(autoMapper);
        }
        
        // GET: api/TimeWindows
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<UserWorkTask>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<UserWorkTask>>> GetUserWorkTasks()
        {
            var userIdStr = _userManager.GetUserId(User);
            if (userIdStr == null)
            {
                return BadRequest(
                    new RestApiErrorResponse()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Error = "Invalid refresh token"
                    }
                );
            }

            if (!Guid.TryParse(userIdStr, out var userId))
            {
                return BadRequest("Deserialization error");
            }
            return Ok((await _bll.UserWorkTasks.GetAllAsync(userId)).Select(t => _mapper.Map(t)).ToList());
        }
        
        // GET: api/TimeWindows
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<UserWorkTask>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<UserWorkTask>> GetUserWorkTask([FromRoute] Guid id)
        {
            var userIdStr = _userManager.GetUserId(User);
            if (userIdStr == null)
            {
                return BadRequest(
                    new RestApiErrorResponse()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Error = "Invalid refresh token"
                    }
                );
            }

            if (!Guid.TryParse(userIdStr, out var userId))
            {
                return BadRequest("Deserialization error");
            }

            var res = await _bll.UserWorkTasks.FirstOrDefaultAsync(id, userId);
            return Ok(res);
        }
        
        // PUT: api/Subjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> UpdateUserWorkTask([FromBody] UserWorkTask userWorkTask)
        {
            var userIdStr = _userManager.GetUserId(User);
            if (userIdStr == null)
            {
                return BadRequest(
                    new RestApiErrorResponse()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Error = "Invalid refresh token"
                    }
                );
            }

            if (!Guid.TryParse(userIdStr, out var userId))
            {
                return BadRequest("Deserialization error");
            }
            try
            {
                var bllUserWorkTask = _mapper.Map(userWorkTask);
                bllUserWorkTask!.UserId = userId;
                _bll.UserWorkTasks.Update(bllUserWorkTask);
                var timeWindow = await _bll.TimeWindows.FindByWorkTaskIdAsync(userWorkTask.WorkTaskId, userId);
                if (timeWindow != null)
                {
                    var timeExpectancy = (await _bll.WorkTasks.FirstOrDefaultAsync(userWorkTask.WorkTaskId, userId))!.TimeExpectancy ?? TimeSpan.Zero;
                    var timeSpent = (await _bll.UserWorkTasks.GetFirstOfWorkTaskAsync(userWorkTask.WorkTaskId, userId))!.TimeSpent ?? TimeSpan.Zero;
                    TimeSpan totalTime;
                    if (timeExpectancy < (userWorkTask.TimeSpent ?? TimeSpan.Zero))
                    {
                        if (timeExpectancy > timeSpent)
                        {
                            totalTime = timeExpectancy - timeSpent;
                        }
                        else
                        {
                            totalTime = TimeSpan.Zero;
                        }
                    }
                    else
                    {
                        totalTime = (userWorkTask.TimeSpent ?? TimeSpan.Zero) - timeSpent;
                    }
                    timeWindow!.FreeTime = timeWindow.FreeTime + totalTime;
                    _bll.TimeWindows.Update(timeWindow);
                
                }
                await _bll.SaveChangesAsync();
                
            }
            catch (DbUpdateConcurrencyException)
            {
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TimeWindows
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<UserWorkTask>((int) HttpStatusCode.Created)]
        public async Task<ActionResult<UserWorkTask>> PostUserWorkTask([FromBody] UserWorkTask userWorkTask)
        {
            var userIdStr = _userManager.GetUserId(User);
            if (userIdStr == null)
            {
                return BadRequest(
                    new RestApiErrorResponse()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Error = "Invalid refresh token"
                    }
                );
            }

            if (!Guid.TryParse(userIdStr, out var userId))
            {
                return BadRequest("Deserialization error");
            }

            var bllUserWorkTask = _mapper.Map(userWorkTask);
            bllUserWorkTask!.Id = Guid.NewGuid();
            bllUserWorkTask.UserId = userId;
            
            _bll.UserWorkTasks.Add(bllUserWorkTask);
            await _bll.SaveChangesAsync();

            return Created();
        }

        // DELETE: api/TimeWindows/5
        [HttpDelete("{workTaskId}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.NotFound)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteUserWorkTask([FromRoute] Guid workTaskId)
        {
            var userIdStr = _userManager.GetUserId(User);
            if (userIdStr == null)
            {
                return BadRequest(
                    new RestApiErrorResponse()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Error = "Invalid refresh token"
                    }
                );
            }

            if (!Guid.TryParse(userIdStr, out var userId))
            {
                return BadRequest("Deserialization error");
            }
            var userWorkTask = await _bll.UserWorkTasks.GetFirstOfWorkTaskAsync(workTaskId, userId);
            if (userWorkTask == null)
            {
                return NotFound("Invalid workTask id!");
            }

            await _bll.UserWorkTasks.RemoveAsync(userWorkTask);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
