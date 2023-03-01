using MediatR;
using PlatformService.Models;

namespace PlatformService.Application.Commands
{
    public class CreatePlatformRequest : IRequest<ApiResponse<CreatePlatformRequest>>
    {
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Cost { get; set; }
    }
}
