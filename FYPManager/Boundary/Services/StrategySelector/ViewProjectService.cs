using FYPManager.Boundary.Services.ConsoleDisplay;
using FYPManager.Boundary.Services.InputHandlers;
using FYPManager.Controller.Utility.Strategy;
using FYPManager.Entity.Projects;

namespace FYPManager.Boundary.Services.StrategySelector;

/// <summary>
/// This service provides the functionality to view projects using a filter and order strategy.
/// It provides user options to select a filter and order strategy, and to display the projects using the selected strategy.
/// </summary>
public static class ViewProjectService
{
    /// <summary>
    /// Displays the options for selecting filter and ordering strategy, and to display projects
    /// </summary>
    private static void DisplayOptions()
    {
        Console.Clear();
        Console.WriteLine(
            $"┌─────────────────────────────────┐\n" +
            $"│            OPTIONS              │\n" +
            $"├─────────────────────────────────┤\n" +
            $"│ (S) Select filter and ordering  │\n" +
            $"│ (D) Display                     │\n" +
            $"│ (B) Back                        │\n" +
            $"└─────────────────────────────────┘\n" +
            $"Please select an option:");
    }

    /// <summary>
    /// Displays projects based on the selected filter and order strategy.
    /// </summary>
    /// <param name="controller">The controller that implements the <see cref="IStrategyCompatible{T}"/> interface.</param>
    public static void ViewProjects(IStrategyCompatible<Project> controller)
    {
        var projectStrategySelector = new ProjectStrategySelector();
        var strategy = new FilterOrderStrategy<Project>();

        while (true)
        {
            DisplayOptions();
            var option = StringHandler.ReadString("S", "D", "B");

            switch (option)
            {
                case "S":
                    strategy = projectStrategySelector.SelectProjectStrategy();
                    break;

                case "D":
                    var projects = controller.GetListUsingStrategy(strategy);
                    PaginatorService.Paginate(projects, 4, "Viewing Projects");
                    break;

                case "B":
                    return;
            }
        }
    }
}
