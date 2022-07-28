using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyAutoAPI1.BackgroundServices.MigrateDb;
using MyAutoAPI1.Services.Role;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyAutoAPI1.BackgroundServices
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public MyBackgroundService(IServiceProvider myScopedService)
        {
            _serviceProvider = myScopedService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using(var scope = _serviceProvider.CreateScope())
            {
                var migrationDbService = scope.ServiceProvider.GetRequiredService<MIgrationDb>();
                migrationDbService.Migration();

                var roleService = scope.ServiceProvider.GetRequiredService<IRoleServices>();
                await roleService.InitilizeAdminAndRolesAsync();
            }
        }

    }
}
