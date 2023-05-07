using FYPManager.Boundary.Services.ConsoleDisplay;
using FYPManager.Entity.Requests;

namespace FYPManager.Boundary.Services.StrategySelector.Requests.CoordinatorRequests;

/// <summary>
/// A service class that implements the <see cref="BaseViewService{T}"/> abstract class for displaying a list of coordinator requests
/// </summary>
public class CoordinatorViewRequestService : BaseViewService<BaseRequest>
{
    public CoordinatorViewRequestService() : base(new CoordinatorRequestStrategySelector()) { }

    protected override void DisplayElements(List<BaseRequest> elements)
        => PaginatorService.Paginate(elements, 5, "Viewing All Pending Requests");
}