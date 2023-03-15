using MediatR;
using PlatformService.Models;

namespace PlatformService.Application.Commands
{
    public class PublishPlatformRequest
    {
        public string Name { get; set; }
        public string Event { get; set; }
    }
}
