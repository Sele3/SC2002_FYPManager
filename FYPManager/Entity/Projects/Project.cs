using FYPManager.Entity.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYPManager.Entity.Projects;

public enum ProjectStatus
{
    Available, Reserved, Unavailable, Allocated
}

public class Project
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int ProjectID { get; set; }

    [Required]
    public string Title { get; set; } = "";

    [Required]
    public string SupervisorID { get; set; } = "";

    [Required]
    public Supervisor Supervisor { get; set; } = null!;

    public string? StudentID { get; set; }

    public Student? Student { get; set; }

    [EnumDataType(typeof(ProjectStatus))]
    public ProjectStatus Status { get; set; } = ProjectStatus.Available;

    public override string ToString() =>
        $"ProjectID: {ProjectID}\n" +
        $"Title: {Title}\n" +
        $"SupervisorID: {SupervisorID}\n" +
        $"StudentID: {StudentID}\n" +
        $"Status: {Status}";

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var other = (Project)obj;
        return Title.Equals(other.Title) &&
            SupervisorID.Equals(other.SupervisorID) &&
            Supervisor.Equals(other.Supervisor) &&
            Status.Equals(other.Status);
    }

    public override int GetHashCode()
        => base.GetHashCode();
}
