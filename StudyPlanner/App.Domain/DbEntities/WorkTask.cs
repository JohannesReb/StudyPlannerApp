using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.ManyToMany;
using Base.Domain;

namespace App.Domain.DbEntities;

public class WorkTask : BaseEntityIdMetadata
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
    public WorkTask? ParentWorkTask { get; set; }
    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }
    public EField Field { get; set; }

    public List<WorkTaskTimeWindow>? WorkTaskTimeWindows { get; set; }
    public List<UserWorkTask>? UserWorkTasks { get; set; }
    public List<WorkTask>? SubWorkTasks { get; set; }
    public List<WorkTaskRole>? WorkTaskRoles { get; set; }
}