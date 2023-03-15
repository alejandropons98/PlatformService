using PlatformService.Application.Commands;

namespace PlatformService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(PublishPlatformRequest platform);
    }
}
