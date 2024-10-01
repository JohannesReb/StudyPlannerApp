using App.Domain;
using Base.Domain;

namespace App.DTO.v1_0.ManyToMany;

public class EwentRole : BaseEntityId
{
    public Guid RoleId { get; set; }
    public Guid EwentId { get; set; }
    public EAccessType AccessType { get; set; }
}