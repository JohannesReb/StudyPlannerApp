using App.Domain.Identity;
using App.DAL.DTO.Entities;
using App.Domain;
using Base.Domain;

namespace App.DAL.DTO.ManyToMany;

public class UserField : BaseEntityId
{
    public decimal Multiplier { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public EField Field { get; set; }
}