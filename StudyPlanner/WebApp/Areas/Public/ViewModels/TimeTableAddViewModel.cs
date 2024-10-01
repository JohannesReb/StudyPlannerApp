using App.BLL.DTO.Entities;
using App.BLL.DTO.ManyToMany;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Public.ViewModels;

public class TimeTableAddViewModel
{
    public Guid TimeWindowId { get; set; }
    public Guid WorkTaskId { get; set; }
    public WorkTask? WorkTask { get; set; }
    public SelectList? TimeWindowsSelectList { get; set; }
}