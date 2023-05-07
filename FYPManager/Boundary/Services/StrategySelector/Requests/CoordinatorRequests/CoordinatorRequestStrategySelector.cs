using FYPManager.Boundary.Services.ConsoleDisplay;
using FYPManager.Boundary.Services.InputHandlers;
using FYPManager.Controller.Utility.Strategy.Requests;
using FYPManager.Entity.Requests;

namespace FYPManager.Boundary.Services.StrategySelector.Requests.CoordinatorRequests;

/// <summary>
/// This class provides the functionality to select a filter and order strategy for viewing coordinator requests.
/// </summary>
public class CoordinatorRequestStrategySelector : BaseRequestStrategySelector, IMenuDisplayable
{
    public CoordinatorRequestStrategySelector() : base()
    {
        RegisterAction(1, ClearFilter);
        RegisterAction(2, SetFilterByRequestType);
        RegisterAction(3, ClearOrder);
        RegisterAction(4, SetOrderByLatestFirst);
        RegisterAction(5, SetOrderByOldestFirst);
    }

    protected override void DisplayMenu()
    {
        MenuDisplayService.DisplayMenuBody<CoordinatorRequestStrategySelector>();
        Console.WriteLine(
            $"Currently selected\n" +
            $"{Strategy}" +
            $"Please select an option:");
    }

    /// <summary>
    /// Sets the filter strategy to filter by request type.
    /// </summary>
    private void SetFilterByRequestType()
    {
        var requestType = EnumHandler<RequestType>.ReadEnum("Request Type");
        Strategy.FilterStrategy = new RequestTypeFilterStrategy(requestType);
    }
}