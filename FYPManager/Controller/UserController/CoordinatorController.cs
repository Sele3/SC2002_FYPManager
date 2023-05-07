using FYPManager.Controller.Utility.Strategy;
using FYPManager.Entity;
using FYPManager.Entity.Projects;
using FYPManager.Entity.Requests;
using FYPManager.Exceptions;

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

    public List<BaseRequest> GetAllPendingRequests()
    {
        var list = new List<BaseRequest>();
        list.AddRange(GetPendingRequests<AllocateProjectRequest>());
        list.AddRange(GetPendingRequests<DeallocateProjectRequest>());
        list.AddRange(GetPendingRequests<TitleChangeRequest>());
        list.AddRange(GetPendingRequests<TransferStudentRequest>());

        list.Sort((a, b) => a.RequestAt.CompareTo(b.RequestAt));
        return list;
    }

    public void MarkRequestsAsSeen(List<BaseRequest> requests)
    {
        requests.ForEach(r => r.IsSeen = true);
        _context.SaveChanges();
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

    public BaseRequest GetRequestByID(int requestID)
    {
        var request = GetRequest<AllocateProjectRequest>(requestID)
            ?? GetRequest<DeallocateProjectRequest>(requestID)
            ?? GetRequest<TitleChangeRequest>(requestID)
            ?? GetRequest<TransferStudentRequest>(requestID)
            ?? throw new RequestException("Request not found");
        return request;
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

    private List<T> GetRequestHistory<T>() where T : BaseRequest
    {
        var dbSet = _context.Set<T>();
        var list = dbSet
            .Where(d => d.RequestStatus != RequestStatus.Pending)
            .ToList();

        return list;
    }

    private List<T> GetPendingRequests<T>() where T : BaseRequest
    {
        var dbSet = _context.Set<T>();

        return dbSet
            .Where(r => r.RequestStatus == RequestStatus.Pending)
            .ToList();
    }

    private BaseRequest? GetRequest<T>(int requestID) where T : BaseRequest
    {
        var dbSet = _context.Set<T>();
        var request = dbSet
            .FirstOrDefault(r => r.RequestID == requestID);
        return request;
    }

    public void ApproveRequest(BaseRequest request)
    {
        throw new NotImplementedException();
    }

    public static void RejectRequest(BaseRequest request)
        => request.Reject();
}
