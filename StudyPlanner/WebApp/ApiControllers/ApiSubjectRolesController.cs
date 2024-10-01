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
    public class ApiSubjectRolesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<User> _userManager;
        private readonly PublicDTOBLLMapper<SubjectRole, App.BLL.DTO.ManyToMany.SubjectRole> _mapper;

        public ApiSubjectRolesController(IAppBLL bll, UserManager<User> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBLLMapper<SubjectRole, App.BLL.DTO.ManyToMany.SubjectRole>(autoMapper);
        }

        // GET: api/TimeWindows
        [HttpGet("{subjectId}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<SubjectRole>>((int)HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<SubjectRole>>> GetSubjectRoles([FromRoute] Guid subjectId)
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

            return Ok((await _bll.SubjectRoles.GetAllOfSubjectAsync(subjectId, userId)).Select(t => _mapper.Map(t)).ToList());
        }
    }
}