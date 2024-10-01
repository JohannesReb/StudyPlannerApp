
using App.Domain;
using App.DTO.v1_0.Entities;
using Base.Domain;

namespace App.DTO.v1_0.ManyToMany;

public class UserWorkTask : BaseEntityId
{
    public TimeSpan? TimeSpent { get; set; }
    public DateTime? CompletedAt { get; set; }
    public double? Result { get; set; }
    public EStatus Status { get; set; }
    public Guid WorkTaskId { get; set; }
    public WorkTask? WorkTask { get; set; }
}