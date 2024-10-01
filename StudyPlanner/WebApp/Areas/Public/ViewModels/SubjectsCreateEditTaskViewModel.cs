using App.BLL.DTO.Entities;
using App.BLL.DTO.Identity;
using App.BLL.DTO.ManyToMany;
using App.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Public.ViewModels;

public class SubjectsCreateEditTaskViewModel
{
    public WorkTask WorkTask { get; set; } = default!;
    public SelectList? ParentWorkTaskSelectList { get; set; }
    public SelectList? WorkTaskCodeSelectList { get; set; }
    public List<Guid> Roles { get; set; } = default!;
    public MultiSelectList? RoleSelectList { get; set; }
    public EAccessType AccessType { get; set; } = default!;
    public Guid? SubjectId { get; set; }
    public IEnumerable<EAccessType>? AccessTypes { get; set; } = default!;
    public Guid UserId { get; set; }
}