using FYPManager.Exceptions;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace FYPManager.Boundary.Services;

/// <summary>
/// This service provides methods for password hashing, validation and generation.
/// </summary>
public static class PasswordService
{
    private const int MinLength = 8;
    private static readonly Regex UpperRegex = new(@"[A-Z]");
    private static readonly Regex LowerRegex = new(@"[a-z]");
    private static readonly Regex DigitRegex = new(@"\d");

    /// <summary>
    /// Hashes a password using the SHA256 algorithm and returns the result as a Base64 encoded string.
    /// </summary>
    /// <param name="password">The password to be hashed.</param>
    /// <returns>The hashed password as a Base64 encoded string.</returns>
    public static string HashPassword(string password = "password")
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Prompts the user to enter a new password, validates it, and returns the hashed password as a Base64 encoded string.
    /// </summary>
    /// <returns>The hashed password as a Base64 encoded string.</returns>
    /// <exception cref="AccountException">Thrown when the password fails validation.</exception>
    public static string GetNewHashedPassword()
    {
        Console.WriteLine("Password must have at least 8 characters, contain 1 uppercase letter, 1 lowercase letter and 1 digit.");
        string? password;

        do
        {
            Console.Write("Enter password: ");
            password = StringHandler.ReadString();
            try
            {
                ValidatePassword(password);
            } catch (AccountException ex)
            {
                Console.WriteLine(ex.Message);
                password = null;
            }
        } while (password == null);

        Console.Write("Enter password again: ");
        var repeatPassword = StringHandler.ReadString();

        if (!password.Equals(repeatPassword))
            throw new AccountException("Passwords do not match.");

        return HashPassword(password);
    }

    /// <summary>
    /// Validates a password to ensure it meets the minimum requirements.
    /// </summary>
    /// <param name="newPassword">The password to be validated.</param>
    /// <exception cref="AccountException">Thrown when the password fails validation.</exception>
    public static void ValidatePassword(string newPassword)
    {
        if (newPassword.Length < MinLength)
            throw new AccountException($"Password should have at least {MinLength} characters.");
        if (!UpperRegex.IsMatch(newPassword))
            throw new AccountException("Password should contain at least 1 uppercase letter.");
        if (!LowerRegex.IsMatch(newPassword))
            throw new AccountException("Password should contain at least 1 lowercase letter.");
        if (!DigitRegex.IsMatch(newPassword))
            throw new AccountException("Password should contain at least 1 digit.");
    }
}