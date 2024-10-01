using System.ComponentModel.DataAnnotations;
using App.BLL.DTO.ManyToMany;
using App.Domain.Identity;
using Base.Domain;

namespace App.BLL.DTO.Entities;

public class TimeWindow : BaseEntityId
{
    public DateTime From { get; set; }
    public DateTime Until { get; set; }
    public TimeSpan FreeTime { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public List<WorkTaskTimeWindow>? WorkTaskTimeWindows { get; set; }
}