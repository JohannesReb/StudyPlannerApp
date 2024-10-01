using App.BLL.DTO.Entities;
using App.BLL.DTO.ManyToMany;

namespace WebApp.Areas.Public.ViewModels;

public class SubjectsSubjectDetailsDeleteViewModel
{
    public Subject Subject { get; set; } = default!;
    public List<UserSubject> UserSubjects { get; set; } = default!;
    public List<SubjectRole> SubjectRoles { get; set; } = default!;
}