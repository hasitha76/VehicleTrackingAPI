using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.Data.Entities;

namespace VTS.Data
{
    public class DBSeeder
    {
        public static void Initialize(ApplicationDBContext context, IServiceProvider services)
        {
            // Get a logger
            var logger = services.GetRequiredService<ILogger<DBSeeder>>();

            // Make sure the database is created
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                logger.LogInformation("The database was already seeded");
                return;
            }

            logger.LogInformation("Start seeding the database.");

            // seeding data
            var user = new User
            {
                Name = "Johan Silva",
                Email = "johan@gmail.com",
                CreatedOn = DateTime.UtcNow
            };
            context.Users.Add(user);

            var vehicle = new Vehicle
            {
                Make = "Nissan",
                Model = "Sunny",
                VIN = "05277804T",
                User = user
            };

            context.Vehicles.Add(vehicle);

            var device = new Device
            {
                Name = "GTR078",
                Vehicle = vehicle
            };

            context.Devices.Add(device);

            context.SaveChanges();

            logger.LogInformation("Finished seeding the database.");
        }
    }
}
