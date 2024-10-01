using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Domain;

namespace App.DTO.v1_0.Entities;

public class WorkTask : BaseEntityId
{
    public DateTime? Deadline { get; set; }
    [MaxLength(64)]
    public string Label { get; set; } = default!;
    public TimeSpan? TimeExpectancy { get; set; }
    [MaxLength(128)]
    public string? Code { get; set; }
    public double? MaxResult { get; set; }

    public ETaskType TaskType { get; set; }
    public Guid? ParentWorkTaskId { get; set; }
    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }
    public EField Field { get; set; }
    public Guid CreatedBy { get; set; }
}