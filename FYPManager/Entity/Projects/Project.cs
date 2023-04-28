using FYPManager.Entity.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYPManager.Entity.Projects;

/// <summary>
/// The different statuses a <see cref="Project"/> can have.
/// </summary>
public enum ProjectStatus
{
    /// <summary>
    /// The <see cref="Project"/> is available for allocation to a <see cref="Student"/>.
    /// </summary>
    Available,

    /// <summary>
    /// The <see cref="Project"/> has been reserved by a <see cref="Student"/> but has not yet been allocated to them.
    /// </summary>
    Reserved,

    /// <summary>
    /// The <see cref="Project"/> is unavailable for allocation to a <see cref="Student"/>.
    /// </summary>
    Unavailable,

    /// <summary>
    /// The <see cref="Project"/> has been allocated to a <see cref="Student"/>.
    /// </summary>
    Allocated
}

/// <summary>
/// A <see cref="Project"/> which is available for allocation to a <see cref="Student"/>.
/// </summary>
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
    public virtual Supervisor Supervisor { get; set; } = null!;

    public string? StudentID { get; set; }

    public virtual Student? Student { get; set; }

    [EnumDataType(typeof(ProjectStatus))]
    public ProjectStatus Status { get; set; } = ProjectStatus.Available;

    public override string ToString() =>
        $"ProjectID: {ProjectID}\n" +
        $"Title: {Title}\n" +
        $"Supervisor Name: {Supervisor.Name}\n" +
        $"Supervisor Email: {Supervisor.Email}\n" +
        $"{GetStudentInfoIfAvailable()}" +
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

    private string GetStudentInfoIfAvailable()
        => StudentID == null 
            ? "" 
            : $"Student Name: {Student!.Name}\n" +
              $"Student Email: {Student!.Email}\n";
}