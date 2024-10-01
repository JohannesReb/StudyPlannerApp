using App.DAL.DTO.Entities;
using App.DAL.DTO.Identity;
using App.Domain;
using Base.Domain;

namespace App.DAL.DTO.ManyToMany;

public class WorkTaskRole : BaseEntityId
{
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }
    public Guid WorkTaskId { get; set; }
    public WorkTask? WorkTask { get; set; }
    public EAccessType AccessType { get; set; }
}