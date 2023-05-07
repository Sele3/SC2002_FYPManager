using FYPManager.Entity.Projects;

namespace FYPManager.Controller.Utility.Strategy.Projects;

/// <summary>
/// An abstract base class for order strategies used to order a list of projects.
/// </summary>
public abstract class BaseProjectOrderStrategy : IOrderStrategy<Project>
{
    protected bool IsDescending { get; }
    protected BaseProjectOrderStrategy(bool isDescending = false)
    {
        IsDescending = isDescending;
    }

    public abstract IEnumerable<Project> Order(IEnumerable<Project> elements);
    public abstract override string ToString();
}

/// <summary>
/// An order strategy used to order a list of projects by title.
/// </summary>
public class TitleOrderStrategy : BaseProjectOrderStrategy
{
    public TitleOrderStrategy(bool isDescending = false) : base(isDescending) { }

    public override IEnumerable<Project> Order(IEnumerable<Project> elements) => IsDescending
        ? elements.OrderByDescending(p => p.Title)
        : elements.OrderBy(p => p.Title);

    public override string ToString() 
        => $"By title {(IsDescending ? "descending" : "ascending")}";
}

/// <summary>
/// An order strategy used to order a list of projects by supervisor name.
/// </summary>
public class SupervisorOrderStrategy : BaseProjectOrderStrategy
{
    public SupervisorOrderStrategy(bool isDescending = false) : base(isDescending) { }

    public override IEnumerable<Project> Order(IEnumerable<Project> elements) => IsDescending
        ? elements.OrderByDescending(p => p.Supervisor.Name)
        : elements.OrderBy(p => p.Supervisor.Name);

    public override string ToString()
        => $"By supervisor {(IsDescending ? "descending" : "ascending")}";
}