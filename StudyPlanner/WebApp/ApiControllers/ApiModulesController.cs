using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.DTO.v1_0.Entities;
using App.Domain.Identity;
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
    public class ApiModulesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAppBLL _bll;
        private readonly PublicDTOBLLMapper<App.DTO.v1_0.Entities.Module, App.BLL.DTO.Entities.Module> _mapper;

        public ApiModulesController(AppDbContext context, IAppBLL bll, UserManager<User> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBLLMapper<App.DTO.v1_0.Entities.Module, App.BLL.DTO.Entities.Module>(autoMapper);
        }

        // GET: api/Modules
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<Module>>((int) HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Module>>> GetModulesByCurriculum([FromRoute] Guid id)
        {
            return Ok((await _bll.Modules.GetAllSortedOfCurriculumAsync(id)).Select(m => _mapper.Map(m)).ToList());
        }

        // GET: api/Modules/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<Module>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<Module>> GetModule(Guid id)
        {
            var module = await _bll.Modules.FirstOrDefaultAsync(id);

            if (module == null)
            {
                return NotFound();
            }

            return _mapper.Map(module);
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // "Identity.application, Bearer"
        public async Task<IActionResult> UpdateModule(Module module)
        {

            _bll.Modules.Update(_mapper.Map(module));

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

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<Module>((int) HttpStatusCode.Created)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<Module>> PostModule(Module module)
        {
            _bll.Modules.Add(_mapper.Map(module));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetModule", new { id = module.Id }, module);
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteModule(Guid id)
        {
            var module = await _bll.Modules.FirstOrDefaultAsync(id);
            if (module == null)
            {
                return NotFound();
            }

            await _bll.Modules.RemoveAsync(module);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool ModuleExists(Guid id)
        {
            return _bll.Modules.Exists(id);
        }
    }
}
