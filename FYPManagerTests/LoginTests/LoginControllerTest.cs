using FYPManager.Boundary.Services.UserAttribute;
using FYPManager.Boundary.UserBoundary;
using FYPManager.Controller;
using FYPManager.Controller.Utility;
using FYPManager.Entity.Users;
using FYPManager.Exceptions;
using Moq;

namespace FYPManagerTests.LoginTests;

/// <summary>
/// This class contains unit tests for the <see cref="LoginController"/> class.
/// </summary>
[TestClass]
public class LoginControllerTest : BaseSeedDataTest
{
    private readonly Mock<SupervisorBoundary> _mockSupervisorBoundary = new(null);
    private LoginController _loginController = null!;

    /// <summary>
    /// Initializes the test environment by setting up the mock objects and creating an instance of <see cref="LoginController"/>.
    /// </summary>
    [TestInitialize]
    public override void Setup()
    {
        base.Setup();

        var _mockServiceProvider = new Mock<IServiceProvider>();
        _mockServiceProvider
            .Setup(x => x.GetService(typeof(SupervisorBoundary)))
            .Returns(_mockSupervisorBoundary.Object);

        _loginController = new LoginController(_context, _mockServiceProvider.Object);
    }

    /// <summary>
    /// Tests a successful login attempt by a <see cref="Supervisor"/>.
    /// </summary>
    [TestMethod]
    public void TestSuccessfulSupervisorLogin()
    {
        // Arrange
        var userID = "asfli";
        var password = PasswordService.HashPassword();

        // Act
        PerformLogin<Supervisor>(userID, password);

        // Assert
        _mockSupervisorBoundary.Verify(b => b.Run(), Times.Once);
        Assert.AreEqual(userID, UserSession.GetCurrentUser().UserID);
    }

    /// <summary>
    /// Tests a login attempt with an unknown userID by a <see cref="Supervisor"/>.
    /// Expects a <see cref="LoginException"/> to be thrown.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(LoginException))]
    public void TestWrongUserIDSupervisorLogin()
    {
        // Arrange
        var userID = "unknown";
        var password = PasswordService.HashPassword();
        // Act
        PerformLogin<Supervisor>(userID, password);

        // Assert
        // Expects LoginException to be thrown
    }

    /// <summary>
    /// Tests a login attempt with an incorrect password by a <see cref="Supervisor"/>.
    /// Expects a <see cref="LoginException"/> to be thrown.
    /// </summary>
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

    /// <summary>
    /// Tests a login attempt by a <see cref="Coordinator"/> using the credentials of a <see cref="Supervisor"/> user.
    /// Expects a <see cref="LoginException"/> to be thrown.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(LoginException))]
    public void TestCoordinatorLoginWithSupervisorCredentials()
    {
        // Arrange
        var userID = "limo";
        var password = PasswordService.HashPassword();

        // Act
        PerformLogin<Coordinator>(userID, password);

        // Assert
        // Expects LoginException to be thrown
    }

    /// <summary>
    /// Helper method that performs a login attempt for the specified <see cref="User"/> using the given userID and password.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="User"/> to log in as.</typeparam>
    /// <param name="userID">The userID to use for the login attempt.</param>
    /// <param name="password">The password to use for the login attempt.</param>
    private void PerformLogin<T>(string userID, string password) where T : User
        => _loginController.LoginAs<T>(userID, password);
}