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
    public class ApiUserCurriculumController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<User> _userManager;
        private readonly PublicDTOBLLMapper<UserCurriculum, App.BLL.DTO.ManyToMany.UserCurriculum> _mapper;

        public ApiUserCurriculumController(IAppBLL bll, UserManager<User> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBLLMapper<UserCurriculum, App.BLL.DTO.ManyToMany.UserCurriculum>(autoMapper);
        }
        
        // GET: api/TimeWindows
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<UserCurriculum>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<UserCurriculum>> GetUserCurriculum()
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

            return Ok((await _bll.UserCurriculums.GetAllAsync(userId)).Where(u => u.UserId.Equals(userId)).FirstOrDefault());
        }
        
        // PUT: api/Curriculums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> UpdateUserCurriculum([FromBody] UserCurriculum userCurriculum)
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
                var bllUserCurriculum = _mapper.Map(userCurriculum);
                bllUserCurriculum!.UserId = userId;
                _bll.UserCurriculums.Update(bllUserCurriculum);
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
        [ProducesResponseType<UserCurriculum>((int) HttpStatusCode.Created)]
        public async Task<ActionResult<UserCurriculum>> PostUserCurriculum([FromBody] UserCurriculum userCurriculum)
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
            var bllUserCurriculum = _mapper.Map(userCurriculum);
            bllUserCurriculum!.UserId = userId;
            bllUserCurriculum.Id = Guid.NewGuid();
            _bll.UserCurriculums.Add(bllUserCurriculum);
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
        public async Task<IActionResult> DeleteUserCurriculum([FromRoute] Guid id)
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
            var userCurriculum = (await _bll.UserCurriculums.GetAllAsync(userId)).Where(u => u.UserId.Equals(userId)).FirstOrDefault();
            if (userCurriculum == null)
            {
                return NotFound("Invalid userCurriculum id!");
            }

            await _bll.UserCurriculums.RemoveAsync(userCurriculum);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
