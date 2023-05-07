using FYPManager.Entity.Requests;

namespace FYPManager.Controller.Utility.Strategy.Requests;

/// <summary>
/// A filter strategy used to filter a list of requests by request type.
/// </summary>
public class RequestTypeFilterStrategy : IFilterStrategy<BaseRequest>
{
    private RequestType RequestType { get; }

    public RequestTypeFilterStrategy(RequestType requestType)
        => RequestType = requestType;

    public IEnumerable<BaseRequest> Filter(IEnumerable<BaseRequest> elements)
        => elements.Where(e => e.RequestType == RequestType);

    public override string ToString() 
        => $"{GetRequestType} requests only";

    private string GetRequestType => RequestType switch
    {
        RequestType.AllocateProject => "Allocate project",
        RequestType.DeallocateProject => "Deallocate project",
        RequestType.TitleChange => "Title change",
        RequestType.TransferStudent => "Transfer student",
        _ => throw new NotImplementedException()
    };
}
