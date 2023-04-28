using System.Security.Cryptography;
using System.Text;

namespace FYPManager.Boundary.Services;

/// <summary>
/// This service provides a method to hash passwords.
/// </summary>
public static class HashService
{
    /// <summary>
    /// This method takes in a password string and returns its hash as a Base64 encoded string.
    /// Uses the SHA256 algorithm to hash the password.
    /// </summary>
    /// <param name="password">The password string to be hashed.</param>
    /// <returns>The hashed password as a Base64 encoded string.</returns>
    public static string Hash(string password = "password")
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}