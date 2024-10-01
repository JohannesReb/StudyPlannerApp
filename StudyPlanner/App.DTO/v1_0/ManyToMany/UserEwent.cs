
using Base.Domain;

namespace App.DTO.v1_0.ManyToMany;

public class UserEwent : BaseEntityId
{
    public Guid UserId { get; set; }
    public Guid EwentId { get; set; }
}