using App.BLL.DTO.Entities;
using Base.Domain;

namespace App.BLL.DTO.ManyToMany;

public class WorkTaskTimeWindow : BaseEntityId
{
    public Guid WorkTaskId { get; set; }
    public WorkTask? WorkTask { get; set; }
    public Guid TimeWindowId { get; set; }
    public TimeWindow? TimeWindow { get; set; }
}