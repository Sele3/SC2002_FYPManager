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
        "<Enter 0 to shutdown system>\n" +
        "Login as\n" +
        "1. Student\n" +
        "2. Supervisor\n" +
        "3. FYP Coordinator\n" +
        "Please select an option:");

    public void Run()
    {
        try
        {
            while (true)
            {
                DisplayMenu();

                var idx = NumberHandler.ReadInt(3);

                if (idx == 0)
                {
                    Console.WriteLine("Shutting down system ...");
                    return;
                }

                Console.WriteLine("Enter your username:");
                var userID = StringHandler.ReadString();

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
        } catch (LoginException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}