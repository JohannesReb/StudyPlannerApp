
using App.Domain;
using Base.Domain;

namespace App.DTO.v1_0.ManyToMany;

public class UserField : BaseEntityId
{
    public decimal Multiplier { get; set; }
    public Guid UserId { get; set; }
    public EField Field { get; set; }
}