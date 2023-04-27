using FYPManager.Boundary.Services;
using FYPManager.Controller;
using FYPManager.Entity.Users;
using FYPManager.Exceptions;

namespace FYPManager.Boundary;

public class LoginBoundary
{
    private readonly LoginController _loginController;

    public LoginBoundary(LoginController loginController)
    {
        _loginController = loginController;
    }

    private static void DisplayMenu() => Console.WriteLine(
        $"\n" +
        $"-------------------------------------------\n" +
        $"<Enter 0 to shutdown system>\n" +
        $"---------- Welcome to FYP Manager ----------\n" +
        $"Login as\n" +
        $"1. Student\n" +
        $"2. Supervisor\n" +
        $"3. FYP Coordinator\n" +
        $"Please select an option:");

    public void Run()
    {
        while (true)
        {
            try
            {
                DisplayMenu();

                var idx = NumberHandler.ReadInt(3);

                if (idx == 0)
                {
                    Console.WriteLine("Shutting down system ...");
                    return;
                }

                Console.WriteLine("Enter your userID:");
                var userID = StringHandler.ReadString().ToLower();

                Console.WriteLine("Enter your password:");
                var password = StringHandler.ReadString();
                var hashedPassword = HashService.Hash(password);

                switch (idx)
                {
                    case 1:
                        _loginController.LoginAs<Student>(userID, hashedPassword);
                        break;
                    case 2:
                        _loginController.LoginAs<Supervisor>(userID, hashedPassword);
                        break;
                    case 3:
                        _loginController.LoginAs<Coordinator>(userID, hashedPassword);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public static string GetNewPassword(string currentPassword)
    {
        return "";
    }
}