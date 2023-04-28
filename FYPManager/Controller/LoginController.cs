using FYPManager.Controller.Utility;
using FYPManager.Entity;
using FYPManager.Entity.Users;
using FYPManager.Exceptions;

namespace FYPManager.Controller;

/// <summary>
/// Controller class responsible for handling the login functionality.
/// </summary>
public class LoginController
{
    private readonly FYPMContext _context;
    private readonly IServiceProvider _serviceProvider;

    public LoginController(FYPMContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Attempts to log in as a <see cref="User"/> of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="User"/> to log in.</typeparam>
    /// <param name="userID">The ID of the <see cref="User"/>.</param>
    /// <param name="password">The password of the <see cref="User"/>.</param>
    /// <exception cref="LoginException">Thrown when the provided userID and password do not match the <see cref="User"/> of the specified type.</exception>
    public void LoginAs<T>(string userID, string password) where T : User
    {
        var user = GetUser<T>(userID, password);

        UserSession.SetCurrentUser(user);

        var userBoundary = UserBoundaryFactory.GetUserBoundary<T>(_serviceProvider);
        userBoundary.Run();
    }

    /// <summary>
    /// Retrieves the <see cref="User"/> with the specified userID and password.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="User"/> to log in.</typeparam>
    /// <param name="userID">The ID of the <see cref="User"/>.</param>
    /// <param name="password">The password of the <see cref="User"/>.</param>
    /// <returns>The <see cref="User"/> with the specified userID and password.</returns>
    /// <exception cref="LoginException">Thrown when the provided userID and password do not match the <see cref="User"/> of the specified type.</exception>
    private T GetUser<T>(string userID, string password) where T : User
        => _context
            .Set<T>()
            .FirstOrDefault(u => u.UserID.Equals(userID) && u.Password.Equals(password))
            ?? throw new LoginException();
}