using FYPManager.Boundary.UserBoundary;
using FYPManager.Controller.Utility;
using FYPManager.Entity.Users;
using Microsoft.Extensions.DependencyInjection;

namespace FYPManagerTests.LoginTests;

[TestClass]
public class UserBoundaryFactoryTest
{
    private IServiceProvider _serviceProvider = null!;

    [TestInitialize]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddScoped<StudentBoundary>();
        services.AddScoped<SupervisorBoundary>();
        services.AddScoped<CoordinatorBoundary>();
        _serviceProvider = services.BuildServiceProvider();
    }

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