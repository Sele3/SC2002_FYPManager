using FYPManager.Boundary.Services;
using FYPManager.Controller.UserController;
using FYPManager.Exceptions;

namespace FYPManager.Boundary.UserBoundary;

public class CoordinatorBoundary : SupervisorBoundary
{
    private readonly CoordinatorController _coordinatorController;

    public CoordinatorBoundary(CoordinatorController coordinatorController, SupervisorController supervisorController) : base(supervisorController)
    {
        _coordinatorController = coordinatorController;
    }

    private static void DisplayMenu() => Console.WriteLine(
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

                var idx = NumberHandler.ReadInt(8);
                
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
                    //    ViewAllExistingProjects();
                    //    break;
                    //case 5:
                    //    ViewAllPendingRequests();
                    //    break;
                    //case 6:
                    //    ViewAllRequestHistory();
                    //    break;
                    //case 7:
                    //    RequestStudentTransfer();
                    //    break;

                    case 8:
                        ChangePassword(_coordinatorController);
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
