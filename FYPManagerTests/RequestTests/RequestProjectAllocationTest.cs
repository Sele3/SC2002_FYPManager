using FYPManager.Controller.UserController;
using FYPManager.Entity.Projects;
using FYPManager.Entity.Users;
using FYPManager.Exceptions;

namespace FYPManagerTests.RequestTests;

/// <summary>
/// This class contains tests for the <see cref="StudentController.RequestProjectAllocation(string, int)"/> method.
/// </summary>
[TestClass]
public class RequestProjectAllocationTest : BaseWithSeedDataTest
{
    private StudentController _studentController = null!;
    private Student TestStudent { get; set; } = null!;
    private Project TestProject { get; set; } = null!;

    /// <summary>
    /// Sets up the test environment by initializing the <see cref="StudentController"/> 
    /// and retrieving a <see cref="Student"/> and <see cref="Project"/> from the database.
    /// </summary>
    [TestInitialize]
    public override void Setup()
    {
        base.Setup();
        TestStudent = _context.Students.First();
        TestProject = _context.Projects.First();
        _studentController = new StudentController(_context);
    }

    /// <summary>
    /// Tests if a valid request for project allocation is successful.
    /// </summary>
    [TestMethod]
    public void RequestProjectAllocation_ValidRequest_Success()
    {
        // Arrange
        var studentID = TestStudent.UserID;
        var projectID = TestProject.ProjectID;

        // Act
        _studentController.RequestProjectAllocation(studentID, projectID);

        // Assert
        var request = _context.AllocateProjectRequests
            .FirstOrDefault(
                r => r.ProjectID == projectID && 
                r.AllocateToStudentID == studentID);
        Assert.IsNotNull(request);
    }

    /// <summary>
    /// Tests if a request for project allocation throws an <see cref="AccountException"/> when the student does not exist.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(AccountException))]
    public void RequestProjectAllocation_StudentDoesNotExist_ThrowsAccountException()
    {
        // Arrange
        var studentID = "NonExistentStudent";
        var projectID = TestProject.ProjectID;

        // Act
        _studentController.RequestProjectAllocation(studentID, projectID);
    }

    /// <summary>
    /// Tests if a request for project allocation throws a <see cref="ProjectException"/> when the project does not exist.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ProjectException))]
    public void RequestProjectAllocation_ProjectDoesNotExist_ThrowsProjectException()
    {
        // Arrange
        var studentID = TestStudent.UserID;
        var projectID = -1;

        // Act
        _studentController.RequestProjectAllocation(studentID, projectID);
    }

    /// <summary>
    /// Tests if a request for project allocation throws a <see cref="ProjectException"/> when the project is not available.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ProjectException))]
    public void RequestProjectAllocation_ProjectIsNotAvailable_ThrowsProjectException()
    {
        // Arrange
        var studentID = TestStudent.UserID;
        var projectID = TestProject.ProjectID;
        TestProject.Status = ProjectStatus.Unavailable;
        _context.SaveChanges();

        // Act
        _studentController.RequestProjectAllocation(studentID, projectID);
    }
}