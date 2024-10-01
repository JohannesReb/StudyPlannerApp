using App.BLL.DTO.Entities;
using App.BLL.DTO.ManyToMany;
using App.Domain;

namespace WebApp.Areas.Public.ViewModels;

public class SubjectsIndexViewModel
{
    public WorkTask WorkTask { get; set; } = default!;
    public IEnumerable<WorkTask> ChosenWorkTasks { get; set; } = default!;
    public IEnumerable<WorkTask> PublicWorkTasks { get; set; } = default!;
    public Subject Subject { get; set; } = default!;
    public IEnumerable<Subject> Subjects { get; set; } = default!;
    public IEnumerable<WorkTaskRole> WorkTaskRoles { get; set; } = default!;
    public IEnumerable<EAccessType> SubjectAccessTypes { get; set; } = default!;
    public Guid UserId { get; set; }
    public Guid? SubjectId { get; set; }
}