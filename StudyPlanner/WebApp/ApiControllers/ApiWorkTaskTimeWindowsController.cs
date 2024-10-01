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
using WebApp.Helpers;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // "Identity.application, Bearer"
    public class ApiWorkTaskTimeWindowsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<User> _userManager;
        private readonly PublicDTOBLLMapper<WorkTaskTimeWindow, App.BLL.DTO.ManyToMany.WorkTaskTimeWindow> _mapper;

        public ApiWorkTaskTimeWindowsController(IAppBLL bll, UserManager<User> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBLLMapper<WorkTaskTimeWindow, App.BLL.DTO.ManyToMany.WorkTaskTimeWindow>(autoMapper);
        }
        
        // GET: api/TimeWindows
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<WorkTaskTimeWindow>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<WorkTaskTimeWindow>>> GetWorkTaskTimeWindows()
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
            return Ok((await _bll.WorkTaskTimeWindows.GetAllSortedAsync(userId)).Select(t => _mapper.Map(t)).ToList());
        }

        // POST: api/TimeWindows
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<WorkTaskTimeWindow>((int) HttpStatusCode.Created)]
        public async Task<ActionResult<WorkTaskTimeWindow>> PostWorkTaskTimeWindow([FromBody] WorkTaskTimeWindow workTaskTimeWindow)
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
            workTaskTimeWindow.Id = Guid.NewGuid();
            _bll.WorkTaskTimeWindows.Add(_mapper.Map(workTaskTimeWindow));
            var timeWindow = await _bll.TimeWindows.FirstOrDefaultAsync(workTaskTimeWindow.TimeWindowId, userId);
            var timeExpectancy = (await _bll.WorkTasks.FirstOrDefaultAsync(workTaskTimeWindow.WorkTaskId, userId))!.TimeExpectancy ?? TimeSpan.Zero;
            var timeSpent = (await _bll.UserWorkTasks.GetFirstOfWorkTaskAsync(workTaskTimeWindow.WorkTaskId, userId))!.TimeSpent ?? TimeSpan.Zero;
            var totalTime = TimeSpan.Zero < timeExpectancy - timeSpent ? timeExpectancy - timeSpent : TimeSpan.Zero;
            
            timeWindow!.FreeTime = timeWindow.FreeTime - totalTime;
            _bll.TimeWindows.Update(timeWindow);
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
        public async Task<IActionResult> DeleteWorkTaskTimeWindow([FromRoute] Guid workTaskId)
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
            var workTaskTimeWindow = await _bll.WorkTaskTimeWindows.GetFirstOfWorkTaskAsync(workTaskId, userId);
            if (workTaskTimeWindow == null)
            {
                return NotFound("Invalid WorkTaskTimeWindow id!");
            }
            var timeWindow = await _bll.TimeWindows.FirstOrDefaultAsync(workTaskTimeWindow.TimeWindowId, userId);
            var timeExpectancy = (await _bll.WorkTasks.FirstOrDefaultAsync(workTaskTimeWindow.WorkTaskId, userId))!.TimeExpectancy ?? TimeSpan.Zero;
            var timeSpent = (await _bll.UserWorkTasks.GetFirstOfWorkTaskAsync(workTaskTimeWindow.WorkTaskId, userId))!.TimeSpent ?? TimeSpan.Zero;
            var totalTime = TimeSpan.Zero < timeExpectancy - timeSpent ? timeExpectancy - timeSpent : TimeSpan.Zero;
            
            timeWindow!.FreeTime = timeWindow.FreeTime + totalTime;
            _bll.TimeWindows.Update(timeWindow);
            await _bll.WorkTaskTimeWindows.RemoveAsync(workTaskTimeWindow);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
