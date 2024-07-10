using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

using Microsoft.Extensions.DependencyInjection;

namespace WebPushNotificationService.Extensions
{
    public static class ServiceExtension
    {

        public static void ConfigureSqlite3(this IServiceCollection services) {

            services.AddDbContext<ServiceMonitorContext>(options =>
                options.UseSqlite("Data Source=./db/servicemon.db"));
        }
    }
}
