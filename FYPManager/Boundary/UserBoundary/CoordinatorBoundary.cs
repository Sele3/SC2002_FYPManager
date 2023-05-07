using FYPManager.Boundary.Services.ConsoleDisplay;
using FYPManager.Boundary.Services.InputHandlers;
using FYPManager.Boundary.Services.StrategySelector.Projects;
using FYPManager.Boundary.Services.StrategySelector.Requests.CoordinatorRequests;
using FYPManager.Controller.UserController;
using FYPManager.Entity.Requests;
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

                    case 5:
                        ViewAllPendingRequests();
                        break;

                    case 6:
                        ResolvePendingRequests();
                        break;

                    case 7:
                        ViewAllRequestHistory();
                        break;

                    case 8:
                        RequestStudentTransfer();
                        break;

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

    private void ViewAllExistingProjects()
    {
        var viewProjectService = new ViewProjectService();
        viewProjectService.RunDisplayService(_coordinatorController);
    }

    private void ViewAllRequestHistory()
    {
        var viewRequestService = new CoordinatorViewRequestService();
        viewRequestService.RunDisplayService(_coordinatorController);
    }

    private void ViewAllPendingRequests()
    {
        var allPendingRequests = _coordinatorController.GetAllPendingRequests();
        PaginatorService.Paginate(allPendingRequests, 3, "All Pending Requests");

        _coordinatorController.MarkRequestsAsSeen(allPendingRequests);
    }

    private void ResolvePendingRequests()
    {
        Console.WriteLine("Enter the RequestID of the request you want to resolve: ");
        var requestID = NumberHandler.ReadInt();
        var request = _coordinatorController.GetRequestByID(requestID);

        DisplayRequestInfo(request);
        Console.WriteLine("Do you want to approve this request? (Y/N)");
        var choice = StringHandler.GetYesNo();

        if (choice)
        {
            _coordinatorController.ApproveRequest(request);
            OptionalMessage = "Request Approved!";
        }
        else
        {
            CoordinatorController.RejectRequest(request);
            OptionalMessage = "Request Rejected!";
        }
    }

    private void DisplayRequestInfo(BaseRequest request)
    {

        Console.WriteLine(request);
    }
}
