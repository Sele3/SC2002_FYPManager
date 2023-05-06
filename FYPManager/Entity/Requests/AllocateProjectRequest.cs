using FYPManager.Entity.Projects;
using FYPManager.Entity.Users;
using System.ComponentModel.DataAnnotations;

namespace FYPManager.Entity.Requests;

/// <summary>
/// A request to allocate a <see cref="Project"/> to a <see cref="Student"/>.
/// </summary>
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

    public override void Reject()
    {
        base.Reject();
        Project.Status = ProjectStatus.Available;
    }

    public override string ToString() =>
        $"--- Allocate Project ---\n" +
        $"RequestID: {RequestID}\n" +
        $"Project: {Project.Title}\n" +
        $"StudentID: {AllocateToStudentID}\n" +
        $"Requested At: {RequestAt}\n" +
        $"Request Status: {RequestStatus}";
}
