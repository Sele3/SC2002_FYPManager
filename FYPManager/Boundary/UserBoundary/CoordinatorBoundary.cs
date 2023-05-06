using FYPManager.Boundary.Services.ConsoleDisplay;
using FYPManager.Boundary.Services.InputHandlers;
using FYPManager.Boundary.Services.StrategySelector.Projects;
using FYPManager.Controller.UserController;
using FYPManager.Controller.Utility;
using FYPManager.Entity.Users;
using FYPManager.Exceptions;

namespace FYPManager.Boundary.UserBoundary;

public class CoordinatorBoundary : SupervisorBoundary, IMenuDisplayable
{
    private readonly CoordinatorController _coordinatorController;

    public CoordinatorBoundary(CoordinatorController coordinatorController, SupervisorController supervisorController) : base(supervisorController)
    {
        _coordinatorController = coordinatorController;
    }

    protected override int GetNewPendingRequestCount()
        => _coordinatorController.GetNewPendingRequestCount();

    public override void Run()
    {
        while (true)
        {
            try
            {
                DisplayMenu<CoordinatorBoundary>();

                var choice = NumberHandler.ReadInt(9);
                
                switch (choice)
                {
                    case 0:
                        Logout();
                        return;

                    case 1:
                        CreateNewProject();
                        break;

                    case 2:
                        UpdateExistingProjectTitle();
                        break;

                    case 3:
                        ViewMySubmittedProjects();
                        break;

                    case 4:
                        ViewAllExistingProjects();
                        break;
                    //case 5:
                    //    ViewAllPendingRequests();
                    //    break;
                    //case 6:
                    //    ResolvePendingRequests();
                    //    break;
                    case 7:
                        ViewAllRequestHistory();
                        break;
                    //case 8:
                    //    RequestStudentTransfer();
                    //    break;

                    case 9:
                        ChangePassword(_coordinatorController);
                        break;
                }
            }
            catch (CustomException ex)
            {
                OptionalFailureMessage = ex.Message;
            }
        }
    }

    private void ViewAllRequestHistory()
    {
        var requests = _coordinatorController.GetAllRequestHistory();
        PaginatorService.Paginate(requests, 5, "Viewing All Requests");
    }

    private void ViewAllExistingProjects()
    {
        var viewProjectService = new ViewProjectService();
        viewProjectService.RunDisplayService(_coordinatorController);
    }
}
