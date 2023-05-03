using FYPManager.Entity.Projects;
using FYPManager.Entity.Users;
using System.ComponentModel.DataAnnotations;

namespace FYPManager.Entity.Requests;

public class TransferStudentRequest : BaseRequest
{
    [Required]
    public string TransferFromSupervisorID { get; set; } = string.Empty;
    [Required]
    public string TransferToSupervisorID { get; set; } = string.Empty;
    [Required]
    public int ProjectID { get; set; }

    public virtual Supervisor TransferFrom { get; set; } = null!;
    public virtual Supervisor TransferTo { get; set; } = null!;
    public virtual Project Project { get; set; } = null!;

    public override void Approve()
    {
        base.Approve();
        Project.SupervisorID = TransferTo.UserID;
    }

    public override string ToString()
    {
        throw new NotImplementedException();
    }
}
