using System.Security.Cryptography;
using System.Text;

namespace FYPManager.Boundary.Services;

public static class HashService
{
    public static string Hash(string password = "password")
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
