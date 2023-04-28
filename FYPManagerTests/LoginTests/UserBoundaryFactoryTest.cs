using FYPManager.Boundary.UserBoundary;
using FYPManager.Controller.Utility;
using FYPManager.Entity.Users;
using Microsoft.Extensions.DependencyInjection;

namespace FYPManagerTests.LoginTests;

/// <summary>
/// This class contains unit tests for the <see cref="UserBoundaryFactory"/> class.
/// </summary>
[TestClass]
public class UserBoundaryFactoryTest
{
    private IServiceProvider _serviceProvider = null!;

    /// <summary>
    /// Set up the test environment by creating an instance of <see cref="IServiceProvider"/>.
    /// </summary>
    [TestInitialize]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddScoped<StudentBoundary>();
        services.AddScoped<SupervisorBoundary>();
        services.AddScoped<CoordinatorBoundary>();
        _serviceProvider = services.BuildServiceProvider();
    }

    /// <summary>
    /// Test that the correct user boundary class that inherits <see cref="BaseUserBoundary"/> is returned.
    /// </summary>
    [TestMethod]
    public void TestGetCorrectUserBoundary()
    {
        // Arrange
        var expectedBoundary = _serviceProvider.GetService<CoordinatorBoundary>();

        // Act
        var actualBoundary = UserBoundaryFactory.GetUserBoundary<Coordinator>(_serviceProvider);

        // Assert
        Assert.AreEqual(expectedBoundary!.GetType(), actualBoundary.GetType());
    }

    /// <summary>
    /// Test that an <see cref="ArgumentException"/> is thrown when an invalid <see cref="User"/> type is passed in.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetInvalidUserBoundary()
    {
        // Arrange

        // Act
        UserBoundaryFactory.GetUserBoundary<User>(_serviceProvider);

        // Assert
        // Expects ArgumentException to be thrown
    }
}