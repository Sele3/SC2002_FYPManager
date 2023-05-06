using FYPManager.Controller.Utility.Strategy;
using FYPManager.Entity;
using FYPManager.Entity.Projects;
using FYPManager.Entity.Requests;
using FYPManager.Entity.Users;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace FYPManager.Controller.UserController;

public class CoordinatorController : BaseUserController, IStrategyCompatible<Project>
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
    
}
