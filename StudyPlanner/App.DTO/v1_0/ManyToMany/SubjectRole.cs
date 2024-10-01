using App.Domain;
using App.DTO.v1_0.Identity;
using Base.Domain;

namespace App.DTO.v1_0.ManyToMany;

public class SubjectRole : BaseEntityId
{
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }
    public Guid SubjectId { get; set; }
    public EAccessType AccessType { get; set; }
}