using FYPManager.Boundary.Services.ConsoleDisplay;
using FYPManager.Boundary.Services.InputHandlers;
using FYPManager.Controller.UserController;
using FYPManager.Controller.Utility;
using FYPManager.Entity.Users;
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

                    case 1:
                        CreateNewProject();
                        break;
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
                OptionalFailureMessage = ex.Message;
            }
        }
    }

    private void CreateNewProject()
    {
        Console.WriteLine("Enter the title of the project: ");
        var projectTitle = StringHandler.ReadString();
        var supervisor = (Supervisor)UserSession.GetCurrentUser();
        _supervisorController.CreateProject(projectTitle, supervisor);

        OptionalSuccessMessage = $"Project '{projectTitle}' created successfully.";
    }
}