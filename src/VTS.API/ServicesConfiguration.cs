using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTS.Business.Services;
using VTS.Data.Repositories;

namespace VTS.API
{
    public static class ServicesConfiguration
    {
        public static void ConfigureIOC(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<ITrackingService, TrackingService>();

        }
    }
}
