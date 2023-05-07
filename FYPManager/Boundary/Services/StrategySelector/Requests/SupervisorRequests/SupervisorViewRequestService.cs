using FYPManager.Boundary.Services.ConsoleDisplay;
using FYPManager.Entity.Requests;

namespace FYPManager.Boundary.Services.StrategySelector.Requests.SupervisorRequests;

/// <summary>
/// A service class that implements the <see cref="BaseViewService{T}"/> abstract class for displaying a list of supervisor requests
/// </summary>
public class SupervisorViewRequestService : BaseViewService<BaseRequest>
{
    public SupervisorViewRequestService() : base(new SupervisorRequestStrategySelector()) { }

    protected override void DisplayElements(List<BaseRequest> elements)
        => PaginatorService.Paginate(elements, 5, "Viewing My Pending Requests");
}