using FYPManager.Entity.Projects;

namespace FYPManager.Entity.Users;

/// <summary>
/// 
/// </summary>
public class Student : User
{
    public virtual Project? Project { get; set; }

    public override string ToString() =>
        $"StudentID: {UserID}\n"
        + base.ToString();
}