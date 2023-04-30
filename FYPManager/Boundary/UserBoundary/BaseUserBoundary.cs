using FYPManager.Boundary.Services;
using FYPManager.Controller.UserController;
using FYPManager.Controller.Utility;

namespace FYPManager.Boundary.UserBoundary;

public abstract class BaseUserBoundary
{
    public abstract void Run();

    protected static string GetWelcomeText() =>
        $"\n" +
        $"┌─────────────────────────────────────┐\n" +
        $"│ Hello, {UserSession.GetCurrentUser().Name,-29}│\n" +
        $"│ <Enter 0 to log out>                │\n" +
        $"└─────────────────────────────────────┘\n";

    protected static void ChangePassword(BaseUserController controller)
    {
        Console.WriteLine("Changing password ...");
        var newPassword = PasswordService.GetNewHashedPassword();
        controller.ChangePassword(newPassword);
        Console.WriteLine("Password changed successfully.");
    }
    
    protected static void Logout()
    {
        UserSession.LogoutCurrentUser();
        Console.WriteLine("Logging out ...");
    }
}
