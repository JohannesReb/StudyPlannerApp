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
    public class ApiUserSubjectsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<User> _userManager;
        private readonly PublicDTOBLLMapper<UserSubject, App.BLL.DTO.ManyToMany.UserSubject> _mapper;

        public ApiUserSubjectsController(IAppBLL bll, UserManager<User> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBLLMapper<UserSubject, App.BLL.DTO.ManyToMany.UserSubject>(autoMapper);
        }
        
        // GET: api/TimeWindows
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<UserSubject>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<UserSubject>>> GetUserSubjects()
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
            return Ok((await _bll.UserSubjects.GetAllAsync(userId)).Select(t => _mapper.Map(t)).ToList());
        }
        
        // GET: api/TimeWindows
        [HttpGet("{subjectId}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<UserSubject>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<UserSubject>>> GetUserSubject([FromRoute] Guid subjectId)
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

            return Ok(await _bll.UserSubjects.GetFirstOfSubjectAsync(subjectId, userId));
        }
        
        // PUT: api/Subjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> UpdateUserSubject([FromBody] UserSubject userSubject)
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
                var bllUserSubject = _mapper.Map(userSubject);
                bllUserSubject!.UserId = userId;
                _bll.UserSubjects.Update(bllUserSubject);
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
        [ProducesResponseType<UserSubject>((int) HttpStatusCode.Created)]
        public async Task<ActionResult<UserSubject>> PostUserSubject([FromBody] UserSubject userSubject)
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
            var bllUserSubject = _mapper.Map(userSubject);
            bllUserSubject!.UserId = userId;
            bllUserSubject.Id = Guid.NewGuid();
            _bll.UserSubjects.Add(bllUserSubject);
            await _bll.SaveChangesAsync();

            return Created();
        }

        // DELETE: api/TimeWindows/5
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.NotFound)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteUserSubject([FromRoute] Guid id)
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
            var userSubject = await _bll.UserSubjects.FirstOrDefaultAsync(id, userId);
            if (userSubject == null)
            {
                return NotFound("Invalid userSubject id!");
            }

            await _bll.UserSubjects.RemoveAsync(userSubject);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
