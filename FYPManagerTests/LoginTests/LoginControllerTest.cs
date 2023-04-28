using FYPManager.Boundary.Services;
using FYPManager.Boundary.UserBoundary;
using FYPManager.Controller;
using FYPManager.Entity.Users;
using FYPManager.Exceptions;
using Moq;

namespace FYPManagerTests.LoginTests;

[TestClass]
public class LoginControllerTest : BaseTest
{
    private readonly Mock<SupervisorBoundary> _mockUserBoundary = new();
    private LoginController _loginController = null!;

    [TestInitialize]
    public override void Setup()
    {
        base.Setup();

        var _mockServiceProvider = new Mock<IServiceProvider>();
        _mockServiceProvider
            .Setup(x => x.GetService(typeof(SupervisorBoundary)))
            .Returns(_mockUserBoundary.Object);

        _loginController = new LoginController(_context, _mockServiceProvider.Object);
    }

    [TestMethod]
    public void TestSuccessfulSupervisorLogin()
    {
        // Arrange
        var userID = "asfli";
        var password = HashService.Hash();

        // Act
        PerformLogin<Supervisor>(userID, password);

        // Assert
        _mockUserBoundary.Verify(b => b.Run(), Times.Once);
        Assert.AreEqual(userID, UserSession.GetCurrentUser().UserID);
    }

    [TestMethod]
    [ExpectedException(typeof(LoginException))]
    public void TestWrongUserIDSupervisorLogin()
    {
        // Arrange
        var userID = "unknown";
        var password = HashService.Hash();
        // Act
        PerformLogin<Supervisor>(userID, password);

        // Assert
        // Expects LoginException to be thrown
    }

    [TestMethod]
    [ExpectedException(typeof(LoginException))]
    public void TestWrongPasswordSupervisorLogin()
    {
        // Arrange
        var userID = "asfli";
        var password = "wrong";

        // Act
        PerformLogin<Supervisor>(userID, password);

        // Assert
        // Expects LoginException to be thrown
    }

    [TestMethod]
    [ExpectedException(typeof(LoginException))]
    public void TestCoordinatorLoginWithSupervisorCredentials()
    {
        // Arrange
        var userID = "limo";
        var password = HashService.Hash();

        // Act
        PerformLogin<Coordinator>(userID, password);

        // Assert
        // Expects LoginException to be thrown
    }

    private void PerformLogin<T>(string userID, string password) where T : User
        => _loginController.LoginAs<T>(userID, password);
}
