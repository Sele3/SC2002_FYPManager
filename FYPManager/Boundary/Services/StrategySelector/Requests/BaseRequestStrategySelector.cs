using FYPManager.Controller.Utility.Strategy.Requests;
using FYPManager.Entity.Requests;

namespace FYPManager.Boundary.Services.StrategySelector.Requests;

/// <summary>
/// This abstract class provides the functionality to select a filter and order strategy for viewing requests.
/// </summary>
public abstract class BaseRequestStrategySelector : BaseStrategySelector<BaseRequest>
{
    /// <summary>
    /// Sets the order strategy to order by latest requests first.
    /// </summary>
    protected void SetOrderByLatestFirst()
        => Strategy.OrderStrategy = new DatetimeOrderStrategy();

    /// <summary>
    /// Sets the order strategy to order by oldest requests first.
    /// </summary>
    protected void SetOrderByOldestFirst()
        => Strategy.OrderStrategy = new DatetimeOrderStrategy(latestFirst: false);
}