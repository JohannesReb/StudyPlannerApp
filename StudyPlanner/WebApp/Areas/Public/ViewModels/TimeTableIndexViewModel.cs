using App.BLL.DTO.Entities;
using App.BLL.DTO.ManyToMany;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Public.ViewModels;

public class TimeTableIndexViewModel
{
    public TimeWindow TimeWindow { get; set; } = default!;
    public WorkTask WorkTask { get; set; } = default!;
    public List<WorkTask> WorkTasks { get; set; } = default!;
    public List<TimeWindow> TimeWindows { get; set; } = default!;
    public List<WorkTaskTimeWindow> WorkTaskTimeWindows { get; set; } = default!;
    public UserWorkTask UserWorkTask { get; set; } = default!;
    public List<UserWorkTask> UserWorkTasks { get; set; } = default!;
}