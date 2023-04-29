using FYPManager.Entity;
using FYPManager.Entity.Projects;

namespace FYPManager.Controller.UserController;

public class StudentController : BaseUserController
{
    public StudentController(FYPMContext context) : base(context)  { }

    public List<Project> GetAllAvailableProjects() 
        => _context.Projects
            .Where(p => p.Status == ProjectStatus.Available)
            .ToList();
}
