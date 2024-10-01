using App.Domain.DbEntities;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain.ManyToMany;

public class SubjectRole : BaseEntityId
{
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }
    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }
    public EAccessType AccessType { get; set; }
}