using FYPManager.Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPManager.Boundary.UserBoundary;

public abstract class BaseUserBoundary
{
    public abstract void Run();

    protected static User GetCurrentUser() => UserSession.GetCurrentUser();

    protected string GetWelcomeText() =>
        $"\n" +
        $"--------------------------------------\n" +
        $"Hello, {GetCurrentUser().Name}\n" +
        $"<Enter 0 to log out>\n";

    protected static void ChangePassword()
    {
        var newPassword = LoginBoundary.GetNewPassword(GetCurrentUser().Password);
        GetCurrentUser().Password = newPassword;
    }

    protected static void Logout()
    {
        UserSession.LogoutCurrentUser();
        Console.WriteLine("Logging out ...");
    }
}
