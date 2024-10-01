using App.Domain.DbEntities;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain.ManyToMany;

public class UserCurriculum : BaseEntityId
{
    public EStatus Status { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid CurriculumId { get; set; }
    public Curriculum? Curriculum { get; set; }
}