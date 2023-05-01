using FYPManager.Boundary.Services.ConsoleDisplay;
using FYPManager.Boundary.Services.InputHandlers;
using FYPManager.Controller.UserController;
using FYPManager.Exceptions;

namespace FYPManager.Boundary.UserBoundary;

public class SupervisorBoundary : BaseUserBoundary, IMenuDisplayable
{
    private readonly SupervisorController _supervisorController;

    public SupervisorBoundary(SupervisorController supervisorController)
    {
        _supervisorController = supervisorController;
    }

    public override void Run()
    {
        while (true)
        {
            try
            {
                DisplayMenu<SupervisorBoundary>();

                var choice = NumberHandler.ReadInt(7);
                
                switch (choice)
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