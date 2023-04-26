using FYPManager.Boundary.Services;
using FYPManager.Entity.Data;
using FYPManager.Entity.Users;
using Microsoft.Extensions.Configuration;

namespace FYPManager.FYPManagerTests;

[TestClass]
public class DataInitialiserTests
{
    private IConfiguration _configuration = null!;
    private TestFYPMContext _context = null!;
    private DataInitialiser _dataInitialiser = null!;

    [TestInitialize]
    public void Setup()
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        _context = new TestFYPMContext();
        _dataInitialiser = new DataInitialiser(_configuration, _context);

        _dataInitialiser.SeedData();
    }

    [TestMethod]
    public void TestSeedStudents()
    {
        // Arrange
        var students = GetUsersFromConfiguration<Student>("SeedData:Students");

        // Act

        // Assert
        students = MapUsers(students);
        CompareUsers(students);
    }

    [TestMethod]
    public void TestSeedSupervisors()
    {
        // Arrange
        var supervisors = GetUsersFromConfiguration<Supervisor>("SeedData:Supervisors");

        // Act

        // Assert
        supervisors = MapUsers(supervisors);
        CompareUsers(supervisors);
    }

    [TestMethod]
    public void TestSeedCoordinators()
    {
        // Arrange
        var coordinators = GetUsersFromConfiguration<Coordinator>("SeedData:Coordinators");

        // Act

        // Assert
        coordinators = MapUsers(coordinators);
        CompareUsers(coordinators);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }

    private List<T> GetUsersFromConfiguration<T>(string sectionName) where T : User
    {
        var section = _configuration.GetRequiredSection(sectionName);
        var users = section.Get<List<T>>();
        return users ?? throw new AssertFailedException("Failed to get user list from configuration file.");
    }

    private static List<T> MapUsers<T>(List<T> user) where T : User
    {
        foreach (var u in user)
        {
            u.Email = u.Email.ToLower();
            u.UserID = EmailService.GetUserID(u.Email);
            u.Password = HashService.Hash();
        }
        return user;
    }

    private void CompareUsers<T>(List<T> users) where T : User
    {
        var actualUsers = _context.Set<T>();

        foreach (var user in users)
        {
            var actualUser = actualUsers.Find(user.UserID);

            Assert.IsNotNull(actualUser);
            Assert.AreEqual(user, actualUser);
        }
    }
}