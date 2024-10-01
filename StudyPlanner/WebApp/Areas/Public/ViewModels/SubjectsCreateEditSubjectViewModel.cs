using App.BLL.DTO.Entities;
using App.BLL.DTO.Identity;
using App.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Public.ViewModels;

public class SubjectsCreateEditSubjectViewModel
{
    
    public Subject Subject { get; set; } = default!;
    public SelectList? ModuleSelectList { get; set; }
    public List<Guid> Roles { get; set; } = default!;
    public MultiSelectList? RoleSelectList { get; set; }
    public EAccessType AccessType { get; set; } = default!;
    public int Semester { get; set; }
    public IEnumerable<EAccessType>? AccessTypes { get; set; } = default!;
    public Guid UserId { get; set; }
}