using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Application.Commands;
using PlatformService.Application.Queries;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;
using PlatformService.Utils;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBaseCustom
    {
        private readonly IMediator _mediator;

        public PlatformsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<FindAllPlatformsRequest>>>> GetPlatforms()
        {
            var reply = await _mediator.Send(new FindAllPlatformsRequest());
            return Ok(reply);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreatePlatformRequest>>> CreatePlatform(CreatePlatformRequest platform)
        {
            var reply = await _mediator.Send(platform);
            return Ok(reply);
        }
    }
}
