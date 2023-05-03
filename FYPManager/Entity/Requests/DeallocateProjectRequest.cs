using FYPManager.Entity.Projects;
using FYPManager.Entity.Users;
using System.ComponentModel.DataAnnotations;

namespace FYPManager.Entity.Requests;

public class DeallocateProjectRequest : BaseRequest
{
    [Required]
    public string DeallocateStudentID { get; set; } = string.Empty;

    public virtual Student DeallocateStudent { get; set; } = null!;

    public override void Approve()
    {
        base.Approve();
        DeallocateStudent.Project!.Status = ProjectStatus.Available;
        DeallocateStudent.Project.StudentID = null;
        DeallocateStudent.IsDeallocated = true;
    }

    public override string ToString()
    {
        throw new NotImplementedException();
    }
}
