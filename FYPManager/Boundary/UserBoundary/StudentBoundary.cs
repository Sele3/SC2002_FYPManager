using FYPManager.Boundary.Services;

namespace FYPManager.Boundary.UserBoundary;

public class StudentBoundary : BaseUserBoundary
{
    //private readonly StudentController _studentController;
    private void DisplayMenu() => Console.WriteLine(
        $"{GetWelcomeText()}" +
        $"---------- Student FYP Menu ----------\n" +
        $"1. \n" +
        $"2. \n" +
        $"3. \n" +
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
