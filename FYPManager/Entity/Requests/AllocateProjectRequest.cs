using FYPManager.Entity.Projects;
using FYPManager.Entity.Users;
using System.ComponentModel.DataAnnotations;

namespace FYPManager.Entity.Requests;

public class AllocateProjectRequest : BaseRequest
{
    [Required]
    public int ProjectID { get; set; }
    [Required]
    public string AllocateToStudentID { get; set; } = string.Empty;

    public virtual Project Project { get; set; } = null!;
    public virtual Student AllocateTo { get; set; } = null!;


    public override void Approve()
    {
        base.Approve();
        Project.StudentID = AllocateTo.UserID;
        Project.Status = ProjectStatus.Allocated;
    }

    public override string ToString()
    {
        throw new NotImplementedException();
    }
}
