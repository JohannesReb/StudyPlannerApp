using App.BLL.DTO.Entities;
using App.BLL.DTO.ManyToMany;

namespace WebApp.Areas.Public.ViewModels;

public class TimeTableFinishViewModel
{
    public Guid WorkTaskId { get; set; }
    public WorkTask? WorkTask { get; set; }
    public UserWorkTask UserWorkTask { get; set; } = default!;
}