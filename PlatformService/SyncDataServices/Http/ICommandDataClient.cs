using PlatformService.Application.Commands;
using PlatformService.Application.Queries;

namespace PlatformService.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(CreatePlatformRequest plat);
    }
}
