using FYPManager.Exceptions;

namespace FYPManager.Entity.Users;

public static class UserSession
{
    private static User? _currentUser;

    public static User GetCurrentUser()
        => _currentUser ?? throw new AccountException("Error. There is no active user currently.");

    public static void SetCurrentUser(User user)
    {
        if (_currentUser != null)
            throw new AccountException("Error. There is already an active user.");
        _currentUser = user;
    }

    public static void LogoutCurrentUser()
        => _currentUser = null;
}