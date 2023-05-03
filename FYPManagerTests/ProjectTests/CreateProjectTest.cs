using FYPManager.Controller.UserController;
using FYPManager.Exceptions;

namespace FYPManagerTests.ProjectTests;

[TestClass]
public class CreateProjectTest : BaseWithSeedDataTest
{
    private SupervisorController SupervisorController { get; set; } = null!;

    [TestInitialize]
    public override void Setup()
    {
        base.Setup();
        SupervisorController = new SupervisorController(_context);
    }

    [TestMethod]
    public void TestAddProject_Success()
    {
        // Arrange
        var projectTitle = "Test Project";
        var supervisor = _context.Supervisors.First();

        // Act
        SupervisorController.CreateProject(projectTitle, supervisor);

        // Assert
        var project = _context.Projects.First(p => p.Title.Equals(projectTitle));
        Assert.AreEqual(projectTitle, project.Title);
        Assert.AreEqual(supervisor, project.Supervisor);
        Assert.AreEqual(supervisor.UserID, project.SupervisorID);
    }

    [TestMethod]
    [ExpectedException(typeof(ProjectException))]
    public void TestAddProject_Fail_ProjectAlreadyExists()
    {
        // Arrange
        var projectTitle = "Test Project";
        var supervisor = _context.Supervisors.First();

        // Act
        SupervisorController.CreateProject(projectTitle, supervisor);
        SupervisorController.CreateProject(projectTitle, supervisor);
    }
}
