using App.Domain.Identity;
using Base.Domain;

namespace App.Domain.ManyToMany;

public class UserField : BaseEntityId
{
    public double Multiplier { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public EField Field { get; set; }
}