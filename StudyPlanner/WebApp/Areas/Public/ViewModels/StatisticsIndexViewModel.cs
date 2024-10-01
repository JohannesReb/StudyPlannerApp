using App.BLL.DTO.Entities;
using App.BLL.DTO.ManyToMany;

namespace WebApp.Areas.Public.ViewModels;

public class StatisticsIndexViewModel
{
    public WorkTask WorkTask { get; set; } = default!;
    public UserWorkTask UserWorkTask { get; set; } = default!;
    public List<UserWorkTask> UserWorkTasks { get; set; } = default!;
    public Subject Subject { get; set; } = default!;
    public List<Subject> Subjects { get; set; } = default!;
    public Guid? SubjectId { get; set; }
    public double TotalTimeSpent { get; set; }
    public int TasksCompleted { get; set; }
    public int TasksNotYetCompleted { get; set; }
}