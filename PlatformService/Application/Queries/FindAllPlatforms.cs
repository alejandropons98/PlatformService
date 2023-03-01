using MediatR;
using PlatformService.Models;

namespace PlatformService.Application.Queries
{
    public class FindAllPlatformsRequest : IRequest<ApiResponse<IReadOnlyList<FindAllPlatformsRequest>>>
    {
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Cost { get; set; }
    }
}
