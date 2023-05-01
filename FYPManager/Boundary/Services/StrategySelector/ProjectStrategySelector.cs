using FYPManager.Boundary.Services.ConsoleDisplay;
using FYPManager.Boundary.Services.InputHandlers;
using FYPManager.Controller.Utility.Strategy;
using FYPManager.Controller.Utility.Strategy.Projects;
using FYPManager.Entity.Projects;

namespace FYPManager.Boundary.Services.StrategySelector;

public class ProjectStrategySelector : IMenuDisplayable
{
    private FilterOrderStrategy<Project> Strategy { get; set; }
    private Dictionary<int, Action> ChoiceMap { get; set; }

    public ProjectStrategySelector()
    {
        Strategy = new();
        ChoiceMap = new()
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

    private void DisplayMenu() => Console.WriteLine(
        $"{MenuDisplayService<ProjectStrategySelector>.GetMenuDisplayText()}" +
        $"Currently selected\n" +
        $"{Strategy}" +
        $"Please select an option:");

    public FilterOrderStrategy<Project> SelectProjectStrategy()
    {
        while (true)
        {
            DisplayMenu();

            var choice = NumberHandler.ReadInt(9);

            if (choice == 0)
                return Strategy;

            ChoiceMap.TryGetValue(choice, out var action);
            action?.Invoke();
        }
    }

    private void ClearFilter()
        => Strategy.FilterStrategy = null;

    private void SetFilterByTitle()
    {
        Console.WriteLine("Please enter a title to filter by:");
        var keyword = StringHandler.ReadString();
        Strategy.FilterStrategy = new TitleFilterStrategy(keyword);
    }

    private void SetFilterByStatus()
    {
        var status = EnumHandler<ProjectStatus>.ReadEnum("Project Status");
        Strategy.FilterStrategy = new StatusFilterStrategy(status);
    }

    private void SetFilterBySupervisor()
    {
        Console.WriteLine("Please enter a supervisor to filter by:");
        var keyword = StringHandler.ReadString();
        Strategy.FilterStrategy = new SupervisorFilterStrategy(keyword);
    }

    private void ClearOrder()
        => Strategy.OrderStrategy = null;

    private void SetOrderByTitleAscending()
        => Strategy.OrderStrategy = new TitleOrderAscendingStrategy();

    private void SetOrderByTitleDescending()
        => Strategy.OrderStrategy = new TitleOrderDescendingStrategy();

    private void SetOrderBySupervisorAscending()
        => Strategy.OrderStrategy = new SupervisorOrderAscendingStrategy();

    private void SetOrderBySupervisorDescending()
        => Strategy.OrderStrategy = new SupervisorOrderDescendingStrategy();
}