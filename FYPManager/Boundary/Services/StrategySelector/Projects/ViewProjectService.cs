using FYPManager.Boundary.Services.ConsoleDisplay;
using FYPManager.Boundary.Services.InputHandlers;
using FYPManager.Controller.Utility.Strategy;
using FYPManager.Entity.Projects;

namespace FYPManager.Boundary.Services.StrategySelector.Projects;

/// <summary>
/// A service class that implements the <see cref="BaseViewService{T}"/> abstract class for displaying a list of projects
/// </summary>
public class ViewProjectService : BaseViewService<Project>
{
    /// <summary>
    /// Gets the filter and ordering strategy selected by the user through the project strategy selector
    /// </summary>
    /// <returns>The selected filter and ordering strategy for projects</returns>
    protected override FilterOrderStrategy<Project> GetFilterOrderStrategy()
    {
        var projectStrategySelector = new ProjectStrategySelector();
        var strategy = projectStrategySelector.SelectProjectStrategy();
        return strategy;
    }

    /// <summary>
    /// Displays a paginated list of projects
    /// </summary>
    /// <param name="elements">The list of projects to be displayed</param>
    protected override void DisplayElements(List<Project> elements)
        => PaginatorService.Paginate(elements, 4, "Viewing Projects");
}