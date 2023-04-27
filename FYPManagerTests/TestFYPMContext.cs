using FYPManager.Entity;
using Microsoft.EntityFrameworkCore;
namespace FYPManager.FYPManagerTests;

internal class TestFYPMContext : FYPMContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "TestDatabase");
    }
}
