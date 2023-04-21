using FYPManager.Boundary;
using FYPManager.Controller;
using FYPManager.Entity;
using FYPManager.Entity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddDbContext<FYPMContext>();

var serviceProvider = services.BuildServiceProvider();

using (var context = serviceProvider.GetRequiredService<FYPMContext>())
{
    if (!context.Database.EnsureCreated())
    {
        context.Database.Migrate();
    }
    DataInitialiser.SeedData(context);
}


serviceProvider.Dispose();
