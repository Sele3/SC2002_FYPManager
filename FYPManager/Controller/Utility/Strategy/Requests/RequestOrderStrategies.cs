using FYPManager.Entity.Requests;

namespace FYPManager.Controller.Utility.Strategy.Requests;

/// <summary>
/// An order strategy used to order a list of requests by date.
/// </summary>
public class DatetimeOrderStrategy : IOrderStrategy<BaseRequest>
{
    private bool LatestFirst { get; }

    public DatetimeOrderStrategy(bool latestFirst = true)
    {
        LatestFirst = latestFirst;
    }

    public IEnumerable<BaseRequest> Order(IEnumerable<BaseRequest> elements) => LatestFirst
        ? elements.OrderByDescending(p => p.RequestAt)
        : elements.OrderBy(p => p.RequestAt);

    public override string ToString() => LatestFirst
        ? "Newest requests first"
        : "Oldest requests first";
}