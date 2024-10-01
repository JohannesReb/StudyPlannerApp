using App.BLL.DTO.Entities;
using App.BLL.DTO.ManyToMany;

namespace WebApp.Areas.Public.ViewModels;

public class CurriculumIndexViewModel
{
    public Subject Subject { get; set; } = default!;
    public Curriculum? Curriculum { get; set; }
    public Module? Module { get; set; }
    public UserSubject UserSubject { get; set; } = default!;
    public IEnumerable<Subject> ChosenSubjects { get; set; } = default!;
    public IEnumerable<Subject> PublicSubjects { get; set; } = default!;
    public IEnumerable<Curriculum> Curriculums { get; set; } = default!;
    public IEnumerable<UserSubject> UserSubjects { get; set; } = default!;
    public IEnumerable<Module> Modules { get; set; } = default!;
    public Guid UserId { get; set; }
    public Guid? CurriculumId { get; set; }
    public Guid? ModuleId { get; set; }

    public int Completed { get; set; }
    public int Missing { get; set; }
    public int Declared { get; set; }
    public int NotDeclared { get; set; }
}