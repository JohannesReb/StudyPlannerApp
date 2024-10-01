using App.DAL.DTO.Entities;
using Base.Domain;

namespace App.DAL.DTO.ManyToMany;

public class WorkTaskTimeWindow : BaseEntityId
{
    public Guid WorkTaskId { get; set; }
    public WorkTask? WorkTask { get; set; }
    public Guid TimeWindowId { get; set; }
    public TimeWindow? TimeWindow { get; set; }
}