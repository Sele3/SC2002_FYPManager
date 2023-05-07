using FYPManager.Controller.Utility.Strategy;
using FYPManager.Entity;
using FYPManager.Entity.Projects;
using FYPManager.Entity.Requests;

namespace FYPManager.Controller.UserController;

public class CoordinatorController : BaseUserController, IStrategyCompatible<Project>, IStrategyCompatible<BaseRequest>
{
    public CoordinatorController(FYPMContext context) : base(context) { }

    public List<Project> GetListUsingStrategy(FilterOrderStrategy<Project> strategy)
    {
        var projects = _context.Projects.AsEnumerable();
        projects = strategy.FilterAndOrder(projects);
        return projects.ToList();
    }

    public int GetNewPendingRequestCount()
    {
        var pendingRequestCount = GetNewPendingRequestCount<AllocateProjectRequest>()
            + GetNewPendingRequestCount<DeallocateProjectRequest>()
            + GetNewPendingRequestCount<TitleChangeRequest>()
            + GetNewPendingRequestCount<TransferStudentRequest>();
        return pendingRequestCount;
    }

    private int GetNewPendingRequestCount<T>() where T : BaseRequest
    {
        var dbSet = _context.Set<T>();
        var pendingRequestCount = dbSet
            .Where(r => r.RequestStatus == RequestStatus.Pending
                && !r.IsSeen)
            .Count();

        return pendingRequestCount;
    }

    public List<BaseRequest> GetAllRequestHistory()
    {
        var list = new List<BaseRequest>();
        list.AddRange(GetRequestHistory<AllocateProjectRequest>());
        list.AddRange(GetRequestHistory<DeallocateProjectRequest>());
        list.AddRange(GetRequestHistory<TitleChangeRequest>());
        list.AddRange(GetRequestHistory<TransferStudentRequest>());

        list.Sort((a, b) => a.RequestAt.CompareTo(b.RequestAt));
        return list;
    }

    private List<T> GetRequestHistory<T>() where T : BaseRequest
    {
        var dbSet = _context.Set<T>();
        var list = dbSet
            .Where(d => d.RequestStatus != RequestStatus.Pending)
            .ToList();

        return list;
    }

    public List<BaseRequest> GetListUsingStrategy(FilterOrderStrategy<BaseRequest> strategy)
    {
        var list = new List<BaseRequest>();
        list.AddRange(GetRequestHistory<AllocateProjectRequest>());
        list.AddRange(GetRequestHistory<DeallocateProjectRequest>());
        list.AddRange(GetRequestHistory<TitleChangeRequest>());
        list.AddRange(GetRequestHistory<TransferStudentRequest>());

        var filteredList = strategy
            .FilterAndOrder(list)
            .ToList();
        return filteredList;
    }

    private IEnumerable<T> GetPendingRequests<T>() where T : BaseRequest
    {
        var dbSet = _context.Set<T>();
        var pendingRequests = dbSet
            .Where(r => r.RequestStatus == RequestStatus.Pending)
            .AsEnumerable();
        return pendingRequests;
    }

    private void MarkRequestAsSeen(List<BaseRequest> requests)
    {
        requests.ForEach(r => r.IsSeen = true);
        _context.SaveChanges();
    }
}
