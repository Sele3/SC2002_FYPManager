using FYPManager.Entity.Users;
using FYPManager.Exceptions;

namespace FYPManager.Controller.Utility;

/// <summary>
/// Utility class to manage the current user session.
/// </summary>
public static class UserSession
{
    private static User? _currentUser;

    /// <summary>
    /// Gets the current logged in <see cref="User"/>.
    /// </summary>
    /// <returns>The current logged in <see cref="User"/>.</returns>
    /// <exception cref="AccountException">Thrown when there is no active <see cref="User"/> currently.</exception>
    public static User GetCurrentUser()
        => _currentUser ?? throw new AccountException("Error. There is no active user currently.");

    /// <summary>
    /// Sets the current logged in <see cref="User"/>.
    /// </summary>
    /// <param name="user">The <see cref="User"/> to set as the current logged in <see cref="User"/>.</param>
    /// <exception cref="AccountException">Thrown when there is already an active <see cref="User"/>.</exception>
    public static void SetCurrentUser(User user)
    {
        if (_currentUser != null)
            throw new AccountException("Error. There is already an active user.");
        _currentUser = user;
    }

    /// <summary>
    /// Logs out the current <see cref="User"/>.
    /// </summary>
    public static void LogoutCurrentUser()
        => _currentUser = null;
}