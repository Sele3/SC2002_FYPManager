using System.Security.Cryptography;
using System.Text;

namespace FYPManager.Boundary.Services;

internal static class HashService
{
    public static string Hash(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
