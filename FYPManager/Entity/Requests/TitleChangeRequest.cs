using FYPManager.Entity.Projects;
using FYPManager.Entity.Users;
using System.ComponentModel.DataAnnotations;

namespace FYPManager.Entity.Requests;

public class TitleChangeRequest : BaseRequest
{
    [Required] 
    public string Title { get; set; } = string.Empty;
    [Required]
    public int ProjectID { get; set; }
    [Required]
    public string RequestByStudentID { get; set; } = string.Empty;
    [Required]
    public string RequestToSupervisorID { get; set; } = string.Empty;

    public virtual Project Project { get; set; } = null!;
    public virtual Student RequestBy { get; set; } = null!;
    public virtual Supervisor RequestTo { get; set; } = null!;

    public override void Approve()
    {
        base.Approve();
        Project.Title = Title;
    }

    public override string ToString()
    {
        throw new NotImplementedException();
    }
}