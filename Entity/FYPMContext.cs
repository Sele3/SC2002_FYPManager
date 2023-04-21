using FYPManager.Entity.Users;
using Microsoft.EntityFrameworkCore;

namespace FYPManager.Entity;

public class FYPMContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FYPManager;Trusted_Connection=True;");
    }


    public DbSet<Student> Students { get; set; }
    public DbSet<Supervisor> Supervisors { get; set; }
    public DbSet<Coordinator> Coordinators { get; set; }
}
