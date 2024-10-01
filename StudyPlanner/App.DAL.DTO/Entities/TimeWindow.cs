using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using App.DAL.DTO.ManyToMany;
using Base.Domain;

namespace App.DAL.DTO.Entities;

public class TimeWindow : BaseEntityId
{
    public DateTime From { get; set; }
    public DateTime Until { get; set; }
    public TimeSpan FreeTime { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public List<WorkTaskTimeWindow>? WorkTaskTimeWindows { get; set; }
}