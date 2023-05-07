using FYPManager.Boundary.Services.ConsoleDisplay;

namespace FYPManager.Boundary.Services.StrategySelector.Requests.SupervisorRequests;

/// <summary>
/// This class provides the functionality to select a filter and order strategy for viewing supervisor requests.
/// </summary>
public class SupervisorRequestStrategySelector : BaseRequestStrategySelector, IMenuDisplayable
{
    public SupervisorRequestStrategySelector() : base()
    {
        RegisterAction(1, ClearOrder);
        RegisterAction(2, SetOrderByLatestFirst);
        RegisterAction(3, SetOrderByOldestFirst);
    }

    protected override void DisplayMenu()
    {
        MenuDisplayService.DisplayMenuBody<SupervisorRequestStrategySelector>();
        Console.WriteLine(
            $"Currently selected\n" +
            $"Order: {Strategy.OrderStrategy}\n" +
            $"Please select an option:");
    }
}