using App.DAL.DTO.Entities;
using App.DAL.DTO.Identity;
using App.Domain;
using Base.Domain;

namespace App.DAL.DTO.ManyToMany;

public class SubjectRole : BaseEntityId
{
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }
    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }
    public EAccessType AccessType { get; set; }
}