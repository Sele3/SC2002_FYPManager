using FYPManager.Boundary.Services.ConsoleDisplay;
using FYPManager.Controller.Utility.Strategy;
using FYPManager.Entity.Projects;

namespace FYPManager.Boundary.Services.StrategySelector.Projects;

/// <summary>
/// A service class that implements the <see cref="BaseViewService{T}"/> abstract class for displaying a list of projects
/// </summary>
public class ViewProjectService : BaseViewService<Project>
{
    public ViewProjectService() : base(new ProjectStrategySelector()) { }

    /// <summary>
    /// Displays a paginated list of projects
    /// </summary>
    /// <param name="elements">The list of projects to be displayed</param>
    protected override void DisplayElements(List<Project> elements)
        => PaginatorService.Paginate(elements, 4, "Viewing Projects");
}