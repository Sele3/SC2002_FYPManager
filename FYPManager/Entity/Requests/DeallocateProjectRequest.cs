using FYPManager.Entity.Projects;
using FYPManager.Entity.Users;
using System.ComponentModel.DataAnnotations;

namespace FYPManager.Entity.Requests;

/// <summary>
/// A request to deallocate a <see cref="Project"/> from a <see cref="Student"/>.
/// </summary>
public class DeallocateProjectRequest : BaseRequest
{
    [Required]
    public string DeallocateStudentID { get; set; } = string.Empty;

    public virtual Student DeallocateStudent { get; set; } = null!;

    public override RequestType RequestType => RequestType.DeallocateProject;

    public override void Approve()
    {
        base.Approve();
        DeallocateStudent.Project!.Status = ProjectStatus.Available;
        DeallocateStudent.Project.StudentID = null;
        DeallocateStudent.IsDeallocated = true;
    }

    public override string ToString() =>
        $"--- Deallocate Project ---\n" +
        $"RequestID: {RequestID}\n" +
        $"Project: {DeallocateStudent.Project!.Title}" +
        $"StudentID: {DeallocateStudentID}\n" +
        $"Requested At: {RequestAt}\n" +
        $"Request Status: {RequestStatus}";
}