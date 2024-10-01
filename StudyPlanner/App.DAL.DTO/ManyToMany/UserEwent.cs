using App.Domain.Identity;
using App.DAL.DTO.Entities;
using Base.Domain;

namespace App.DAL.DTO.ManyToMany;

public class UserEwent : BaseEntityId
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid EwentId { get; set; }
    public Ewent? Ewent { get; set; }
}