
using App.Domain;
using App.DTO.v1_0.Entities;
using Base.Domain;

namespace App.DTO.v1_0.ManyToMany;

public class UserSubject : BaseEntityId
{
    public int? Grade { get; set; }
    public EStatus Status { get; set; }
    public int Semester { get; set; }
    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }
}