using FYPManager.Entity.Projects;

namespace FYPManager.Controller.Utility.Strategy.Projects;

public class StatusFilterStrategy : IFilterStrategy<Project>
{
    private ProjectStatus Status { get; set; }

    public StatusFilterStrategy(ProjectStatus status)
    {
        Status = status;
    }

    public IEnumerable<Project> Filter(IEnumerable<Project> elements)
        => elements.Where(p => p.Status == Status);

    public override string ToString()
        => $"Projects with '{Status}' status";
}

public class TitleFilterStrategy : IFilterStrategy<Project>
{
    private string Keyword { get; set; }
    public TitleFilterStrategy(string keyword)
    {
        Keyword = keyword;
    }

    public IEnumerable<Project> Filter(IEnumerable<Project> elements)
        => elements.Where(p => p.Title.Contains(Keyword, StringComparison.OrdinalIgnoreCase));

    public override string ToString()
        => $"Projects with '{Keyword}' in title (case insensitive)";
}

public class SupervisorFilterStrategy : IFilterStrategy<Project>
{
    private string Keyword { get; set; }
    public SupervisorFilterStrategy(string keyword)
    {
        Keyword = keyword;
    }

    public IEnumerable<Project> Filter(IEnumerable<Project> elements)
        => elements.Where(p => p.Supervisor.Name.Contains(Keyword, StringComparison.OrdinalIgnoreCase));
    public override string ToString()
        => $"Projects with '{Keyword}' in supervisor name (case insensitive)";
}