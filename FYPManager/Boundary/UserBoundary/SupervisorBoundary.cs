using FYPManager.Boundary.Services;

namespace FYPManager.Boundary.UserBoundary;

public class SupervisorBoundary : BaseUserBoundary
{
    private void DisplayMenu() => Console.WriteLine(
        $"{GetWelcomeText()}" +
        $"-------- Supervisor FYP Menu ---------\n" +
        $"-- PROJECTS\n" +
        $"1. Create a new project\n" +
        $"2. Update an existing project title\n" +
        $"3. View my submitted projects\n" +
        $"-- REQUESTS\n" +
        $"4. View my pending requests\n" +
        $"5. View my request history\n" +
        $"6. Request a student transfer\n" +
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
