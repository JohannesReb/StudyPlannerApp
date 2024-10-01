using App.DTO.v1_0.Entities;
using Base.Domain;

namespace App.DTO.v1_0.ManyToMany;

public class WorkTaskTimeWindow : BaseEntityId
{
    public Guid WorkTaskId { get; set; }
    public WorkTask? WorkTask { get; set; }
    public Guid TimeWindowId { get; set; }
    public TimeWindow? TimeWindow { get; set; }
}