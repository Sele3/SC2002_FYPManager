using FYPManager.Boundary.Services;
using FYPManager.Entity.Data;
using FYPManager.Entity.Projects;
using FYPManager.Entity.Users;
using Microsoft.Extensions.Configuration;

namespace FYPManager.FYPManagerTests;

/// <summary>
/// This class contains tests for the data initialiser. 
/// It tests that the data initialiser can seed the database with students, supervisors, coordinators and projects. 
/// It also tests that the data initialiser can map the data from the excel files to the correct user types.
/// </summary>
[TestClass]
public class DataInitialiserTests
{
    private IConfiguration _configuration = null!;
    private TestFYPMContext _context = null!;
    private DataInitialiser _dataInitialiser = null!;

    /// <summary>
    /// Set up the configuration and context before each test.
    /// </summary>
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

    /// <summary>
    /// Test that the data initialiser can seed the database with students.
    /// </summary>
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

    /// <summary>
    /// Test that the data initialiser can seed the database with supervisors.
    /// </summary>
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

    /// <summary>
    /// Test that the data initialiser can seed the database with coordinators.
    /// </summary>
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

    /// <summary>
    /// Test that the data initialiser can seed the database with projects.
    /// </summary>
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

    /// <summary>
    /// Clean up the context after each test.
    /// </summary>
    [TestCleanup]
    public void Cleanup() 
        => _context.Database.EnsureDeleted();

    /// <summary>
    /// Get a list of data from the configuration file.
    /// </summary>
    /// <typeparam name="T">The type of data to retrive. It must inherit from the <see cref="ExcelData"/> class.</typeparam>
    /// <param name="sectionName">The name of the configuration section containing the data.</param>
    /// <returns>A <see cref="List{T}"/> object containing the retrieved data.</returns>
    /// <exception cref="AssertFailedException">Thrown if the data list could not be retrieved from the configuration file.</exception>
    private List<T> GetDataListFromConfiguration<T>(string sectionName) where T : ExcelData
    {
        var section = _configuration.GetRequiredSection(sectionName);
        var dataList = section.Get<List<T>>();
        return dataList ?? throw new AssertFailedException("Failed to get data list from configuration file.");
    }

    /// <summary>
    /// Maps a list of <see cref="ExcelUserData"/> objects to a list of <see cref="User"/> objects.
    /// </summary>
    /// <typeparam name="T">A type of User to map the ExcelUserData to.</typeparam>
    /// <param name="excelUserList">A list of <see cref="ExcelUserData"/> to be mapped to <see cref="User"/> objects of type T.</param>
    /// <returns>A list of <see cref="User"/> objects of type T that have been mapped from the given <see cref="ExcelUserData"/>.</returns>
    private static List<T> MapUsers<T>(List<ExcelUserData> excelUserList) where T : User, new() 
        => excelUserList.Select(excelUser => new T()
    {
        Name = excelUser.Name.Trim(),
        Email = excelUser.Email.ToLower(),
        UserID = EmailService.GetUserID(excelUser.Email.ToLower()),
        Password = HashService.Hash()
    }).ToList();

    /// <summary>
    /// Compares a list of <see cref="User"/> objects with those in the database.
    /// </summary>
    /// <typeparam name="T">A type of <see cref="User"/> to compare with the database.</typeparam>
    /// <param name="users">A list of <see cref="User"/> objects of type T to compare with those in the database.</param>
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

    /// <summary>
    /// Maps a list of <see cref="ExcelProjectData"/> objects to a list of <see cref="Project"/> objects.
    /// </summary>
    /// <param name="projectDataList">The list of <see cref="ExcelProjectData"/> objects to be mapped to <see cref="Project"/> objects.</param>
    /// <returns>A list of <see cref="Project"/> objects.</returns>
    /// <exception cref="AssertFailedException">Thrown when a supervisor associated with a project cannot be found in the database.</exception>
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

    /// <summary>
    /// Compares a list of <see cref="Project"/> objects with the actual <see cref="Project"/> objects in the database.
    /// </summary>
    /// <param name="projects">The list of <see cref="Project"/> objects to be compared with the actual <see cref="Project"/> objects in the database.</param>
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