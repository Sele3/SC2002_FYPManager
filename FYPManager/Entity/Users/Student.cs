using FYPManager.Entity.Projects;
using System.ComponentModel.DataAnnotations;

namespace FYPManager.Entity.Users;

/// <summary>
/// 
/// </summary>
public class Student : User
{
    [Required]
    public bool IsDeallocated { get; set; } = false;
    public virtual Project? Project { get; set; }

    public override string ToString() =>
        $"StudentID: {UserID}\n"
        + base.ToString();
}