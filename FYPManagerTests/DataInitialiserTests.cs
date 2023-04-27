using FYPManager.Boundary.Services;
using FYPManager.Entity.Data;
using FYPManager.Entity.Projects;
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
        var dataList = GetDataListFromConfiguration<ExcelUserData>("SeedData:Students");
        var students = MapUsers<Student>(dataList);

        // Act

        // Assert
        CompareUsers(students);
    }

    [TestMethod]
    public void TestSeedSupervisors()
    {
        // Arrange
        var dataList = GetDataListFromConfiguration<ExcelUserData>("SeedData:Supervisors");
        var supervisors = MapUsers<Supervisor>(dataList);

        // Act

        // Assert
        CompareUsers(supervisors);
    }

    [TestMethod]
    public void TestSeedCoordinators()
    {
        // Arrange
        var dataList = GetDataListFromConfiguration<ExcelUserData>("SeedData:Coordinators");
        var coordinators = MapUsers<Coordinator>(dataList);

        // Act

        // Assert
        CompareUsers(coordinators);
    }

    [TestMethod]
    public void TestSeedProjects()
    {
        // Arrange
        var dataList = GetDataListFromConfiguration<ExcelProjectData>("SeedData:Projects");
        var projects = MapProjects(dataList);

        // Act

        // Assert
        CompareProjects(projects);
    }

    [TestCleanup]
    public void Cleanup() 
        => _context.Database.EnsureDeleted();

    private List<T> GetDataListFromConfiguration<T>(string sectionName) where T : ExcelData
    {
        var section = _configuration.GetRequiredSection(sectionName);
        var dataList = section.Get<List<T>>();
        return dataList ?? throw new AssertFailedException("Failed to get data list from configuration file.");
    }

    private static List<T> MapUsers<T>(List<ExcelUserData> excelUserList) where T : User, new() 
        => excelUserList.Select(excelUser => new T()
    {
        Name = excelUser.Name.Trim(),
        Email = excelUser.Email.ToLower(),
        UserID = EmailService.GetUserID(excelUser.Email.ToLower()),
        Password = HashService.Hash()
    }).ToList();

    private void CompareUsers<T>(List<T> users) where T : User
    {
        var databaseUsers = _context.Set<T>();

        foreach (var user in users)
        {
            var databaseUser = databaseUsers.Find(user.UserID);

            Assert.IsNotNull(databaseUser);
            Assert.AreEqual(user, databaseUser);
        }
    }

    private List<Project> MapProjects(List<ExcelProjectData> projectDataList)
    {
        var projects = new List<Project>();
        foreach (var project in projectDataList)
        {
            var supervisor = _context.Supervisors.FirstOrDefault(s => s.Name.Equals(project.Supervisor))
                ?? throw new AssertFailedException($"Failed to find supervisor {project.Supervisor}.");

            projects.Add(new()
            {
                SupervisorID = supervisor.UserID,
                Supervisor = supervisor,
                Title = project.Title,
            });
        }
        return projects;
    }

    private void CompareProjects(List<Project> projects)
    {
        var actualProjects = _context.Projects;
        foreach (var project in projects)
        {
            var actualProject = actualProjects.FirstOrDefault(p => p.Title.Equals(project.Title));

            Assert.IsNotNull(actualProject);
            Assert.AreEqual(project, actualProject);
        }
    }
}