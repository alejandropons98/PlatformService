using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Data.Interfaces;
using PlatformService.Data.Repositories;
using System.Reflection;
using MediatR;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Data
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env) {
            if (env.IsDevelopment())
            {
				services.AddDbContext<AppDbContext>(options =>
options.UseInMemoryDatabase("InMem"));
			}
            else
            {
				services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("PlatformsConn")));
			}

            services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped<IPlatformRepo, PlatformRepo>();

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            return services;
        
        }
    }
}
