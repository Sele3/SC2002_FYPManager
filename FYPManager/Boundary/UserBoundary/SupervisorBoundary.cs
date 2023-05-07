using FYPManager.Boundary.Services.ConsoleDisplay;
using FYPManager.Boundary.Services.InputHandlers;
using FYPManager.Boundary.Services.StrategySelector.Requests.SupervisorRequests;
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

    protected virtual int GetNewPendingRequestCount()
    {
        var supervisorID = ((Supervisor)UserSession.GetCurrentUser()).UserID;
        var newPendingRequestCount = _supervisorController.GetNewPendingRequestCount(supervisorID);
        return newPendingRequestCount;
    }

    protected override void DisplayMenu<T>()
    {
        var newPendingRequestCount = GetNewPendingRequestCount();

        if (newPendingRequestCount > 0)
            OptionalMessage = $"You have {newPendingRequestCount} new pending request(s).";
        base.DisplayMenu<T>();
    }

    public override void Run()
    {
        while (true)
        {
            try
            {
                DisplayMenu<SupervisorBoundary>();

                var choice = NumberHandler.ReadInt(8);
                
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
                        ViewMyPendingRequests();
                        break;

                    case 5:
                        ResolvePendingRequests();
                        break;

                    case 6:
                        ViewMyRequestHistory();
                        break;

                    case 7:
                        RequestStudentTransfer();
                        break;

                    case 8:
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

    protected void CreateNewProject()
    {
        Console.WriteLine("Enter the title of the project: ");
        var projectTitle = StringHandler.ReadString();
        var supervisor = (Supervisor)UserSession.GetCurrentUser();
        _supervisorController.CreateProject(projectTitle, supervisor);

        OptionalSuccessMessage = $"Project '{projectTitle}' created successfully.";
    }

    protected void UpdateExistingProjectTitle()
    {
        Console.WriteLine("Enter the ID of the project you wish to update: ");
        var projectID = NumberHandler.ReadInt();

        Console.WriteLine("Enter the new title of the project: ");
        var newProjectTitle = StringHandler.ReadString();

        _supervisorController.UpdateProjectTitle(projectID, newProjectTitle);

        OptionalSuccessMessage = $"Project title updated successfully.";
    }

    protected void ViewMySubmittedProjects()
    {
        var supervisorID = UserSession.GetCurrentUser().UserID;
        var projects = _supervisorController.GetProjectsBy(supervisorID);
        PaginatorService.Paginate(projects, 4, "My Submitted Projects");
    }

    protected void RequestStudentTransfer()
    {
        throw new NotImplementedException();
    }

    private void ViewMyPendingRequests()
    {
        var supervisorID = UserSession.GetCurrentUser().UserID;
        var requests = _supervisorController.GetRequestsBy(supervisorID);
        PaginatorService.Paginate(requests, 3, "My Pending Requests");
    }

    private void ResolvePendingRequests()
    {
        throw new NotImplementedException();
    }

    private void ViewMyRequestHistory()
    {
        var requestStrategySelector = new SupervisorViewRequestService();
        requestStrategySelector.RunDisplayService(_supervisorController);
    }

}