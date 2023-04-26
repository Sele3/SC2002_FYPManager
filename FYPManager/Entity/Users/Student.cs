using FYPManager.Entity.Projects;

namespace FYPManager.Entity.Users;

public class Student : User
{
    public Project? Project { get; set; }
}
