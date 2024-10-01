using App.Domain;
using App.DTO.v1_0.Entities;

namespace App.DTO.v1_0;

public class SubjectCreateUpdate
{
    public Subject Subject { get; set; } = default!;
    public List<Guid> Roles { get; set; } = default!;
    public int Semester { get; set; }
    public EAccessType AccessType { get; set; } = default!;
}