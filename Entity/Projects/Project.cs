using FYPManager.Entity.Users;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Identity.Client;
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
}
