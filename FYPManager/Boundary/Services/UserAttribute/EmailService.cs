using System.ComponentModel.DataAnnotations;

namespace FYPManager.Boundary.Services.UserAttribute;

/// <summary>
/// This service provides functionality to retrieve the userID associated with a given email address.
/// </summary>
public static class EmailService
{
    /// <summary>
    /// Retrieves the userID associated with a given email address.
    /// </summary>
    /// <param name="email">The email address for which to retrieve the userID.</param>
    /// <returns>The userID associated with the email address.</returns>
    /// <exception cref="ArgumentException">Thrown if the email address provided is invalid.</exception>
    public static string GetUserID(string email)
    {
        if (!IsValidEmail(email))
            throw new ArgumentException($"Invalid email address: {email}");

        return email.Split("@")[0];
    }

    /// <summary>
    /// Validates whether a given email address is valid.
    /// </summary>
    /// <param name="email">The email address to validate.</param>
    /// <returns><see langword="true"/> if the email address is valid, <see langword="false"/> otherwise.</returns>
    private static bool IsValidEmail(string email)
        => email != null && new EmailAddressAttribute().IsValid(email);
}