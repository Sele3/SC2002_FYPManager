using FYPManager.Entity.Projects;
using FYPManager.Entity.Requests;
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

        modelBuilder.Entity<TitleChangeRequest>()
            .HasOne(cr => cr.Project)
            .WithMany()
            .HasForeignKey(cr => cr.ProjectID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TitleChangeRequest>()
            .HasOne(cr => cr.RequestTo)
            .WithMany()
            .HasForeignKey(cr => cr.RequestToSupervisorID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TitleChangeRequest>()
            .HasOne(cr => cr.RequestBy)
            .WithMany()
            .HasForeignKey(cr => cr.RequestByStudentID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AllocateProjectRequest>()
            .HasOne(pr => pr.Project)
            .WithMany()
            .HasForeignKey(pr => pr.ProjectID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AllocateProjectRequest>()
            .HasOne(pr => pr.AllocateTo)
            .WithMany()
            .HasForeignKey(pr => pr.AllocateToStudentID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DeallocateProjectRequest>()
            .HasOne(pr => pr.DeallocateStudent)
            .WithMany()
            .HasForeignKey(pr => pr.DeallocateStudentID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TransferStudentRequest>()
            .HasOne(sr => sr.TransferFrom)
            .WithMany()
            .HasForeignKey(sr => sr.TransferFromSupervisorID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TransferStudentRequest>()
            .HasOne(sr => sr.TransferTo)
            .WithMany()
            .HasForeignKey(sr => sr.TransferToSupervisorID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<TransferStudentRequest>()
            .HasOne(sr => sr.Project)
            .WithMany()
            .HasForeignKey(sr => sr.ProjectID)
            .OnDelete(DeleteBehavior.Cascade);
    }

    // User Entities
    public DbSet<Student> Students { get; set; }
    public DbSet<Supervisor> Supervisors { get; set; }
    public DbSet<Coordinator> Coordinators { get; set; }

    // Project Entity
    public DbSet<Project> Projects { get; set; }

    // Request Entities
    public DbSet<TitleChangeRequest> TitleChangeRequests { get; set; }
    public DbSet<AllocateProjectRequest> AllocateProjectRequests { get; set; }
    public DbSet<DeallocateProjectRequest> DeallocateProjectRequests { get; set; }
    public DbSet<TransferStudentRequest> TransferStudentRequests { get; set; }
}
