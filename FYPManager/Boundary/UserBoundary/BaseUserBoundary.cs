using FYPManager.Boundary.Services.ConsoleDisplay;
using FYPManager.Boundary.Services.UserAttribute;
using FYPManager.Controller.UserController;
using FYPManager.Controller.Utility;

namespace FYPManager.Boundary.UserBoundary;

public abstract class BaseUserBoundary : BaseBoundary
{
    public abstract void Run();

    protected void DisplayMenu<T>() where T : IMenuDisplayable
    {
        DisplayOptionalHeaderMessage();
        DisplayWelcomeText();
        MenuDisplayService<T>.DisplayMenuBody();
    }

    private static void DisplayWelcomeText() => Console.WriteLine(
        $"┌─────────────────────────────────────┐\n" +
        $"│ Hello, {UserSession.GetCurrentUser().Name,-29}│\n" +
        $"│ <Enter 0 to log out>                │\n" +
        $"└─────────────────────────────────────┘");

    protected static void ChangePassword(BaseUserController controller)
    {
        Console.WriteLine("Changing password ...");
        var newPassword = PasswordService.GetNewHashedPassword();
        controller.ChangePassword(newPassword);
        Console.WriteLine("Password changed successfully.");
    }
    
    protected static void Logout() => UserSession.LogoutCurrentUser();
}
