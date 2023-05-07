using FYPManager.Boundary.Services.ConsoleDisplay;
using FYPManager.Boundary.Services.InputHandlers;
using FYPManager.Boundary.Services.StrategySelector.Projects;
using FYPManager.Controller.UserController;
using FYPManager.Controller.Utility;
using FYPManager.Exceptions;

namespace FYPManager.Boundary.UserBoundary;

public class StudentBoundary : BaseUserBoundary, IMenuDisplayable
{
    private readonly StudentController _studentController;

    public StudentBoundary(StudentController studentController)
    {
        _studentController = studentController;
    }  

    public override void Run()
    {
        while (true)
        {
            try
            {
                DisplayMenu<StudentBoundary>();

                var choice = NumberHandler.ReadInt(7);
                
                switch (choice)
                {
                    case 0:
                        Logout();
                        return;

                    case 1:
                        ViewAllAvailableProjects();
                        break;

                    case 2:
                        ViewAllocatedProject();
                        break;

                    case 3:
                        RequestProjectAllocation();
                        break;

                    //case 4:
                    //    RequestProjectTitleChange();
                    //    break;
                    //case 5:
                    //    RequestProjectDeregistration();
                    //    break;

                    case 6:
                        ViewRequestHistory();
                        break;

                    case 7:
                        ChangePassword(_studentController);
                        break;
                }
            }
            catch (CustomException ex)
            {
                OptionalFailureMessage = ex.Message;
            }
        }
    }

    private void ViewAllAvailableProjects()
    {
        var viewProjectService = new ViewProjectService();
        viewProjectService.RunDisplayService(_studentController);
    }

    private void ViewAllocatedProject()
    {
        var studentID = GetCurrentStudentID();
        var project = _studentController.GetProjectByStudent(studentID);

        PaginatorService.Paginate(project, "Your current allocated project is: ");
    }

    private void RequestProjectAllocation()
    {
        Console.WriteLine("Please enter the project ID you wish to request (0 to cancel):");
        var projectID = NumberHandler.ReadInt();

        if (projectID == 0)
            return;

        var studentID = GetCurrentStudentID();

        _studentController.RequestProjectAllocation(studentID, projectID);
        OptionalSuccessMessage = "Project allocation request sent successfully.";
    }

    private void ViewRequestHistory()
    {
        var studentID = GetCurrentStudentID();
        var requests = _studentController.GetRequestHistory(studentID);
        PaginatorService.Paginate(requests, 5, "Your Request History");
    }

    private static string GetCurrentStudentID() => UserSession.GetCurrentUser().UserID;
}
