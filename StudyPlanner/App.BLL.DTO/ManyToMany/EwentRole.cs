using App.BLL.DTO.Entities;
using App.Domain;
using App.Domain.Identity;
using Base.Domain;

namespace App.BLL.DTO.ManyToMany;

public class EwentRole : BaseEntityId
{
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }
    public Guid EwentId { get; set; }
    public Ewent? Ewent { get; set; }
    public EAccessType AccessType { get; set; }
}