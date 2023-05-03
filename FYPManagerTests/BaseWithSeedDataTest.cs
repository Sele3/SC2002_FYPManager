using FYPManager.Entity.Data;
using FYPManager.FYPManagerTests;
using Microsoft.Extensions.Configuration;

namespace FYPManagerTests;

/// <summary>
/// This abtract class provides the base setup which seeds the test database with data.
/// </summary>
public abstract class BaseWithSeedDataTest
{
    protected IConfiguration _configuration = null!;
    protected TestFYPMContext _context = null!;
    protected DataInitialiser _dataInitialiser = null!;

    /// <summary>
    /// Set up the configuration and context before each test.
    /// </summary>
    [TestInitialize]
    public virtual void Setup()
    {
        _configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

        _context = new TestFYPMContext();
        _dataInitialiser = new DataInitialiser(_configuration, _context);

        _dataInitialiser.SeedData();
    }

    /// <summary>
    /// Clean up the context after each test.
    /// </summary>
    [TestCleanup]
    public void Cleanup()
        => _context.Database.EnsureDeleted();
}
