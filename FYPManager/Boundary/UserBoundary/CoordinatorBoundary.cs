using FYPManager.Boundary.Services;

namespace FYPManager.Boundary.UserBoundary;

public class CoordinatorBoundary : SupervisorBoundary
{
    private void DisplayMenu() => Console.WriteLine(
        $"{GetWelcomeText()}" +
        $"-------- Coordinator FYP Menu --------\n" +
        $"-- PROJECTS\n" +
        $"1. Create a new project\n" +
        $"2. Update an existing project title\n" +
        $"3. View my submitted projects\n" +
        $"4. View all existing projects\n" +
        $"-- REQUESTS\n" +
        $"5. View all pending requests\n" +
        $"6. View all request history\n" +
        $"7. Request a student transfer\n" +
        $"-- SETTINGS\n" +
        $"8. Change password\n" +
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
