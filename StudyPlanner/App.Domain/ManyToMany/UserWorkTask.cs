using App.Domain.DbEntities;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain.ManyToMany;

public class UserWorkTask : BaseEntityId
{
    public TimeSpan? TimeSpent { get; set; }
    public DateTime? CompletedAt { get; set; }
    public double? Result { get; set; }
    public EStatus Status { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid WorkTaskId { get; set; }
    public WorkTask? WorkTask { get; set; }
}