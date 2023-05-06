using FYPManager.Entity.Projects;
using FYPManager.Entity.Users;
using System.ComponentModel.DataAnnotations;

namespace FYPManager.Entity.Requests;

/// <summary>
/// A request to transfer a <see cref="Project"/> from a <see cref="Supervisor"/> to another <see cref="Supervisor"/>.
/// </summary>
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

    public override string ToString() =>
        $"--- Transfer Student ---\n" +
        $"RequestID: {RequestID}\n" +
        $"Project: {Project.Title}\n" +
        $"Transfer From: {TransferFromSupervisorID}\n" +
        $"Transfer To: {TransferToSupervisorID}\n" +
        $"Requested At: {RequestAt}\n" +
        $"Request Status: {RequestStatus}";
}
