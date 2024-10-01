using App.BLL.DTO.Entities;
using App.BLL.DTO.Identity;
using App.Domain;
using Base.Domain;

namespace App.BLL.DTO.ManyToMany;

public class SubjectRole : BaseEntityId
{
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }
    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }
    public EAccessType AccessType { get; set; }
}