using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using App.DAL.DTO.Entities;
using App.Domain;
using Base.Domain;

namespace App.DAL.DTO.ManyToMany;

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