using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PlatformModels.Data {
    public static class FeedDB {




        public static void FeedDb(this IApplicationBuilder app, bool isProduction) {
            using var serviceScope = app.ApplicationServices.CreateScope();

            SeedData(serviceScope.ServiceProvider.GetRequiredService<PlatformDbContext>(), isProduction);

        }

        public static void SeedData(PlatformDbContext _db, bool isProduction) {
            if (isProduction) {
                Console.WriteLine("--> Attempting to apply migrations...");
                try {
                    _db.Database.Migrate();
                }
                catch (Exception ex) {
                    Console.WriteLine($"--> Failed to apply migrations...{ex.Message}");
                }
            }

            if (!_db.Platforms.Any()) {
                Console.WriteLine("=> Seeding Data...");

                _db.Platforms.AddRange(
                    new Models.Platform { Name = "Dot Net", Publisher = "Microsoft", Cost= "Free" },
                    new Models.Platform { Name = "SQL", Publisher = "Microsoft", Cost = "Free" },
                    new Models.Platform { Name = "Oracle", Publisher = "Oracle", Cost = "Free" }
                );

                _db.SaveChanges();


            }
            else {
                Console.WriteLine("=> Db alreaddy Feedead");
            }
        }

    
    }
}
