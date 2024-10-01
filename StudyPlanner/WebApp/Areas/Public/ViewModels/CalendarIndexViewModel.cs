using App.BLL.DTO.ManyToMany;
using App.BLL.DTO.Entities;

namespace WebApp.Areas.Public.ViewModels;

public class CalendarIndexViewModel
{
    public Ewent Ewent { get; set; } = default!;
    public WorkTask WorkTask { get; set; } = default!;
    public TimeWindow TimeWindow { get; set; } = default!;
    public IEnumerable<Ewent> Ewents { get; set; } = default!;
    public IEnumerable<WorkTaskTimeWindow> WorkTaskTimeWindows { get; set; } = default!;
    public IEnumerable<WorkTask> UnPlannedWorkTasks { get; set; } = default!;
    public Guid UserId { get; set; }
    public IEnumerable<EwentRole> EwentRoles { get; set; } = default!;
}