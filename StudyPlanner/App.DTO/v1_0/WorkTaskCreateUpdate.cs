using App.Domain;
using App.DTO.v1_0.Entities;

namespace App.DTO.v1_0;

public class WorkTaskCreateUpdate
{
    public WorkTask WorkTask { get; set; } = default!;
    public List<Guid> Roles { get; set; } = default!;
    public EAccessType AccessType { get; set; } = default!;
}