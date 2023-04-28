using FYPManager.Entity;
using Microsoft.EntityFrameworkCore;
namespace FYPManager.FYPManagerTests;

public class TestFYPMContext : FYPMContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "TestDatabase");
        optionsBuilder.UseLazyLoadingProxies();
    }
}
