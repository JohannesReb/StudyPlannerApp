using App.BLL.DTO.Entities;
using App.Domain;
using App.Domain.Identity;
using Base.Domain;

namespace App.BLL.DTO.ManyToMany;

public class UserField : BaseEntityId
{
    public decimal Multiplier { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public EField Field { get; set; }
}