using FYPManager.Controller.Utility.Strategy;
using FYPManager.Entity;
using FYPManager.Entity.Projects;

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
}
