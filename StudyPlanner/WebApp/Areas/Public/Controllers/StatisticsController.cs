using App.Contracts.BLL;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp.Areas.Public.ViewModels;

namespace WebApp.Areas.Public.Controllers
{
    [Area("Public")]
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<User> _userManager;

        public StatisticsController(IAppBLL bll, UserManager<User> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: Statistics
        public async Task<IActionResult> Index(Guid? subjectId)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var vm = new StatisticsIndexViewModel();
            if (subjectId != null)
            {
                vm.UserWorkTasks = (await _bll.UserWorkTasks.GetAllAsync(userId)).ToList();
                vm.Subjects = (await _bll.Subjects.GetAllSortedAsync(userId)).ToList();
                vm.Subject = vm.Subjects.FirstOrDefault(s => s.Id.Equals(subjectId.Value))!;
                vm.SubjectId = subjectId;
            }
            else
            {
                vm.UserWorkTasks = (await _bll.UserWorkTasks.GetAllAsync(userId)).ToList();
                vm.Subjects = (await _bll.Subjects.GetAllSortedAsync(userId)).ToList();
                vm.SubjectId = subjectId;
            }

            vm.TotalTimeSpent = vm.UserWorkTasks.Select(u => u.TimeSpent).Sum(u => (u ?? TimeSpan.Zero).TotalMinutes);
            vm.TasksCompleted = vm.UserWorkTasks.Where(w => w.Status.Equals(EStatus.Completed)).ToList().Count;
            vm.TasksNotYetCompleted = vm.UserWorkTasks.Where(w => !w.Status.Equals(EStatus.Completed)).ToList().Count;
            
            return View(vm);
        }
    }
}
