using System.Net;
using App.BLL.DTO.ManyToMany;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using App.DTO.v1_0;
using App.DTO.v1_0.Entities;
using User = App.Domain.Identity.User;
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
    public class ApiCurriculumsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAppBLL _bll;
        private readonly PublicDTOBLLMapper<App.DTO.v1_0.Entities.Curriculum, App.BLL.DTO.Entities.Curriculum> _mapper;

        public ApiCurriculumsController(AppDbContext context, IAppBLL bll, UserManager<User> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBLLMapper<App.DTO.v1_0.Entities.Curriculum, App.BLL.DTO.Entities.Curriculum>(autoMapper);
        }

        // GET: api/Curriculums
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<Curriculum>>((int) HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Curriculum>>> GetCurriculums()
        {
            return Ok((await _bll.Curriculums.GetAllSortedAsync()).Select(c => _mapper.Map(c)).ToList());
        }

        // GET: api/Curriculums/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<Curriculum>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<Curriculum>> GetCurriculum(Guid id)
        {
            var curriculum = await _bll.Curriculums.FirstOrDefaultAsync(id);

            if (curriculum == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(curriculum));
        }

        // PUT: api/Curriculums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<Curriculum>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<Curriculum>> UpdateCurriculum([FromBody] Curriculum curriculum)
        {
            _bll.Curriculums.Update(_mapper.Map(curriculum));

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

            return Ok(_mapper.Map(await _bll.Curriculums.FirstOrDefaultAsync(curriculum.Id)));
        }

        // POST: api/Curriculums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<Curriculum>((int) HttpStatusCode.Created)]
        public async Task<ActionResult<Curriculum>> PostCurriculum([FromBody]Curriculum curriculum)
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
            curriculum.Id = Guid.NewGuid();
            _bll.Curriculums.Add(_mapper.Map(curriculum));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCurriculum", new { id = curriculum.Id }, curriculum);
        }

        // DELETE: api/Curriculums/5
        [HttpDelete("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteCurriculum([FromRoute]Guid id)
        {
            var curriculum = await _bll.Curriculums.FirstOrDefaultAsync(id);
            if (curriculum == null)
            {
                return NotFound();
            }

            await _bll.Curriculums.RemoveAsync(curriculum);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool CurriculumExists(Guid id)
        {
            return _bll.Curriculums.Exists(id);
        }
    }
}
