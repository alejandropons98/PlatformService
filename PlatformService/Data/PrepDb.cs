﻿using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using( var serviceScope = app.ApplicationServices.CreateScope() )
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData( AppDbContext context, bool isProd ) {

            if (isProd)
            {
                Console.WriteLine("MIGRATING");
                try
                {
					context.Database.Migrate();
				}
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }

            if (!context.Platforms.Any())
            {
                Console.WriteLine("Seeding");

                context.Platforms.AddRange(
                    new Platform() { Name = "Dotnet", Publisher = "Microsoft", Cost = "Free"},
                    new Platform() { Name = "SQL", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Microsoft", Cost = "Free" }
                    );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("No");
            }
        
        }
    }
}
