using App.BLL.DTO.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Public.ViewModels;

public class CalendarCreateEditViewModel
{
    public Ewent Ewent { get; set; } = default!;
    public SelectList? Subjects { get; set; }
}