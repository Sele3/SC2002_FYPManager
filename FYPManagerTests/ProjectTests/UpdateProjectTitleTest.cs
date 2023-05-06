using FYPManager.Entity.Projects;
using FYPManager.Exceptions;

namespace FYPManagerTests.ProjectTests;

/// <summary>
/// This class contains unit tests for the <see cref="SupervisorController.UpdateProjectTitle"/> method.
/// </summary>
[TestClass]
public class UpdateProjectTitleTest : BaseProjectTest
{
    private Project TestProject1 { get; set; } = null!;
    private Project TestProject2 { get; set; } = null!;

    /// <summary>
    /// Sets up the test case by initializing two test projects.
    /// </summary>
    [TestInitialize]
    public override void Setup()
    {
        base.Setup();
        var supervisor = _context.Supervisors.First();

        TestProject1 = new()
        {
            Title = "Test Project 1",
            SupervisorID = supervisor.UserID,
        };

        TestProject2 = new()
        {
            Title = "Test Project 2",
            SupervisorID = supervisor.UserID,
        };

        _context.Projects.Add(TestProject1);
        _context.Projects.Add(TestProject2);
        _context.SaveChanges();
    }

    /// <summary>
    /// Tests that the method successfully updates the title of an existing project.
    /// </summary>
    [TestMethod]
    public void TestUpdateProjectTitle_Success()
    {
        // Arrange
        var projectID = _context
            .Projects
            .First(p => p.Title.Equals(TestProject1.Title))
            .ProjectID;

        // Act
        SupervisorController.UpdateProjectTitle(projectID, "New Project Title");
        
        // Assert
        var project = _context
            .Projects
            .First(p => p.ProjectID == projectID);

        Assert.AreEqual("New Project Title", project.Title);
        Assert.AreEqual(project, TestProject1);
    }

    /// <summary>
    /// Tests that the method throws a <see cref="ProjectException"/> when the specified project does not exist.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ProjectException))]
    public void TestUpdateProjectTitle_Fail_ProjectDoesNotExist()
    {
        // Arrange
        
        // Act
        SupervisorController.UpdateProjectTitle(9999, "New Project Title");

        // Assert
    }

    /// <summary>
    /// Tests that the method throws a <see cref="ProjectException"/> when the specified project title already exists.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ProjectException))]
    public void TestUpdateProjectTitle_Fail_ProjectTitleAlreadyExists()
    {
        // Arrange
        var projectID = _context.Projects
            .First(p => p.Title.Equals(TestProject1.Title))
            .ProjectID;
        var repeatTitle = TestProject2.Title;

        // Act
        SupervisorController.UpdateProjectTitle(projectID, repeatTitle);

        // Assert
    }
}