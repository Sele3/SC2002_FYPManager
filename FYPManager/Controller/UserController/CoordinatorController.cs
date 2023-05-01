using FYPManager.Controller.Utility.Strategy;
using FYPManager.Entity;
using FYPManager.Entity.Projects;

namespace FYPManager.Controller.UserController;

public class CoordinatorController : BaseUserController, IStrategyCompatible<Project>
{
    public CoordinatorController(FYPMContext context) : base(context) { }

    public List<Project> GetListUsingStrategy(FilterOrderStrategy<Project> strategy)
    {
        var projects = _context.Projects.AsEnumerable();
        projects = strategy.FilterAndOrder(projects);
        return projects.ToList();
    }
}
