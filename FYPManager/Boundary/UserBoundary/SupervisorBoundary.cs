using FYPManager.Boundary.Services;
using FYPManager.Controller.UserController;
using FYPManager.Exceptions;

namespace FYPManager.Boundary.UserBoundary;

public class SupervisorBoundary : BaseUserBoundary
{
    private readonly SupervisorController _supervisorController;

    public SupervisorBoundary(SupervisorController supervisorController)
    {
        _supervisorController = supervisorController;
    }

    private static void DisplayMenu() => Console.WriteLine(
        $"{GetWelcomeText()}" +
        $"╔═════════════════════════════════════╗\n" +
        $"║        Supervisor FYP Menu          ║\n" +
        $"╟─────────────────────────────────────╢\n" +
        $"║    PROJECTS                         ║\n" +
        $"║ 1. Create a new project             ║\n" +
        $"║ 2. Update an existing project title ║\n" +
        $"║ 3. View my submitted projects       ║\n" +
        $"║                                     ║\n" +
        $"║    REQUESTS                         ║\n" +
        $"║ 4. View my pending requests         ║\n" +
        $"║ 5. View my request history          ║\n" +
        $"║ 6. Request a student transfer       ║\n" +
        $"║                                     ║\n" +
        $"║    SETTINGS                         ║\n" +
        $"║ 7. Change password                  ║\n" +
        $"╚═════════════════════════════════════╝\n" +
        $"Please select an option:");

    public override void Run()
    {
        while (true)
        {
            try
            {
                DisplayMenu();

                var idx = NumberHandler.ReadInt(7);
                
                switch (idx)
                {
                    case 0:
                        Logout();
                        return;
                    //case 1:
                    //    CreateNewProject();
                    //    break;
                    //case 2:
                    //    UpdateExistingProjectTitle();
                    //    break;
                    //case 3:
                    //    ViewMySubmittedProjects();
                    //    break;
                    //case 4:
                    //    ViewMyPendingRequests();
                    //    break;
                    //case 5:
                    //    ViewMyRequestHistory();
                    //    break;
                    //case 6:
                    //    RequestStudentTransfer();
                    //    break;

                    case 7:
                        ChangePassword(_supervisorController);
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