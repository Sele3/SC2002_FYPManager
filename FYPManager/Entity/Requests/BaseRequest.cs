using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FYPManager.Boundary.Services.ConsoleDisplay;

namespace FYPManager.Entity.Requests;

/// <summary>
/// Abstract class representing a request in the system
/// </summary>
public abstract class BaseRequest : IDisplayable
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int RequestID { get; set; }

    [Required]
    public DateTime RequestAt { get; set; } = DateTime.Now;

    [EnumDataType(typeof(RequestStatus))]
    [Required]
    public RequestStatus RequestStatus { get; set; } = RequestStatus.Pending;

    [Required]
    public bool IsSeen { get; set; } = false;

    public abstract RequestType RequestType { get; }

    public override string ToString() => IsSeen
        ? ""
        : "[NEW]\n";

    public virtual void Approve()
        => RequestStatus = RequestStatus.Approved;
  
    public virtual void Reject()
        => RequestStatus = RequestStatus.Rejected;
}

/// <summary>
/// The different statuses a <see cref="BaseRequest"/> can have.
/// </summary>
public enum RequestStatus
{
    /// <summary>
    /// The <see cref="BaseRequest"/> is pending approval.
    /// </summary>
    Pending,
    /// <summary>
    /// The <see cref="BaseRequest"/> has been approved.
    /// </summary>
    Approved,
    /// <summary>
    /// The <see cref="BaseRequest"/> has been rejected.
    /// </summary>
    Rejected
}

/// <summary>
/// The different types of <see cref="BaseRequest"/>s that can be made.
/// </summary>
public enum RequestType
{
    AllocateProject,
    DeallocateProject,
    TitleChange,
    TransferStudent,
}