using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.BLL.DTO.Entities;
using App.BLL.DTO.ManyToMany;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp.Areas.Public.ViewModels;

namespace WebApp.Areas.Public.Controllers
{
    [Area("Public")]
    [Authorize]
    public class CalendarController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<User> _userManager;

        public CalendarController(IAppBLL bll, UserManager<User> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: Calendar
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var vm = new CalendarIndexViewModel
            {
                Ewents = await _bll.Ewents.GetAllAsync(userId),
                WorkTaskTimeWindows = await _bll.WorkTaskTimeWindows.GetAllSortedAsync(userId),
                UnPlannedWorkTasks = await _bll.WorkTasks.GetAllUnPlannedAsync(userId),
                EwentRoles = await _bll.EwentRoles.GetAllAsync(userId),
                UserId = userId
            };
            return View(vm);
        }

        // GET: Calendar/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var ewent = await _bll.Ewents
                .FirstOrDefaultAsync(id.Value, userId);
            if (ewent == null)
            {
                return NotFound();
            }

            return View(ewent);
        }

        // GET: Calendar/Create
        public async Task<IActionResult> Create()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var vm = new CalendarCreateEditViewModel()
            {
                Subjects = new SelectList(await _bll.Subjects.GetAllSortedAsync(userId), nameof(Subject.Id), nameof(Subject.Label))
            };
            return View(vm);
        }

        // POST: Calendar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CalendarCreateEditViewModel vm)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            if (ModelState.IsValid)
            {
                vm.Ewent.Id = Guid.NewGuid();
                vm.Ewent.CreatedBy = userId;
                vm.Ewent.CreatedAt = DateTime.Now;
                _bll.Ewents.Add(vm.Ewent);
                await _bll.SaveChangesAsync();
                _bll.UserEwents.Add(new UserEwent()
                {
                    Id = Guid.NewGuid(),
                    EwentId = vm.Ewent.Id,
                    UserId = userId
                });
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.Subjects = new SelectList(await _bll.Subjects.GetAllSortedAsync(userId), nameof(Subject.Id),
                nameof(Subject.Label), vm.Ewent.SubjectId);
            return View(vm);
        }

        // GET: Calendar/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var ewent = await _bll.Ewents.FirstOrDefaultAsync(id.Value, userId);
            if (ewent == null)
            {
                return NotFound();
            }
            var vm = new CalendarCreateEditViewModel()
            {
                Ewent = ewent,
                Subjects = new SelectList(await _bll.Subjects.GetAllSortedAsync(userId), nameof(Subject.Id), nameof(Subject.Label), ewent.SubjectId)
            };
            return View(vm);
        }

        // POST: Calendar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CalendarCreateEditViewModel vm)
        {
            if (id != vm.Ewent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Ewents.Update(vm.Ewent);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.Ewents.ExistsAsync(vm.Ewent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            vm.Subjects = new SelectList(await _bll.Subjects.GetAllSortedAsync(userId), nameof(Subject.Id),
                nameof(Subject.Label), vm.Ewent.SubjectId);
            return View(vm);
        }

        // GET: Calendar/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var ewent = await _bll.Ewents
                .FirstOrDefaultAsync(id.Value, userId);
            if (ewent == null)
            {
                return NotFound();
            }

            return View(ewent);
        }

        // POST: Calendar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var ewent = await _bll.Ewents.FirstOrDefaultAsync(id, userId);
            if (ewent != null)
            {
                await _bll.Ewents.RemoveAsync(ewent);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
