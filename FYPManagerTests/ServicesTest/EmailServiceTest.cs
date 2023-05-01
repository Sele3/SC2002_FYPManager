using FYPManager.Boundary.Services.UserAttribute;

namespace FYPManagerTests.ServicesTest;

/// <summary>
/// This class contains unit tests for the <see cref="EmailService"/> class.
/// </summary>
[TestClass]
public class EmailServiceTest
{
    /// <summary>
    /// Test <see cref="EmailService.GetUserID(string)"/> method with a valid email address.
    /// </summary>
    [TestMethod]
    public void TestGetUserID_ValidEmail()
    {
        // Arrange
        var email = "test123@example.com";

        // Act
        var userID = EmailService.GetUserID(email);

        // Assert
        Assert.AreEqual("test123", userID);
    }

    /// <summary>
    /// Test <see cref="EmailService.GetUserID(string)"/> method with an invalid email address.
    /// An <see cref="ArgumentException"/> is expected to be thrown.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetUserID_InvalidEmail()
    {
        // Arrange
        var email = "invalid123";

        // Act
        EmailService.GetUserID(email);

        // Assert
    }
}