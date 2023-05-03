using FYPManager.Boundary.Services.ConsoleDisplay;
using FYPManager.Boundary.Services.InputHandlers;
using FYPManager.Controller.Utility.Strategy;
using FYPManager.Controller.Utility.Strategy.Projects;
using FYPManager.Entity.Projects;

namespace FYPManager.Boundary.Services.StrategySelector.Projects;

/// <summary>
/// This class provides the functionality to select a filter and order strategy for viewing projects.
/// </summary>
public class ProjectStrategySelector : IMenuDisplayable
{
    private FilterOrderStrategy<Project> Strategy { get; set; }
    private Dictionary<int, Action> ChoiceDict { get; set; }

    public ProjectStrategySelector()
    {
        Strategy = new();
        ChoiceDict = new()
        {
            { 1, ClearFilter },
            { 2, SetFilterByTitle },
            { 3, SetFilterByStatus },
            { 4, SetFilterBySupervisor },
            { 5, ClearOrder },
            { 6, SetOrderByTitleAscending },
            { 7, SetOrderByTitleDescending },
            { 8, SetOrderBySupervisorAscending },
            { 9, SetOrderBySupervisorDescending }
        };
    }

    /// <summary>
    /// Displays the current selected options.
    /// </summary>
    private void DisplayMenu()
    {
        MenuDisplayService.DisplayMenuBody<ProjectStrategySelector>();
        Console.WriteLine(
            $"Currently selected\n" +
            $"{Strategy}" +
            $"Please select an option:");
    }

    /// <summary>
    /// Selects a custom filter and order strategy chosen by the user.
    /// </summary>
    /// <returns>The selected filter and order strategy.</returns>
    public FilterOrderStrategy<Project> SelectProjectStrategy()
    {
        while (true)
        {
            DisplayMenu();

            var choice = NumberHandler.ReadInt(9);

            if (choice == 0)
                return Strategy;

            ChoiceDict.TryGetValue(choice, out var action);
            action?.Invoke();
        }
    }

    /// <summary>
    /// Clears the current filter strategy.
    /// </summary>
    private void ClearFilter()
        => Strategy.FilterStrategy = null;

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
    /// Clears the current order strategy.
    /// </summary>
    private void ClearOrder()
        => Strategy.OrderStrategy = null;

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