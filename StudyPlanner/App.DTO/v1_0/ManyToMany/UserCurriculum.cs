using App.Domain;
using Base.Domain;

namespace App.DTO.v1_0.ManyToMany;

public class UserCurriculum : BaseEntityId
{
    public EStatus Status { get; set; }
    public Guid UserId { get; set; }
    public Guid CurriculumId { get; set; }
}