using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.DTO.v1_0.Entities;
using App.Domain.Identity;
using App.BLL.DTO.ManyToMany;
using App.DTO.v1_0;
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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // "Identity.application, Bearer"
    public class ApiEwentsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAppBLL _bll;
        private readonly PublicDTOBLLMapper<App.DTO.v1_0.Entities.Ewent, App.BLL.DTO.Entities.Ewent> _mapper;

        public ApiEwentsController(AppDbContext context, IAppBLL bll, UserManager<User> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBLLMapper<App.DTO.v1_0.Entities.Ewent, App.BLL.DTO.Entities.Ewent>(autoMapper);
        }

        // GET: api/Ewents
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<Ewent>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<Ewent>>> GetEvents()
        {
            var userIdStr = _userManager.GetUserId(User);
            if (userIdStr == null)
            {
                return BadRequest(
                    new RestApiErrorResponse()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Error = "Invalid jwt"
                    }
                );
            }

            if (!Guid.TryParse(userIdStr, out var userId))
            {
                return BadRequest("Deserialization error");
            }
            return Ok((await _bll.Ewents.GetAllAsync(userId)).Select(e => _mapper.Map(e)).ToList());
        }
        
        // GET: api/Ewents
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<Ewent>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<Ewent>>> GetPublicEvents()
        {
            var userIdStr = _userManager.GetUserId(User);
            if (userIdStr == null)
            {
                return BadRequest(
                    new RestApiErrorResponse()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Error = "Invalid jwt"
                    }
                );
            }

            if (!Guid.TryParse(userIdStr, out var userId))
            {
                return BadRequest("Deserialization error");
            }
            return Ok((await _bll.Ewents.GetAllAsync(userId)).Select(e => _mapper.Map(e)).ToList());
        }
        
        // GET: api/Ewents
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<Ewent>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<Ewent>>> GetChosenEvents()
        {
            var userIdStr = _userManager.GetUserId(User);
            if (userIdStr == null)
            {
                return BadRequest(
                    new RestApiErrorResponse()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Error = "Invalid jwt"
                    }
                );
            }

            if (!Guid.TryParse(userIdStr, out var userId))
            {
                return BadRequest("Deserialization error");
            }
            return Ok((await _bll.Ewents.GetAllAsync(userId)).Select(e => _mapper.Map(e)).ToList());
        }

        // GET: api/Ewents/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<Ewent>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<Ewent>> GetEvent([FromRoute] Guid id)
        {
            var userIdStr = _userManager.GetUserId(User);
            if (userIdStr == null)
            {
                return BadRequest(
                    new RestApiErrorResponse()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Error = "Invalid jwt"
                    }
                );
            }

            if (!Guid.TryParse(userIdStr, out var userId))
            {
                return BadRequest("Deserialization error");
            }
            var ewent = await _bll.Ewents.FirstOrDefaultAsync(id, userId);

            if (ewent == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(ewent));
        }

        // PUT: api/Ewents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateEvent([FromBody] Ewent ewent)
        {

            _bll.Ewents.Update(_mapper.Map(ewent));

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

        // POST: api/Ewents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<Ewent>((int) HttpStatusCode.Created)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Ewent>> PostEvent([FromBody] Ewent ewent)
        {
            var userIdStr = _userManager.GetUserId(User);
            if (userIdStr == null)
            {
                return BadRequest(
                    new RestApiErrorResponse()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Error = "Invalid jwt"
                    }
                );
            }

            if (!Guid.TryParse(userIdStr, out var userId))
            {
                return BadRequest("Deserialization error");
            }
            ewent.Id = Guid.NewGuid();
            _bll.Ewents.Add(_mapper.Map(ewent));
            await _bll.SaveChangesAsync();
            _bll.UserEwents.Add(new UserEwent()
            {
                Id = Guid.NewGuid(),
                EwentId = ewent.Id,
                UserId = userId
            });
            await _bll.SaveChangesAsync();
            return CreatedAtAction("GetEvent", new { id = ewent.Id }, ewent);
        }

        // DELETE: api/Ewents/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteEvent([FromRoute] Guid id)
        {
            var userIdStr = _userManager.GetUserId(User);
            if (userIdStr == null)
            {
                return BadRequest(
                    new RestApiErrorResponse()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Error = "Invalid jwt"
                    }
                );
            }

            if (!Guid.TryParse(userIdStr, out var userId))
            {
                return BadRequest("Deserialization error");
            }
            var ewent = await _bll.Ewents.FirstOrDefaultAsync(id, userId);
            if (ewent == null)
            {
                return NotFound();
            }
            var userEwent = (await _bll.UserEwents.GetAllAsync(userId)).FirstOrDefault(u => u.EwentId.Equals(ewent.Id) && !u.UserId.Equals(userId));  // Todo: FindByEwentId(ewent.Id);
            if (userEwent != null)
            {
                return BadRequest("Event cannot be deleted when in usage by someone!");
            }

            await _bll.Ewents.RemoveAsync(ewent);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
