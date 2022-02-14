using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformModels.Data {
    public static class FeedDB {

        public static void FeedDb(this IApplicationBuilder app) {
            using var serviceScope = app.ApplicationServices.CreateScope();

            SeedData(serviceScope.ServiceProvider.GetService<PlatformDbContext>()!);

        }

        public static void SeedData(PlatformDbContext _db) {
            if (!_db.Platforms.Any()) {
                Console.WriteLine("=> Seeding Data...");

                _db.Platforms.AddRange(
                    new Models.Platform { Name="Dot Net", Publisher = "Microsoft", Cost= "Free" },
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
