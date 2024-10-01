using System.Net;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class EnumsController : ControllerBase
    {
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<Dictionary<string, int>>((int) HttpStatusCode.OK)]
        public IActionResult GetAccessTypes()
        {
            var items = Enum.GetValues<EAccessType>()
                .ToDictionary(item => item.ToString(), item => (int)item);
            return Ok(items);
        }
        
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<Dictionary<string, int>>((int) HttpStatusCode.OK)]
        public IActionResult GetFields()
        {
            var items = Enum.GetValues<EField>()
                .ToDictionary(item => item.ToString(), item => (int)item);
            return Ok(items);
        }
        
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<Dictionary<string, int>>((int) HttpStatusCode.OK)]
        public IActionResult GetStatuses()
        {
            var items = Enum.GetValues<EStatus>()
                .ToDictionary(item => item.ToString(), item => (int)item);
            return Ok(items);
        }
        
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType<Dictionary<string, int>>((int) HttpStatusCode.OK)]
        public IActionResult GetTaskTypes()
        {
            var items = Enum.GetValues<ETaskType>()
                .ToDictionary(item => item.ToString(), item => (int)item);
            return Ok(items);
        }
    }
    
    
}