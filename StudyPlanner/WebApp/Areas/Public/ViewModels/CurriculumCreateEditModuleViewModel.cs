using App.BLL.DTO.Entities;

namespace WebApp.Areas.Public.ViewModels;

public class CurriculumCreateEditModuleViewModel
{
    public Module Module { get; set; } = default!;
    public Guid? CurriculumId { get; set; }
}