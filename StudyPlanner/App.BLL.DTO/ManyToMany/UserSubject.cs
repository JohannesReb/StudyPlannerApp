using App.BLL.DTO.Entities;
using App.Domain;
using App.Domain.Identity;
using Base.Domain;

namespace App.BLL.DTO.ManyToMany;

public class UserSubject : BaseEntityId
{
    public int? Grade { get; set; }
    public EStatus Status { get; set; }
    public int Semester { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }
}