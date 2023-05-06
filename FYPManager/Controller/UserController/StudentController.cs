using FYPManager.Controller.Utility.Strategy;
using FYPManager.Entity;
using FYPManager.Entity.Projects;
using FYPManager.Entity.Requests;
using FYPManager.Entity.Users;
using FYPManager.Exceptions;

namespace FYPManager.Controller.UserController;

public class StudentController : BaseUserController, IStrategyCompatible<Project>
{
    public StudentController(FYPMContext context) : base(context)  { }

    public List<Project> GetListUsingStrategy(FilterOrderStrategy<Project> strategy)
    {
        var availableProjects = _context
            .Projects
            .Where(p => p.Status == ProjectStatus.Available)
            .AsEnumerable();

        availableProjects = strategy.FilterAndOrder(availableProjects);
        return availableProjects.ToList();
    }

    public Project GetProjectByStudent(string studentID)
    {
        var student = GetUser<Student>(studentID);
        return student.Project
            ?? throw new ProjectException("You have not been allocated a project yet.");
    }

    public void RequestProjectAllocation(string studentID, int projectID)
    {
        var student = GetUser<Student>(studentID);
        var project = GetProject(projectID);

        ValidateValidProjectAllocation(student, project);

        project.Status = ProjectStatus.Reserved;

        var projectAllocateRequest = new AllocateProjectRequest()
        {
            ProjectID = projectID,
            AllocateToStudentID = studentID,
        };
        _context.AllocateProjectRequests.Add(projectAllocateRequest);
        _context.SaveChanges();
    }

    private Project GetProject(int projectID) => _context
            .Projects
            .FirstOrDefault(p => p.ProjectID == projectID)
            ?? throw new ProjectException("Project not found");

    private void ValidateValidProjectAllocation(Student student, Project project)
    {
        ValidateStudentIsNotDeregistered(student);
        ValidateStudentHasNoProject(student);
        ValidateProjectIsAvailable(project);
        ValidateStudentHasNoAllocationRequests(student);
    }

    private static void ValidateStudentIsNotDeregistered(Student student)
    {
        if (student.IsDeallocated)
            throw new AccountException("You have been deregistered from the system.");
    }

    private static void ValidateProjectIsAvailable(Project project)
    {
        if (project.Status != ProjectStatus.Available)
            throw new ProjectException("The project you have selected is not available.");
    }

    private static void ValidateStudentHasNoProject(Student student)
    {
        if (student.Project != null)
            throw new ProjectException("You have already been allocated a project.");
    }

    private void ValidateStudentHasNoAllocationRequests(Student student)
    {
        var hasAllocationRequest = _context
            .AllocateProjectRequests
            .Any(r => r.AllocateToStudentID.Equals(student.UserID));

        if (hasAllocationRequest)
            throw new RequestException("You have already requested a project allocation.");
    }

    public List<BaseRequest> GetRequestHistory(string studentID)
    {
        GetUser<Student>(studentID);

        var requests = new List<BaseRequest>();
        
        requests.AddRange(
            _context
            .AllocateProjectRequests
            .Where(s => s.AllocateToStudentID.Equals(studentID))
            );
        requests.AddRange(
            _context
            .DeallocateProjectRequests
            .Where(s => s.DeallocateStudentID.Equals(studentID))
            );
        requests.AddRange(
            _context
            .TitleChangeRequests
            .Where(s => s.RequestByStudentID.Equals(studentID))
            );

        return requests;
    }
}
