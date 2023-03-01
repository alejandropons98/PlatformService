using PlatformService.Data.Interfaces;
using PlatformService.Models;

namespace PlatformService.Data.Repositories
{
    public class PlatformRepo : GenericRepo<Platform>, IPlatformRepo
    {
        public PlatformRepo(AppDbContext context) : base(context) { }

    }
}
