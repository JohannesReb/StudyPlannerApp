using App.BLL.DTO.Entities;
using App.BLL.DTO.ManyToMany;

namespace WebApp.Areas.Public.ViewModels;

public class SubjectsTaskDetailsDeleteViewModel
{
    public WorkTask WorkTask { get; set; } = default!;
    public List<UserWorkTask> UserWorkTasks { get; set; } = default!;
    public List<WorkTaskRole> WorkTaskRoles { get; set; } = default!;
    public Guid? SubjectId { get; set; }
}