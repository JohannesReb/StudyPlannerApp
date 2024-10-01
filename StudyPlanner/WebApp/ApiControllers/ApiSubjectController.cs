using System.Net;
using App.BLL.DTO.ManyToMany;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
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
    public class ApiSubjectController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<User> _userManager;
        private readonly PublicDTOBLLMapper<Subject, App.BLL.DTO.Entities.Subject> _mapper;

        public ApiSubjectController(IAppBLL bll, UserManager<User> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBLLMapper<Subject, App.BLL.DTO.Entities.Subject>(autoMapper);
        }

        // GET: api/Subjects
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<IEnumerable<Subject>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
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
            var res = (await _bll.Subjects.GetAllSortedAsync(userId))
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }
        
        // GET: api/Subjects
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<IEnumerable<Subject>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjectsByModule([FromRoute]Guid id)
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
            var res = (await _bll.Subjects.GetAllSortedOfModuleAsync(userId, id))
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }
        
        // GET: api/Subjects
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<IEnumerable<Subject>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<Subject>>> GetPublicSubjects()
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
            var res = (await _bll.Subjects.GetAllPublicSortedAsync(userId))
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        // GET: api/Subjects
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<IEnumerable<Subject>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<Subject>>> GetChosenSubjects()
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
            var res = (await _bll.Subjects.GetAllChosenSortedAsync(userId))
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        // GET: api/Subjects
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<IEnumerable<Subject>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<Subject>>> GetPublicSubjectsByCurriculum([FromRoute] Guid id)
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
            var res = (await _bll.Subjects.GetAllPublicSortedOfCurriculumAsync(userId, id))
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        // GET: api/Subjects
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<IEnumerable<Subject>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<Subject>>> GetChosenSubjectsByCurriculum([FromRoute] Guid id)
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
            var res = (await _bll.Subjects.GetAllChosenSortedOfCurriculumAsync(userId, id))
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        // GET: api/Subjects/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<Subject>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<Subject>> GetSubject(Guid id)
        {
            var subject = await _bll.Subjects.FirstOrDefaultAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        // PUT: api/Subjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> UpdateSubject([FromBody]SubjectCreateUpdate subjectCreateUpdate)
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
                _bll.Subjects.Update(_mapper.Map(subjectCreateUpdate.Subject));
                await _bll.SaveChangesAsync();
                
                var subjectRoles = (await _bll.SubjectRoles.GetAllOfSubjectAsync(subjectCreateUpdate.Subject.Id, userId)).ToList();
                foreach (var role in subjectCreateUpdate.Roles ?? [])
                {
                    if (subjectRoles.Exists(w => w.RoleId.Equals(role)))
                    {
                        var subjectRole = subjectRoles.Find(w => w.RoleId.Equals(role) && subjectCreateUpdate.Subject.Id.Equals(w.SubjectId));
                        if (subjectRole!.AccessType != subjectCreateUpdate.AccessType)
                        {
                            subjectRole.AccessType = subjectCreateUpdate.AccessType;
                            subjectRole.Subject = null;
                            subjectRole.Role = null;
                            _bll.SubjectRoles.Update(subjectRole);
                        }
                    }
                    else
                    {
                        _bll.SubjectRoles.Add(new SubjectRole()
                        {
                            Id = Guid.NewGuid(),
                            AccessType = subjectCreateUpdate.AccessType,
                            RoleId = role,
                            SubjectId = subjectCreateUpdate.Subject.Id
                        });
                    }
                }

                foreach (var roleId in subjectRoles.Select(w => w.RoleId))
                {
                    if (!(subjectCreateUpdate.Roles ?? []).ToList().Exists(r => r.Equals(roleId)))
                    {
                        var subjectRole = subjectRoles.Find(w => w.RoleId.Equals(roleId) && subjectCreateUpdate.Subject.Id.Equals(w.SubjectId));
                        await _bll.SubjectRoles.RemoveAsync(subjectRole!);
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Subjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<Subject>((int) HttpStatusCode.Created)]
        public async Task<ActionResult<Subject>> PostSubject([FromBody]SubjectCreateUpdate subjectCreateUpdate)
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
            
            subjectCreateUpdate.Subject.Id = Guid.NewGuid();
            subjectCreateUpdate.Subject.CreatedBy = userId;
            _bll.Subjects.Add(_mapper.Map(subjectCreateUpdate.Subject));
            await _bll.SaveChangesAsync();
            foreach (var role in subjectCreateUpdate.Roles ?? [])
            {
                _bll.SubjectRoles.Add(new SubjectRole()
                {
                    Id = Guid.NewGuid(),
                    AccessType = subjectCreateUpdate.AccessType,
                    RoleId = role,
                    SubjectId = subjectCreateUpdate.Subject.Id
                });
            }
                
            _bll.UserSubjects.Add(new UserSubject()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Status = EStatus.Claimed,
                Semester = subjectCreateUpdate.Semester,
                SubjectId = subjectCreateUpdate.Subject.Id
            });
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetSubject", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = subjectCreateUpdate.Subject.Id
            }, subjectCreateUpdate.Subject);
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteSubject(Guid id)
        {
            var subject = await _bll.Subjects.FirstOrDefaultAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _bll.Subjects.Remove(subject);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
