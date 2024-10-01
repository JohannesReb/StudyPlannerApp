using App.DAL.DTO.ManyToMany;
using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace App.DAL.DTO.Identity;

public class Role : IdentityRole<Guid>, IDomainEntityId
{
    public Guid? ParentRoleId { get; set; }
    public Role? ParentRole { get; set; }
    public List<Role>? ParentRoles { get; set; }
    public List<EwentRole>? EwentRoles { get; set; }
    public List<SubjectRole>? SubjectRoles { get; set; }
    public List<WorkTaskRole>? WorkTaskRoles { get; set; }
}