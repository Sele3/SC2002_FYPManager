using FYPManager.Boundary.Services.ConsoleDisplay;
using FYPManager.Boundary.Services.InputHandlers;
using FYPManager.Boundary.Services.StrategySelector;
using FYPManager.Controller.UserController;
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

                    //case 2:
                    //    ViewAllocatedProject();
                    //    break;
                    //case 3:
                    //    RequestProjectAllocation();
                    //    break;
                    //case 4:
                    //    RequestProjectTitleChange();
                    //    break;
                    //case 5:
                    //    RequestProjectDeregistration();
                    //    break;
                    //case 6:
                    //    ViewRequestHistory();
                    //    break;

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
        => ViewProjectService.ViewProjects(_studentController);
}
