namespace FYPManager.Entity.Users;

/// <summary>
/// 
/// </summary>
public class Coordinator : Supervisor
{
    public override string ToString() =>
        $"CoordinatorID: {UserID}\n"
        + base.ToString();
}