using FYPManager.Boundary.Services.ConsoleDisplay;
using FYPManager.Boundary.Services.InputHandlers;
using FYPManager.Controller.Utility.Strategy;
using FYPManager.Entity.Projects;

namespace FYPManager.Boundary.Services.StrategySelector;

public static class ViewProjectService
{
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

    public static void ViewAllExistingProjects(IStrategyCompatible<Project> controller)
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
