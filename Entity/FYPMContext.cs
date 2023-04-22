using FYPManager.Entity.Users;
using Microsoft.EntityFrameworkCore;

namespace FYPManager.Entity;

public class FYPMContext : DbContext
{
    public FYPMContext(DbContextOptions<FYPMContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Supervisor> Supervisors { get; set; }
    public DbSet<Coordinator> Coordinators { get; set; }
}
