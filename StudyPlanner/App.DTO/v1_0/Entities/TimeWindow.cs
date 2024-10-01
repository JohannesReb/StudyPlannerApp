using Base.Domain;

namespace App.DTO.v1_0.Entities;

public class TimeWindow : BaseEntityId
{
    public DateTime From { get; set; }
    public DateTime Until { get; set; }
    public TimeSpan FreeTime { get; set; }
    public Guid UserId { get; set; }
}