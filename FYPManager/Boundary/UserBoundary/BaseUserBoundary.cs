using FYPManager.Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPManager.Boundary.UserBoundary;

public abstract class BaseUserBoundary
{
    protected readonly User _currentUser;

    public BaseUserBoundary()
    {
        _currentUser = UserSession.GetCurrentUser();
    }

    public abstract void Run();

    protected string GetWelcomeText() =>
        $"\n" +
        $"--------------------------------------\n" +
        $"Hello, {_currentUser.Name}\n" +
        $"<Enter 0 to log out>\n";

    protected void ChangePassword()
    {
        var newPassword = LoginBoundary.GetNewPassword(_currentUser.Password);
        _currentUser.Password = newPassword;
    }

    protected static void Logout()
    {
        UserSession.LogoutCurrentUser();
        Console.WriteLine("Logging out ...");
    }
}
