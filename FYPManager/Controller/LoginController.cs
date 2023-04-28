using FYPManager.Controller.Utility;
using FYPManager.Entity;
using FYPManager.Entity.Users;
using FYPManager.Exceptions;

namespace FYPManager.Controller;

public class LoginController
{
    private readonly FYPMContext _context;
    private readonly IServiceProvider _serviceProvider;

    public LoginController(FYPMContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    public void LoginAs<T>(string userID, string password) where T : User
    {
        var user = GetUser<T>(userID, password);

        UserSession.SetCurrentUser(user);

        var userBoundary = UserBoundaryFactory.GetUserBoundary<T>(_serviceProvider);
        userBoundary.Run();
    }

    private T GetUser<T>(string userID, string password) where T : User
        => _context
            .Set<T>()
            .FirstOrDefault(u => u.UserID.Equals(userID) && u.Password.Equals(password))
            ?? throw new LoginException();
}
