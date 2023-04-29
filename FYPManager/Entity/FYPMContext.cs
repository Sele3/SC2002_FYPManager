using FYPManager.Entity.Projects;
using FYPManager.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FYPManager.Entity;

public class FYPMContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasOne(s => s.Project)
            .WithOne(p => p.Student)
            .HasForeignKey<Project>(p => p.StudentID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Supervisor>()
            .HasMany(s => s.Projects)
            .WithOne(p => p.Supervisor)
            .HasForeignKey(p => p.SupervisorID)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Supervisor> Supervisors { get; set; }
    public DbSet<Coordinator> Coordinators { get; set; }
    public DbSet<Project> Projects { get; set; }
}
