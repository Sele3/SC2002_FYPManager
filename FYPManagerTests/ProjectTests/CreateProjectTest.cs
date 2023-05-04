using FYPManager.Controller.UserController;
using FYPManager.Exceptions;

namespace FYPManagerTests.ProjectTests;

/// <summary>
/// This class contains unit tests for the <see cref="SupervisorController.CreateProject"/> method.
/// </summary>
[TestClass]
public class CreateProjectTest : BaseProjectTest
{
    /// <summary>
    /// Tests that a new project can be added successfully.
    /// </summary>
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

    /// <summary>
    /// Tests that attempting to add a project with a title that already exists should fail and throw a <see cref="ProjectException"/>.
    /// </summary>
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