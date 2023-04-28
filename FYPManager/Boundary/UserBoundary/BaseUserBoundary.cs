using FYPManager.Boundary.Services;
using FYPManager.Controller.Utility;
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

    protected static string GetWelcomeText() =>
        $"\n" +
        $"--------------------------------------\n" +
        $"Hello, {GetCurrentUser().Name}\n" +
        $"<Enter 0 to log out>\n";

    protected static string GetNewPassword()
    {
        Console.WriteLine("Please enter your new password:");
        var newPassword = StringHandler.ReadString();
        return newPassword;
    }
    
    protected static void Logout()
    {
        UserSession.LogoutCurrentUser();
        Console.WriteLine("Logging out ...");
    }
}
