using AutoMapper;
using PlatformService.Application.Commands;
using PlatformService.Application.Queries;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile() {

            CreateMap<Platform, FindAllPlatformsRequest>();

            CreateMap<CreatePlatformRequest, Platform>();

            CreateMap<Platform, CreatePlatformRequest>();

        }
    }
}
