using FYPManager.Entity.Projects;
using FYPManager.Entity.Users;
using System.ComponentModel.DataAnnotations;

namespace FYPManager.Entity.Requests;

/// <summary>
/// A request to change the title of a <see cref="Project"/>.
/// </summary>
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

    public override RequestType RequestType => RequestType.TitleChange;

    public override void Approve()
    {
        base.Approve();
        Project.Title = Title;
    }

    public override string ToString() =>
        $"{base.ToString()}" +
        $"Request Type: Title Change\n" +
        $"RequestID: {RequestID}\n" +
        $"Old Title: {Project.Title}\n" +
        $"New Title: {Title}\n" +
        $"Request By: {RequestByStudentID}\n" +
        $"Request To: {RequestToSupervisorID}\n" +
        $"Requested At: {RequestAt}\n" +
        $"Request Status: {RequestStatus}";
}