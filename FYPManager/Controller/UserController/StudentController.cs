using FYPManager.Entity;
using FYPManager.Entity.Projects;

namespace FYPManager.Controller.UserController;

public class StudentController : BaseUserController
{
    private readonly FYPMContext _context;

    public StudentController(FYPMContext context)
    {
        _context = context;
    }

    public List<Project> GetAllAvailableProjects() 
        => _context.Projects
            .Where(p => p.Status == ProjectStatus.Available)
            .ToList();
}
