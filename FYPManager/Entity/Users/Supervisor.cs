using FYPManager.Entity.Projects;

namespace FYPManager.Entity.Users;

/// <summary>
/// 
/// </summary>
public class Supervisor : User
{
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public override string ToString() =>
        $"SupervisorID: {UserID}\n"
        + base.ToString();
}