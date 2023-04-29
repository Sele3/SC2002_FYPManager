using FYPManager.Boundary.Services;
using FYPManager.Controller;
using FYPManager.Entity.Users;
using FYPManager.Exceptions;

namespace FYPManager.Boundary;

/// <summary>
/// The LoginBoundary class provides a console-based login system that allows users to login as a student, supervisor, or coordinator.
/// </summary>
public class LoginBoundary
{
    private readonly LoginController _loginController;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginBoundary"/> class with the specified <see cref="LoginController"/>.
    /// </summary>
    /// <param name="loginController">The <see cref="LoginController"/> to be used.</param>
    public LoginBoundary(LoginController loginController)
    {
        _loginController = loginController;
    }

    /// <summary>
    /// Displays the login menu options to the console.
    /// </summary>
    private static void DisplayMenu() => Console.WriteLine(
        $"\n" +
        $"--------------------------------------------\n" +
        $"<Enter 0 to shutdown system>\n" +
        $"---------- Welcome to FYP Manager ----------\n" +
        $"Login as\n" +
        $"1. Student\n" +
        $"2. Supervisor\n" +
        $"3. FYP Coordinator\n" +
        $"Please select an option:");

    /// <summary>
    /// Displays the login menu and allows users to login as a student, supervisor, or coordinator.
    /// </summary>
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
                var hashedPassword = PasswordService.HashPassword(password);

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
            catch (CustomException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}