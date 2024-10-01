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
    public class ApiWorkTasksController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;
        private readonly UserManager<User> _userManager;
        private readonly PublicDTOBLLMapper<WorkTask, App.BLL.DTO.Entities.WorkTask> _mapper;

        public ApiWorkTasksController(AppDbContext context, IAppBLL bll, UserManager<User> userManager, IMapper autoMapper)
        {
            _context = context;
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBLLMapper<WorkTask, App.BLL.DTO.Entities.WorkTask>(autoMapper);
        }

        // GET: api/WorkTasks
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<WorkTask>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<WorkTask>>> GetWorkTasks()
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
            return Ok((await _bll.WorkTasks.GetAllAsync(userId)).Select(w => _mapper.Map(w)).ToList());
        }

        // GET: api/WorkTasks
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<WorkTask>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<WorkTask>>> GetAllUnPlannedWorkTasksAsync()
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
            return Ok((await _bll.WorkTasks.GetAllUnPlannedAsync(userId)).Select(w => _mapper.Map(w)).ToList());
        }

        // GET: api/WorkTasks
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<WorkTask>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<WorkTask>>> GetAllSortedOfSubjectWorkTasksAsync([FromRoute]Guid id)
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
            return Ok((await _bll.WorkTasks.GetAllSortedOfSubjectAsync(userId, id)).Select(w => _mapper.Map(w)).ToList());
        }
        // GET: api/WorkTasks
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<WorkTask>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<WorkTask>>> GetAllPublicSortedWorkTasksAsync()
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
            return Ok((await _bll.WorkTasks.GetAllPublicSortedAsync(userId)).Select(w => _mapper.Map(w)).ToList());
        }

        // GET: api/WorkTasks
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<WorkTask>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<WorkTask>>> GetAllPublicSortedOfSubjectWorkTasksAsync([FromRoute]Guid id)
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
            return Ok((await _bll.WorkTasks.GetAllPublicSortedOfSubjectAsync(userId, id)).Select(w => _mapper.Map(w)).ToList());
        }
        // GET: api/WorkTasks
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<WorkTask>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<WorkTask>>> GetAllChosenSortedWorkTasksAsync()
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
            return Ok((await _bll.WorkTasks.GetAllChosenSortedAsync(userId)).Select(w => _mapper.Map(w)).ToList());
        }

       // GET: api/WorkTasks
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<List<WorkTask>>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<WorkTask>>> GetAllChosenSortedOfSubjectWorkTasksAsync([FromRoute] Guid id)
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
            return Ok((await _bll.WorkTasks.GetAllChosenSortedOfSubjectAsync(userId, id)).Select(w => _mapper.Map(w)).ToList());
        }


        // GET: api/WorkTasks/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<WorkTask>((int) HttpStatusCode.OK)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<WorkTask>> GetWorkTask([FromRoute] Guid id)
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
            var workTask = await _bll.WorkTasks.FirstOrDefaultAsync(id, userId);

            if (workTask == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(workTask));
        }

        // PUT: api/WorkTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateWorkTask([FromBody] WorkTaskCreateUpdate workTaskCreateUpdate)
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
                _bll.WorkTasks.Update(_mapper.Map(workTaskCreateUpdate.WorkTask));
                await _bll.SaveChangesAsync();
                
                var workTaskRoles = (await _bll.WorkTaskRoles.GetAllOfWorkTaskAsync(workTaskCreateUpdate.WorkTask.Id, userId)).ToList();
                foreach (var role in workTaskCreateUpdate.Roles ?? [])
                {
                    var workTaskRole = workTaskRoles.Find(w => w.RoleId.Equals(role));
                    if (workTaskRole != default)
                    {
                        if (workTaskRole.AccessType != workTaskCreateUpdate.AccessType)
                        {
                            workTaskRole.AccessType = workTaskCreateUpdate.AccessType;
                            workTaskRole.WorkTask = null;
                            workTaskRole.Role = null;
                            _bll.WorkTaskRoles.Update(workTaskRole);
                        }
                    }
                    else
                    {
                        _bll.WorkTaskRoles.Add(new WorkTaskRole()
                        {
                            Id = Guid.NewGuid(),
                            AccessType = workTaskCreateUpdate.AccessType,
                            RoleId = role,
                            WorkTaskId = workTaskCreateUpdate.WorkTask.Id
                        });
                    }
                }

                foreach (var roleId in workTaskRoles.Select(w => w.RoleId))
                {
                    if (!(workTaskCreateUpdate.Roles ?? []).ToList().Exists(r => r.Equals(roleId)))
                    {
                        var workTaskRole = workTaskRoles.Find(w => w.RoleId.Equals(roleId) && workTaskCreateUpdate.WorkTask.Id.Equals(w.WorkTaskId));
                        await _bll.WorkTaskRoles.RemoveAsync(workTaskRole!);
                    }
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

        // POST: api/WorkTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<WorkTask>((int) HttpStatusCode.Created)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<WorkTask>> PostWorkTask([FromBody] WorkTaskCreateUpdate workTaskCreateUpdate)
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

            workTaskCreateUpdate.WorkTask.Id = Guid.NewGuid();
            workTaskCreateUpdate.WorkTask.CreatedBy = userId;
            _bll.WorkTasks.Add(_mapper.Map(workTaskCreateUpdate.WorkTask));
            await _bll.SaveChangesAsync();
            foreach (var role in workTaskCreateUpdate.Roles ?? [])
            {
                _bll.WorkTaskRoles.Add(new WorkTaskRole()
                {
                    Id = Guid.NewGuid(),
                    AccessType = workTaskCreateUpdate.AccessType,
                    RoleId = role,
                    WorkTaskId = workTaskCreateUpdate.WorkTask.Id
                });
            }
                
            _bll.UserWorkTasks.Add(new UserWorkTask()
            {
                Id = Guid.NewGuid(),
                Status = EStatus.Claimed,
                UserId = userId,
                WorkTaskId = workTaskCreateUpdate.WorkTask.Id
            });
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetWorkTask", new { id = workTaskCreateUpdate.WorkTask.Id }, workTaskCreateUpdate.WorkTask);
        }

        // DELETE: api/WorkTasks/5
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteWorkTask(Guid id)
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
            var workTask = await _bll.WorkTasks.FirstOrDefaultAsync(id, userId);
            if (workTask == null)
            {
                return NotFound();
            }

            await _bll.WorkTasks.RemoveAsync(workTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
