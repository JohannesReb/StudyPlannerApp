using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Identity;
using App.DTO.v1_0;
using App.DTO.v1_0.Entities;
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
    public class ApiTimeWindowsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<User> _userManager;
        private readonly PublicDTOBLLMapper<TimeWindow, App.BLL.DTO.Entities.TimeWindow> _mapper;

        public ApiTimeWindowsController(IAppBLL bll, UserManager<User> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBLLMapper<TimeWindow, App.BLL.DTO.Entities.TimeWindow>(autoMapper);
        }

        // GET: api/TimeWindows
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<TimeWindow>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<TimeWindow>>> GetTimeWindows()
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
            return Ok((await _bll.TimeWindows.GetAllAsync(userId)).Select(t => _mapper.Map(t)).ToList());
        }
        
        // GET: api/TimeWindows
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<TimeWindow>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<TimeWindow>>> GetActiveTimeWindows()
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
            return Ok((await _bll.TimeWindows.GetAllActiveSortedAsync(userId)).Select(t => _mapper.Map(t)).ToList());
        }
        
        // GET: api/TimeWindows
        [HttpGet("{userWorkTaskId}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<TimeWindow>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<TimeWindow>>> GetAvailableTimeWindows([FromRoute] Guid userWorkTaskId)
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
            return Ok((await _bll.TimeWindows.GetAllAvailableAsync(userWorkTaskId, userId)).Select(t => _mapper.Map(t)).ToList());
        }

        // GET: api/TimeWindows/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<TimeWindow>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TimeWindow>> GetTimeWindow([FromRoute] Guid id)
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
            var timeWindow = await _bll.TimeWindows.FirstOrDefaultAsync(id, userId);

            if (timeWindow == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(timeWindow));
        }
        
        // GET: api/TimeWindows/5
        [HttpGet("{workTaskId}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<TimeWindow>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TimeWindow>> GetTimeWindowByWorkTaskId([FromRoute] Guid workTaskId)
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
            var timeWindow = await _bll.TimeWindows.FindByWorkTaskIdAsync(workTaskId, userId);

            if (timeWindow == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(timeWindow));
        }

        // PUT: api/TimeWindows/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateTimeWindow([FromBody] TimeWindow timeWindow)
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
            var bllTimeWindow = _mapper.Map(timeWindow);
            bllTimeWindow!.UserId = userId;
            _bll.TimeWindows.Update(bllTimeWindow);

            try
            {
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
        [ProducesResponseType<TimeWindow>((int) HttpStatusCode.Created)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TimeWindow>> PostTimeWindow([FromBody] TimeWindow timeWindow)
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
            var bllTimeWindow = _mapper.Map(timeWindow);
            bllTimeWindow!.Id = Guid.NewGuid();
            bllTimeWindow!.UserId = userId;
            bllTimeWindow.FreeTime = bllTimeWindow.Until - bllTimeWindow.From;
            _bll.TimeWindows.Add(bllTimeWindow);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTimeWindow", new { id = timeWindow.Id }, timeWindow);
        }

        // DELETE: api/TimeWindows/5
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteTimeWindow([FromRoute] Guid id)
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
            var timeWindow = await _bll.TimeWindows.FirstOrDefaultAsync(id, userId);
            if (timeWindow == null)
            {
                return NotFound("Invalid Time Window id!");
            }

            await _bll.TimeWindows.RemoveAsync(timeWindow);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
