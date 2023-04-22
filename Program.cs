using FYPManager.Boundary;
using FYPManager.Controller;
using FYPManager.Entity;
using FYPManager.Entity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

// Read ConnectionString from appsettings.json
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();
var connectionString = configuration.GetConnectionString("DefaultConnection");

// Setup Dependency Injection
var services = new ServiceCollection();

// Add DBContext
services.AddDbContext<FYPMContext>(
    options => options.UseSqlServer(connectionString));

// Add Boundary
services.AddTransient<LoginBoundary>();

// Add Controller
services.AddTransient<LoginController>();

var serviceProvider = services.BuildServiceProvider();

try
{
    // Create DB if not exists and seed data
    using (var context = serviceProvider.GetRequiredService<FYPMContext>())
    {
        if (!context.Database.EnsureCreated())
            context.Database.Migrate();

        DataInitialiser.SeedData(context);
    }

    // Run Application
    var loginBoundary = serviceProvider.GetRequiredService<LoginBoundary>();
    loginBoundary.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
finally
{
    serviceProvider.Dispose();
}
