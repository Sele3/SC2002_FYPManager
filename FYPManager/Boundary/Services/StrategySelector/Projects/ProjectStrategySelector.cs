using FYPManager.Boundary.Services.ConsoleDisplay;
using FYPManager.Boundary.Services.InputHandlers;
using FYPManager.Controller.Utility.Strategy.Projects;
using FYPManager.Entity.Projects;

namespace FYPManager.Boundary.Services.StrategySelector.Projects;

/// <summary>
/// This class provides the functionality to select a filter and order strategy for viewing projects.
/// </summary>
public class ProjectStrategySelector : BaseStrategySelector<Project>, IMenuDisplayable
{
    public ProjectStrategySelector() : base() 
    {
        RegisterAction(1, ClearFilter);
        RegisterAction(2, SetFilterByTitle);
        RegisterAction(3, SetFilterByStatus);
        RegisterAction(4, SetFilterBySupervisor);
        RegisterAction(5, ClearOrder);
        RegisterAction(6, SetOrderByTitleAscending);
        RegisterAction(7, SetOrderByTitleDescending);
        RegisterAction(8, SetOrderBySupervisorAscending);
        RegisterAction(9, SetOrderBySupervisorDescending);
    }

    /// <summary>
    /// Displays the current selected options.
    /// </summary>
    protected override void DisplayMenu()
    {
        MenuDisplayService.DisplayMenuBody<ProjectStrategySelector>();
        Console.WriteLine(
            $"Currently selected\n" +
            $"{Strategy}" +
            $"Please select an option:");
    }

    /// <summary>
    /// Sets the filter strategy to filter by title.
    /// </summary>
    private void SetFilterByTitle()
    {
        Console.WriteLine("Please enter a title to filter by:");
        var keyword = StringHandler.ReadString();
        Strategy.FilterStrategy = new TitleFilterStrategy(keyword);
    }

    /// <summary>
    /// Sets the filter strategy to filter by status.
    /// </summary>
    private void SetFilterByStatus()
    {
        var status = EnumHandler<ProjectStatus>.ReadEnum("Project Status");
        Strategy.FilterStrategy = new StatusFilterStrategy(status);
    }

    /// <summary>
    /// Sets the filter strategy to filter by supervisor.
    /// </summary>
    private void SetFilterBySupervisor()
    {
        Console.WriteLine("Please enter a supervisor to filter by:");
        var keyword = StringHandler.ReadString();
        Strategy.FilterStrategy = new SupervisorFilterStrategy(keyword);
    }

    /// <summary>
    /// Sets the order strategy to order by title in ascending order.
    /// </summary>
    private void SetOrderByTitleAscending()
        => Strategy.OrderStrategy = new TitleOrderStrategy();

    /// <summary>
    /// Sets the order strategy to order by title in descending order.
    /// </summary>
    private void SetOrderByTitleDescending()
        => Strategy.OrderStrategy = new TitleOrderStrategy(isDescending: true);

    /// <summary>
    /// Sets the order strategy to order by supervisor in ascending order.
    /// </summary>
    private void SetOrderBySupervisorAscending()
        => Strategy.OrderStrategy = new SupervisorOrderStrategy();

    /// <summary>
    /// Sets the order strategy to order by supervisor in descending order.
    /// </summary>
    private void SetOrderBySupervisorDescending()
        => Strategy.OrderStrategy = new SupervisorOrderStrategy(isDescending: true);
}