using FYPManager.Boundary;
using FYPManager.Boundary.UserBoundary;
using FYPManager.Controller;
using FYPManager.Controller.UserController;
using FYPManager.Entity;
using FYPManager.Entity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Read Configuration
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();
var connectionString = configuration.GetConnectionString("DefaultConnection");

// Setup Dependency Injection
var services = new ServiceCollection();

// Add Configuration
services.AddSingleton<IConfiguration>(configuration);
services.AddTransient<DataInitialiser>();

// Add DBContext
services.AddDbContext<FYPMContext>();

// Add Boundary
services.AddTransient<LoginBoundary>();
services.AddTransient<StudentBoundary>();
services.AddTransient<SupervisorBoundary>();
services.AddTransient<CoordinatorBoundary>();

// Add Controller
services.AddTransient<LoginController>();
services.AddTransient<StudentController>();
services.AddTransient<SupervisorController>();
services.AddTransient<CoordinatorController>();

var serviceProvider = services.BuildServiceProvider();

try
{
    // Seed Data
    using (var scope = serviceProvider.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<FYPMContext>();
        context.Database.EnsureCreated();

        var dataInitializer = scope.ServiceProvider.GetRequiredService<DataInitialiser>();
        dataInitializer.SeedData();
    }

    // Run Application
    var loginBoundary = serviceProvider.GetRequiredService<LoginBoundary>();
    loginBoundary.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
}
finally
{
    serviceProvider.Dispose();
}
