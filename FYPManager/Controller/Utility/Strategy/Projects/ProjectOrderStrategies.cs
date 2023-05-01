using FYPManager.Entity.Projects;

namespace FYPManager.Controller.Utility.Strategy.Projects;

public class TitleOrderAscendingStrategy : IOrderStrategy<Project>
{
    public IEnumerable<Project> Order(IEnumerable<Project> elements)
        => elements.OrderBy(p => p.Title);
    public override string ToString()
        => "By title ascending";
}

public class TitleOrderDescendingStrategy : IOrderStrategy<Project>
{
    public IEnumerable<Project> Order(IEnumerable<Project> elements)
        => elements.OrderByDescending(p => p.Title);
    public override string ToString()
        => "By title descending";
}

public class SupervisorOrderAscendingStrategy : IOrderStrategy<Project>
{
    public IEnumerable<Project> Order(IEnumerable<Project> elements)
        => elements.OrderBy(p => p.Supervisor.Name);
    public override string ToString()
        => "By supervisor ascending";
}

public class SupervisorOrderDescendingStrategy : IOrderStrategy<Project>
{
    public IEnumerable<Project> Order(IEnumerable<Project> elements)
        => elements.OrderByDescending(p => p.Supervisor.Name);
    public override string ToString()
        => "By supervisor descending";
}