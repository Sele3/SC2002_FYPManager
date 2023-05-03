using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYPManager.Entity.Requests;

public enum RequestStatus
{
    Pending,
    Approved,
    Rejected
}

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

    public override abstract string ToString();

    public virtual void Approve()
        => RequestStatus = RequestStatus.Approved;
  
    public void Reject()
        => RequestStatus = RequestStatus.Rejected;
}