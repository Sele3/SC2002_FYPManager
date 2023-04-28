using FYPManager.Boundary.Services;

namespace FYPManager.Boundary.UserBoundary;

public class StudentBoundary : BaseUserBoundary
{
    //private readonly StudentController _studentController;
    private void DisplayMenu() => Console.WriteLine(
        $"{GetWelcomeText()}" +
        $"---------- Student FYP Menu ----------\n" +
        $"-- PROJECTS\n" +
        $"1. View all available projects\n" +
        $"2. View my allocated project\n" +
        $"-- REQUESTS\n" +
        $"3. Request a project allocation\n" +
        $"4. Request a project title change\n" +
        $"5. Request a project deregistration\n" +
        $"6. View my request history\n" +
        $"-- SETTINGS\n" +
        $"7. Change password\n" +
        $"Please select an option:");

    public override void Run()
    {
        while (true)
        {
            try
            {
                DisplayMenu();

                var idx = NumberHandler.ReadInt(3);
                if (idx == 0)
                {
                    Logout();
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
