using FYPManager.Boundary.Services.UserAttribute;
using FYPManager.Exceptions;

namespace FYPManagerTests.ServicesTest;

/// <summary>
/// This class contains unit tests for the <see cref="PasswordService"/> class.
/// </summary>
[TestClass]
public class PasswordServiceTest
{
    /// <summary>
    /// Tests the HashPassword method of the <see cref="PasswordService"/> class.
    /// </summary>
    [TestMethod]
    public void TestHashPassword()
    {
        // Arrange
        var password = "password";

        // Act
        var hashedPassword = PasswordService.HashPassword(password);

        // Assert
        Assert.IsNotNull(hashedPassword);
        Assert.AreNotEqual(password, hashedPassword);
    }

    /// <summary>
    /// Tests the ValidatePassword method of the <see cref="PasswordService"/> class with a valid password.
    /// </summary>
    [TestMethod]
    public void TestValidatePassword_ValidPassword()
    {
        // Arrange
        var password = "Passw0rd";

        // Act
        PasswordService.ValidatePassword(password);

        // Assert
    }

    /// <summary>
    /// Tests the ValidatePassword method of the <see cref="PasswordService"/> class with a password that is too short.
    /// </summary>
    [TestMethod]
    public void TestValidatePassword_TooShort()
    {
        // Arrange
        var password = "Passw0r";

        // Act
        var exception = Assert.ThrowsException<AccountException>(()
            => PasswordService.ValidatePassword(password));

        // Assert
        Assert.AreEqual("Password should have at least 8 characters.", exception.Message);
    }

    /// <summary>
    /// Tests the ValidatePassword method of the <see cref="PasswordService"/> class with a password that contains no uppercase letters.
    /// </summary>
    [TestMethod]
    public void TestValidatePassword_NoUppercase()
    {
        // Arrange
        var password = "passw0rd";

        // Act
        var exception = Assert.ThrowsException<AccountException>(()
            => PasswordService.ValidatePassword(password));

        // Assert
        Assert.AreEqual("Password should contain at least 1 uppercase letter.", exception.Message);
    }

    /// <summary>
    /// Tests the ValidatePassword method of the <see cref="PasswordService"/> class with a password that contains no lowercase letters.
    /// </summary>
    [TestMethod]
    public void TestValidatePassword_NoLowercase()
    {
        // Arrange
        var password = "PASSW0RD";

        // Act
        var exception = Assert.ThrowsException<AccountException>(()
            => PasswordService.ValidatePassword(password));

        // Assert
        Assert.AreEqual("Password should contain at least 1 lowercase letter.", exception.Message);
    }

    /// <summary>
    /// Tests the ValidatePassword method of the <see cref="PasswordService"/> class with a password that contains no digits.
    /// </summary>
    [TestMethod]
    public void TestValidatePassword_NoDigit()
    {
        // Arrange
        var password = "Password";

        // Act
        var exception = Assert.ThrowsException<AccountException>(()
            => PasswordService.ValidatePassword(password));

        // Assert
        Assert.AreEqual("Password should contain at least 1 digit.", exception.Message);
    }

    /// <summary>
    /// Tests the GetNewHashedPassword method of the <see cref="PasswordService"/> class with passwords that do not match.
    /// </summary>
    [TestMethod]
    public void TestGetNewHashedPassword_NoMatch()
    {
        // Arrange
        var password = "Passw0rd";
        var repeatPassword = "Password";
        Console.SetIn(new StringReader($"{password}\n{repeatPassword}\n"));

        // Act
        var exception = Assert.ThrowsException<AccountException>(()
                       => PasswordService.GetNewHashedPassword());

        // Assert
        Assert.AreEqual("Passwords do not match.", exception.Message);
    }
}